using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class BasicMovement : Gravitable
{

    Vector2 rawInput = Vector2.zero;

    GameObject graphics;

    FuelSystem fuelSystem;

    public FuelSystem FuelSystem { get { return fuelSystem; } }

    [Header("Data (Read only)")]
    [SerializeField] public float currentSpeed = 0.0f;
    [SerializeField] public float currentTurningSpeed = 0.0f;

    [Header("Drive Settings")]
    [SerializeField] private float _initialSpeed = 0.0f;

    [SerializeField] private float _steerFactor = 3f;
    [SerializeField] private float _thrustFactor = 20.0f;

    [SerializeField] private float _accelerationFactor = 1.0f;
    [SerializeField] private float _decelerationFactor = 2.0f;

    [SerializeField] private float _maxThrustSpeed = 300.0f;
    [SerializeField] private float _maxRotationSpeed = 1.0f;

    [SerializeField] private float _speedDrag = 0.0000f;
    [SerializeField] private float _angularDrag = 0.0000f;

    [SerializeField] private float _maxFuel = 100f;
    [SerializeField] private float _fuelUsageThrusting = 1f;
    [SerializeField] private float _fuelUsageSteering = 0.1f;
    [SerializeField] private float _orbitRefuel = 0.1f;

    private float _steerInput;
    private float _thrustInput;

    private Vector2 _currentVelocity;
    private Vector2 _desiredVelocity;

    private Vector2 _inputDirection;
    private Vector2 _currentDirection;
    private Vector2 _desiredDirection;

    private Vector2 _thrustForce;
    private Vector2 _steerForce;
    private Vector2 _totalForce;
    private float _steerTorque;

    void Start()
    {
        graphics = transform.GetChild(0).gameObject;

        rootObject = transform.root.gameObject;

        rb = GetComponent<Rigidbody2D>();

        Camera.main.GetComponent<CameraFollow>().ResetTarget();

        fuelSystem = GetComponent<FuelSystem>();
        fuelSystem.SetMaxFuel(_maxFuel);
        fuelSystem.ResetFuel();
    }

    void Update()
    {
        // Adjust linear and angular drag to control speed and rotation.
        rb.drag = _speedDrag;
        rb.angularDrag = _angularDrag;

        // Splitting input in X and Y
        _thrustInput = rawInput.y;
        _steerInput = -rawInput.x;

        // Why cast this lol...
        _inputDirection = rawInput;

        // Save current dir
        _currentDirection = graphics.transform.forward;

        if (InOrbit)
        {
            fuelSystem.AddFuel(_orbitRefuel);
        }
    }

    void FixedUpdate()
    {
        // Save current velocity
        _currentVelocity = rb.velocity;

        PhysicsMovement();

        // Save current speed
        currentSpeed = rb.velocity.magnitude;

        // Clamp speed
        if (currentSpeed > _maxThrustSpeed) {
            rb.velocity = rb.velocity.normalized * _maxThrustSpeed;
        }
    }

    void OnMoveInput(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void PhysicsMovement()
    {
        if (_thrustInput != 0)
        {
            if (fuelSystem.IsFuelEmpty())
            {
                print("FUEL IS EMPTY");
                return;
            }

            if (_thrustInput > 0)
            {
                _thrustForce = _thrustFactor * _accelerationFactor * _currentDirection;
            }
            else if (_thrustInput < 0)
            {
                _thrustForce = rb.velocity * -_decelerationFactor;
            }

            rb.AddForce(_thrustForce, ForceMode2D.Force);

            if (currentSpeed >= 1)
            {
                if (!InOrbit)
                {
                    fuelSystem.TryRemoveFuel(_fuelUsageThrusting);
                }
            }
        }

        if (_steerInput != 0)
        {
            if (fuelSystem.IsFuelEmpty())
            {
                print("FUEL IS EMPTY");
                return;
            }

            rb.angularVelocity = 0;
            //rb.rotation += _steerInput * _steerFactor + Vector2.SignedAngle(_currentDirection, _currentVelocity);
            rb.MoveRotation(rb.rotation + _steerInput * _steerFactor + Vector2.SignedAngle(_currentDirection, _currentVelocity));
            _desiredVelocity = _currentVelocity.magnitude * _currentDirection.normalized;
            rb.velocity = _desiredVelocity;

            if (!InOrbit)
            {
                fuelSystem.TryRemoveFuel(_fuelUsageSteering);
            }
        }
        
        if(_steerInput == 0)
        {
            rb.angularVelocity = 0;
            //rb.rotation += Vector2.SignedAngle(_currentDirection, _currentVelocity);
            rb.MoveRotation(rb.rotation + Vector2.SignedAngle(_currentDirection, _currentVelocity));
        }
    }

    void PhysicsMovementTest()
    {
        if (_steerInput != 0)
        {
            rb.AddTorque(_steerFactor / 10f * _steerInput);
        }

        if (_thrustInput != 0)
        {
            if (_thrustInput < 0)
            {
                //rb.AddForce(-_currentDirection * _thrustFactor * _decelerationFactor);
                _thrustForce = rb.velocity * -_decelerationFactor;
            }
        }
        else
        {
            _thrustForce = _currentDirection * _thrustFactor * _accelerationFactor;
        }

        rb.AddForce(_thrustForce, ForceMode2D.Force);
    }

    void NoPhysicsMovement()
    {
        
    }
}
