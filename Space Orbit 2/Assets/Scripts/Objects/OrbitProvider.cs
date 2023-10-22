using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class OrbitProvider : MonoBehaviour
{
    [SerializeField]
    public List<Orbiting> OrbitingObjects;

    private void Start()
    {
        
    }

    [SerializeField]
    public void AddNewOrbiting(GameObject _orbitingObject, Transform _target, float _circleRadius, float _rotationSpeed, float _startAngle, bool _useTargetCoordinateSystem, bool _lookAtTarget, float _angle)
    {
        OrbitingObjects.Add(new Orbiting(_orbitingObject, _target, _circleRadius, _rotationSpeed, _startAngle, _useTargetCoordinateSystem, _lookAtTarget, _angle));
    }
}
