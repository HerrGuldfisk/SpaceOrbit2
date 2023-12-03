using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.Mathematics;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

//[ExecuteInEditMode]
public class OrbitProvider : MonoBehaviour {
    [SerializeField] private GameObject orbitCenterPrefab;

    private Transform _planetTransform;

    private void Awake() {
        _planetTransform = transform;
    }
    // [SerializeField] public List<Orbiting> OrbitingObjects;

    // [SerializeField]
    // public void AddNewOrbiting(GameObject _orbitingObject, Transform _target, float _circleRadius, float _rotationSpeed, float _startAngle, bool _useTargetCoordinateSystem, bool _lookAtTarget, float _angle)
    // {
    //     Orbiting newOrbiting = new Orbiting(_orbitingObject, _target, _circleRadius, _rotationSpeed, _startAngle, _useTargetCoordinateSystem, _lookAtTarget, _angle);
    //     OrbitingObjects.Add(newOrbiting);
    // }

    public void AddObjectInOrbit(GameObject objectThatShouldOrbit) {
        GameObject orbitCenter =
            Instantiate(orbitCenterPrefab, transform.position, quaternion.identity, _planetTransform);
        objectThatShouldOrbit.transform.parent = orbitCenter.transform;
        orbitCenter.GetComponent<RotateContinuously>().angularVelocity = 10;
    }

//     private void LateUpdate()
//     {
//         foreach(var o in OrbitingObjects)
//         {
//             o.UpdatePosition();
//         }
//     }
//
// #if UNITY_EDITOR
//
//     [SerializeField]
//     private bool drawGizmos = true;
//
//     private void OnDrawGizmos()
//     {
//         if (!drawGizmos)
//             return;
//
//         foreach (var o in OrbitingObjects)
//         {
//             // Draw an arc around the target
//             Vector3 position = o.target != null ? o.target.position : Vector3.zero;
//             Vector3 normal = Vector3.forward;
//             Vector3 forward = Vector3.up;
//             Vector3 labelPosition;
//
//             Vector3 positionOffset = o.ComputePositionOffset(o.startAngle);
//             Vector3 verticalOffset;
//
//
//             if (o.target != null && o.useTargetCoordinateSystem)
//             {
//                 normal = o.target.forward;
//                 forward = o.target.up;
//             }
//             verticalOffset = positionOffset.y * normal;
//
//             // Draw label to indicate elevation
//             if (Mathf.Abs(positionOffset.y) > 0.1)
//             {
//                 UnityEditor.Handles.DrawDottedLine(position, position + verticalOffset, 5);
//                 labelPosition = position + verticalOffset * 0.5f;
//                 labelPosition += Vector3.Cross(verticalOffset.normalized, o.target != null && o.useTargetCoordinateSystem ? o.target.forward : Vector3.forward) * 0.25f;
//             }
//
//             position += verticalOffset;
//             positionOffset -= verticalOffset;
//
//             UnityEditor.Handles.DrawWireArc(position, normal, forward, 360, o.circleRadius);
//
//             // Draw label to indicate radius
//             UnityEditor.Handles.DrawLine(position, position + positionOffset);
//             labelPosition = position + positionOffset * 0.5f;
//             labelPosition += Vector3.Cross(positionOffset.normalized, o.target != null && o.useTargetCoordinateSystem ? o.target.forward : Vector3.forward) * 0.25f;
//             UnityEditor.Handles.Label(labelPosition, o.circleRadius.ToString("0.00"));
//         }
//     }
//
// #endif
}