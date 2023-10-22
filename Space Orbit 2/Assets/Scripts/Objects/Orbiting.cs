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
    private float angle;

    public Orbiting(GameObject _orbitingObject, Transform _target, float _circleRadius, float _rotationSpeed, float _startAngle, bool _useTargetCoordinateSystem, bool _lookAtTarget, float _angle)
    {
        orbitingObject = _orbitingObject;
        target = _target;
        circleRadius = _circleRadius;
        rotationSpeed = _rotationSpeed;
        startAngle = _startAngle;
        useTargetCoordinateSystem = _useTargetCoordinateSystem;
        lookAtTarget = _lookAtTarget;
        angle = _angle;
    }

    private Vector3 ComputePositionOffset(float a)
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

#if UNITY_EDITOR

    [SerializeField]
    private bool drawGizmos = true;

    private void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;

        // Draw an arc around the target
        Vector3 position = target != null ? target.position : Vector3.zero;
        Vector3 normal = Vector2.up;
        Vector3 forward = Vector3.forward;
        Vector3 labelPosition;

        Vector3 positionOffset = ComputePositionOffset(startAngle);
        Vector3 verticalOffset;


        if (target != null && useTargetCoordinateSystem)
        {
            normal = target.up;
            forward = target.forward;
        }
        verticalOffset = positionOffset.y * normal;

        // Draw label to indicate elevation
        if (Mathf.Abs(positionOffset.y) > 0.1)
        {
            UnityEditor.Handles.DrawDottedLine(position, position + verticalOffset, 5);
            labelPosition = position + verticalOffset * 0.5f;
            labelPosition += Vector3.Cross(verticalOffset.normalized, target != null && useTargetCoordinateSystem ? target.forward : Vector3.forward) * 0.25f;
        }

        position += verticalOffset;
        positionOffset -= verticalOffset;

        UnityEditor.Handles.DrawWireArc(position, normal, forward, 360, circleRadius);

        // Draw label to indicate radius
        UnityEditor.Handles.DrawLine(position, position + positionOffset);
        labelPosition = position + positionOffset * 0.5f;
        labelPosition += Vector3.Cross(positionOffset.normalized, target != null && useTargetCoordinateSystem ? target.up : Vector3.up) * 0.25f;
        UnityEditor.Handles.Label(labelPosition, circleRadius.ToString("0.00"));
    }

#endif
}
