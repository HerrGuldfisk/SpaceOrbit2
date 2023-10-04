using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicmovementFloaty : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float forwardInput = 0;
    float _steerInput;

    GameObject graphics;

    [Header("Drive Settings")]

    [SerializeField] private float _currentSpeed = 0;
    [SerializeField] private float _initialSpeed = 0;
    // [SerializeField] private float _steering = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * _initialSpeed;

        graphics = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        // Save heading direction to avoid recalculating each frame
        // Might need to move to fixed update
        // graphics.transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }

    void FixedUpdate()
    {
        if (forwardInput > 0)
        {
            rb.AddForce(transform.forward);
        }

        if (_steerInput < 0)
        {
            rb.AddTorque(new Vector3(0, -1, 0));
        }
        else if (_steerInput > 0)
        {
            rb.AddTorque(new Vector3(0, 1, 0));
        }


        _currentSpeed = rb.velocity.magnitude;
    }

    void OnForward(InputValue value)
    {
        forwardInput = value.Get<float>();
    }

    void OnTurn(InputValue value)
    {
        _steerInput = value.Get<float>();  
    }
}
