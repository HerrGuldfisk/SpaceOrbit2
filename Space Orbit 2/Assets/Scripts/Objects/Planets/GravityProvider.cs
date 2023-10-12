using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class GravityProvider : MonoBehaviour
{
    protected List<Gravitable> _objectsInOrbit = new List<Gravitable>();


    void FixedUpdate()
    {
        if(_objectsInOrbit.Count > 0)
        {
            CalculateGravity();
        }
    }

    public abstract void ApplyGravity(Gravitable objectInOrbit, float gravityStrength);

    public abstract void CalculateGravity();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Gravitable newObjectInGravityField))
        {
            if (newObjectInGravityField.rootObject.tag == "Player")
            {
                CameraFollow.Instance.ChangeTarget(transform.root.gameObject, CameraFollow.FollowType.Planet);
                AudioManager.Instance.PlayerEnterOrbit();
            }

            _objectsInOrbit.Add(newObjectInGravityField);
            newObjectInGravityField.InOrbit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Gravitable objectLeavingGravityField))
        {

            if (objectLeavingGravityField.rootObject.tag == "Player")
            {
                CameraFollow.Instance.ChangeTarget(objectLeavingGravityField.rootObject, CameraFollow.FollowType.PlayerShip);
                AudioManager.Instance.PlayerExitOrbit();
            }

            _objectsInOrbit.Remove(objectLeavingGravityField);
            objectLeavingGravityField.InOrbit = false;
        }
    }
}
