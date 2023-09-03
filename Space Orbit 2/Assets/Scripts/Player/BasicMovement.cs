using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class BasicMovement : MonoBehaviour
{

    Rigidbody rb;

    Vector2 rawInput = Vector2.zero;

    GameObject graphics;

    [Header("Drive Settings")]

    [SerializeField] private float _currentSpeed = 0;
    [SerializeField] private float _initialSpeed = 8;
    [SerializeField] private float _steering = 10;

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
        if (rawInput.x < 0) 
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector3.left * _steering * Mathf.Clamp(Mathf.Log(rb.velocity.magnitude, 3), 1, 10)));
        }
        else if (rawInput.x > 0) 
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector3.right * _steering * Mathf.Clamp(Mathf.Log(rb.velocity.magnitude, 3), 1, 10)));
        }

        if(rawInput.y < 0)
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector3.back * rb.velocity.magnitude));
        }
        if (rawInput.y > 0) 
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector3.forward * rb.velocity.magnitude / 2f));
        }


        _currentSpeed = rb.velocity.magnitude;

        graphics.transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }

    void OnMoveInput(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }
}
