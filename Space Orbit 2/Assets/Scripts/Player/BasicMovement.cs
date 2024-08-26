using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class BasicMovement : Gravitable {
    Vector2 rawInput = Vector2.zero;

    GameObject graphics;

    FuelTank _fuelTank;

    public FuelTank FuelTank {
        get { return _fuelTank; }
    }

    [Header("Data (Read only)")] [SerializeField]
    public float currentSpeed = 0.0f;

    [Header("Drive Settings")] [SerializeField]
    private float _steerFactor = 3f;

    [SerializeField] private float _thrustFactor = 20.0f;

    [SerializeField] private float _accelerationFactor = 1.0f;
    [SerializeField] private float _decelerationFactor = 2.0f;

    [SerializeField] private float _maxThrustSpeed = 300.0f;

    [SerializeField] private float _speedDrag = 0.0000f;
    [SerializeField] private float _angularDrag = 0.0000f;

    [SerializeField] private float _fuelUsageThrusting = 1f;
    [SerializeField] private float _fuelUsageSteering = 0.1f;

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

    private bool _goingFast;

    void Start() {
        graphics = transform.GetChild(0).gameObject;

        rootObject = transform.root.gameObject;

        rb = GetComponent<Rigidbody2D>();

        Camera.main.GetComponent<CameraFollow>().ResetTarget();

        _fuelTank = GetComponent<FuelTank>();
    }

    void Update() {
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

        if (_currentVelocity.magnitude > 100) {
            if (!_goingFast) {
                _goingFast = true;
                //AudioManager.Instance.GoingFast();
            }
        }

        if (_currentVelocity.magnitude < 100) {
            if (_goingFast) {
                _goingFast = false;
                //AudioManager.Instance.GoingSlow();
            }
        }
    }

    void FixedUpdate() {
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

    void OnMoveInput(InputValue value) {
        rawInput = value.Get<Vector2>();
    }

    void PhysicsMovement() {
        if (_thrustInput != 0) {
            if (_fuelTank.IsFuelEmpty()) {
                print("FUEL IS EMPTY");
                return;
            }

            if (_thrustInput > 0) {
                _thrustForce = _thrustFactor * _accelerationFactor * _currentDirection;
            }
            else if (_thrustInput < 0) {
                _thrustForce = rb.velocity * -_decelerationFactor;
            }

            rb.AddForce(_thrustForce, ForceMode2D.Force);

            if (currentSpeed >= 1) {
                if (!InOrbit) {
                    _fuelTank.TryRemoveFuel(_fuelUsageThrusting);
                }
            }
        }

        if (_steerInput != 0) {
            if (_fuelTank.IsFuelEmpty()) {
                print("FUEL IS EMPTY");
                return;
            }

            rb.angularVelocity = 0;
            //rb.rotation += _steerInput * _steerFactor + Vector2.SignedAngle(_currentDirection, _currentVelocity);
            rb.MoveRotation(rb.rotation + _steerInput * _steerFactor + Vector2.SignedAngle(_currentDirection, _currentVelocity));
            _desiredVelocity = _currentVelocity.magnitude * _currentDirection.normalized;
            rb.velocity = _desiredVelocity;

            if (!InOrbit) {
                _fuelTank.TryRemoveFuel(_fuelUsageSteering);
            }
        }

        if (_steerInput == 0) {
            rb.angularVelocity = 0;
            //rb.rotation += Vector2.SignedAngle(_currentDirection, _currentVelocity);
            rb.MoveRotation(rb.rotation + Vector2.SignedAngle(_currentDirection, _currentVelocity));
        }
    }

    void PhysicsMovementTest() {
        if (_steerInput != 0) {
            rb.AddTorque(_steerFactor / 10f * _steerInput);
        }

        if (_thrustInput != 0) {
            if (_thrustInput < 0) {
                //rb.AddForce(-_currentDirection * _thrustFactor * _decelerationFactor);
                _thrustForce = rb.velocity * -_decelerationFactor;
            }
        }
        else {
            _thrustForce = _currentDirection * _thrustFactor * _accelerationFactor;
        }

        rb.AddForce(_thrustForce, ForceMode2D.Force);
    }

    void NoPhysicsMovement() {
    }

    public Vector2 GetDirection() {
        return _currentDirection;
    }
}