using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class GravityProvider : MonoBehaviour
{
    protected List<Gravitable> _objectsInOrbit = new List<Gravitable>();


    void Update()
    {
        if(_objectsInOrbit.Count > 0)
        {
            CalculateGravity();
        }
    }

    public abstract void ApplyGravity(GameObject objectInOrbit, float gravityStrength);

    public abstract void CalculateGravity();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Gravitable newObjectInGravityField))
        {
            if (newObjectInGravityField.rootObject.tag == "Ship")
            {
                CameraFollow.Instance.ChangeTarget(transform.root.gameObject, CameraFollow.FollowType.Planet);
            }

            _objectsInOrbit.Add(newObjectInGravityField);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Gravitable objectLeavingGravityField))
        {

            if (objectLeavingGravityField.rootObject.tag == "Ship")
            {
                CameraFollow.Instance.ChangeTarget(objectLeavingGravityField.rootObject, CameraFollow.FollowType.PlayerShip);
            }

            _objectsInOrbit.Remove(objectLeavingGravityField);
        }
    }
}
