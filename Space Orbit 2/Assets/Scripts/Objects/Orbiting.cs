using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Orbiting
{
    public GameObject orbitingObject;
    public Transform target;
    public float circleRadius = 1;
    public float rotationSpeed = 1;
    [Range(0, 360)]
    public float startAngle = 0;
    public bool useTargetCoordinateSystem = false;
    public bool lookAtTarget = false;
    public float angle;

    public Orbiting(GameObject _orbitingObject, Transform _target, float _circleRadius, float _rotationSpeed, float _startAngle, bool _useTargetCoordinateSystem, bool _lookAtTarget, float _angle)
    {
        orbitingObject = _orbitingObject;
        /* Replace with this?
        orbitingObject = GameObject.Instantiate(_orbitingObject) as GameObject;
        */
        target = _target;
        circleRadius = _circleRadius;
        rotationSpeed = _rotationSpeed;
        startAngle = _startAngle;
        useTargetCoordinateSystem = _useTargetCoordinateSystem;
        lookAtTarget = _lookAtTarget;
        angle = _angle;
    }

    public void UpdatePosition()
    {
        // Define the position the object must rotate around
        Vector3 position = target != null ? target.position : Vector3.zero;

        Vector3 positionOffset = ComputePositionOffset(angle);

        // Assign new position
        if(orbitingObject != null)
        {
            orbitingObject.transform.position = position + positionOffset;

            // Rotate object so as to look at the target
            if (lookAtTarget)
                orbitingObject.transform.rotation = Quaternion.LookRotation(position - orbitingObject.transform.position, target == null ? Vector3.up : target.forward);
        }

        angle += Time.deltaTime * rotationSpeed;
    }

    public Vector3 ComputePositionOffset(float a)
    {
        a *= Mathf.Deg2Rad;

        // Compute the position of the object
        Vector3 positionOffset = new Vector3(
            Mathf.Cos(a) * circleRadius,
            Mathf.Sin(a) * circleRadius,
            target.position.z
        );

        // Change position if the object must rotate in the coordinate system of the target
        // (i.e in the local space of the target)
        if (target != null && useTargetCoordinateSystem)
            positionOffset = target.TransformVector(positionOffset);

        return positionOffset;
    }
}
