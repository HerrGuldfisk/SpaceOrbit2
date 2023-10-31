using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class GravityProvider : MonoBehaviour
{
    protected List<Gravitable> _objectsInOrbit = new List<Gravitable>();

    protected CameraFollow cameraFollow;


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
                if(cameraFollow == null)
                {
                    cameraFollow = Camera.main.GetComponent<CameraFollow>();
                }
                cameraFollow.ChangeTarget(transform.root.gameObject, CameraFollow.FollowType.Planet);
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
                if (cameraFollow == null)
                {
                    cameraFollow = Camera.main.GetComponent<CameraFollow>();
                }

                cameraFollow.ChangeTarget(objectLeavingGravityField.rootObject, CameraFollow.FollowType.PlayerShip);
            }

            _objectsInOrbit.Remove(objectLeavingGravityField);
            objectLeavingGravityField.InOrbit = false;
        }
    }
}
