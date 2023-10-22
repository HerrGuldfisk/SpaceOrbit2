using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitable
{
    public Transform Target;

    public float CircleRadius = 1;

    public float RotationSpeed = 1;

    [Range(0, 360)]
    public float StartAngle = 0;

    public bool UseTargetCoordinateSystem = false;

    public bool LookAtTarget = false;

    private float angle;

    public Orbitable(Transform _target, float _circleRadius, float _rotationSpeed, float _startAngle, bool _useTargetCoordinateSystem, bool _lookAtTarget, float _angle)
    {
        Target = _target;
        CircleRadius = _circleRadius;
        RotationSpeed = _rotationSpeed;
        StartAngle = _startAngle;
        UseTargetCoordinateSystem = _useTargetCoordinateSystem;
        LookAtTarget = _lookAtTarget;
        angle = _angle;
    }
}
