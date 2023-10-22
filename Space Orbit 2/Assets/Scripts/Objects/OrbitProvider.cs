using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class OrbitProvider : MonoBehaviour
{
    [SerializeField] 
    private int _numPlanets;

    [SerializeField] 
    private int _numAsteroids;

    public GameObject planetPrefab;
    public GameObject asteroidPrefab;

    [SerializeField]
    private Orbitable[] _allInOrbit;

    // Drag & drop the target in the inspector
    public Transform Target;

    public float CircleRadius = 1;

    public float RotationSpeed = 1;

    [Range(0, 360)]
    public float StartAngle = 0;

    public bool UseTargetCoordinateSystem = false;

    public bool LookAtTarget = false;

    private float angle;

    private void Start()
    {
        
    }

    private Vector3 ComputePositionOffset(float a)
    {
        a *= Mathf.Deg2Rad;

        // Compute the position of the object
        Vector3 positionOffset = new Vector3(
            Mathf.Cos(a) * CircleRadius,
            Mathf.Sin(a) * CircleRadius,
            Target.position.z
        );

        // Change position if the object must rotate in the coordinate system of the target
        // (i.e in the local space of the target)
        if (Target != null && UseTargetCoordinateSystem)
            positionOffset = Target.TransformVector(positionOffset);

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
        Vector3 position = Target != null ? Target.position : Vector3.zero;
        Vector3 normal = Vector2.up;
        Vector3 forward = Vector3.forward;
        Vector3 labelPosition;

        Vector3 positionOffset = ComputePositionOffset(StartAngle);
        Vector3 verticalOffset;


        if (Target != null && UseTargetCoordinateSystem)
        {
            normal = Target.up;
            forward = Target.forward;
        }
        verticalOffset = positionOffset.y * normal;

        // Draw label to indicate elevation
        if (Mathf.Abs(positionOffset.y) > 0.1)
        {
            UnityEditor.Handles.DrawDottedLine(position, position + verticalOffset, 5);
            labelPosition = position + verticalOffset * 0.5f;
            labelPosition += Vector3.Cross(verticalOffset.normalized, Target != null && UseTargetCoordinateSystem ? Target.forward : Vector3.forward) * 0.25f;
        }

        position += verticalOffset;
        positionOffset -= verticalOffset;

        UnityEditor.Handles.DrawWireArc(position, normal, forward, 360, CircleRadius);

        // Draw label to indicate radius
        UnityEditor.Handles.DrawLine(position, position + positionOffset);
        labelPosition = position + positionOffset * 0.5f;
        labelPosition += Vector3.Cross(positionOffset.normalized, Target != null && UseTargetCoordinateSystem ? Target.up : Vector3.up) * 0.25f;
        UnityEditor.Handles.Label(labelPosition, CircleRadius.ToString("0.00"));
    }

#endif
}
