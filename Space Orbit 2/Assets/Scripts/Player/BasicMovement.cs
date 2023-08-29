using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class BasicMovement : MonoBehaviour
{

    Rigidbody rb;

    float rawInput = 0;

    GameObject graphics;

    [Header("Drive Settings")]

    [SerializeField] private float _currentSpeed = 0;
    [SerializeField] private float _initialSpeed = 8;
    [SerializeField] private float _steering = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward * 8;

        graphics = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        // Save heading direction to avoid recalculating each frame
        // Might need to move to fixed update
        graphics.transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }

    void FixedUpdate()
    {
        if(rawInput < 0) 
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector3.left * _steering * Mathf.Clamp(Mathf.Log(rb.velocity.magnitude, 3), 1, 10)));
        }
        else if (rawInput > 0) 
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector3.right * _steering * Mathf.Clamp(Mathf.Log(rb.velocity.magnitude, 3), 1, 10)));
            // rb.AddForce(graphics.transform.TransformDirection(Vector3.right * _steering));
        }

        _currentSpeed = rb.velocity.magnitude;
    }

    void OnTurn(InputValue value)
    {
        rawInput = value.Get<float>();
    }
}
