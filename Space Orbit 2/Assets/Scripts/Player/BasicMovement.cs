using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class BasicMovement : MonoBehaviour
{

    Rigidbody2D rb;

    Vector2 rawInput = Vector2.zero;

    GameObject graphics;

    [Header("Data (Read only)")]
    [SerializeField] public float currentSpeed = 0;

    [Header("Drive Settings")]
    [SerializeField] private float _initialSpeed = 8f;

    [SerializeField] private float _steerFactor = 2.4f;
    [SerializeField] private float _thrustFactor = 16f;

    [SerializeField] private float _accelerationFactor = 1f;
    [SerializeField] private float _decelerationFactor = 2f;

    [SerializeField] private float _maxThrustSpeed = 300f;
    [SerializeField] private float _maxRotationSpeed = 1f;

    [SerializeField] private float _speedDrag = 0.0000f;
    [SerializeField] private float _angularDrag = 1.0000f;


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
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.forward * _initialSpeed;

        graphics = transform.GetChild(0).gameObject;
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
    }

    void FixedUpdate()
    {
        // Save current velocity
        _currentVelocity = rb.velocity;

        PhysicsMovement();
        //PhysicsMovementTest();

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

    void OldMovement()
    {
        // OOPS I BROKE IT?
        if (rawInput.x < 0) 
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector2.left * _steerFactor * Mathf.Clamp(Mathf.Log(rb.velocity.magnitude, 3), 1, 10)));
        }
        else if (rawInput.x > 0) 
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector2.right * _steerFactor * Mathf.Clamp(Mathf.Log(rb.velocity.magnitude, 3), 1, 10)));
        }

        if(rawInput.y < 0)
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector2.down * rb.velocity.magnitude * _accelerationFactor));
        }
        if (rawInput.y > 0) 
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector2.up * rb.velocity.magnitude * _decelerationFactor));
        }


        currentSpeed = rb.velocity.magnitude;

        graphics.transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }

    void PhysicsMovement()
    {
        if (_thrustInput != 0)
        {
            if (_thrustInput > 0)
            {
                _thrustForce = _thrustFactor * _accelerationFactor * _currentDirection;
            }
            else if (_thrustInput < 0)
            {
                _thrustForce = rb.velocity * -_decelerationFactor;
            }

            rb.AddForce(_thrustForce, ForceMode2D.Force);
        }

        if (_steerInput != 0)
        {
            rb.angularVelocity = 0;
            rb.rotation += _steerInput * _steerFactor;
            _desiredVelocity = _currentVelocity.magnitude * _currentDirection.normalized;
            rb.velocity = _desiredVelocity;
        }
    }

    void PhysicsMovementTest()
    {
        if (_steerInput != 0)
        {
            rb.AddTorque(_steerFactor * _steerInput);
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
