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

    [Header("Data (Read only)")]
    [SerializeField] public float currentSpeed = 0;

    [Header("Drive Settings")]
    [SerializeField] private float _initialSpeed = 8;
    [SerializeField] private float _accelerationFactor = 1;
    [SerializeField] private float _decelerationFactor = 1;
    [SerializeField] private float _steeringFactor = 10;

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
            rb.AddForce(graphics.transform.TransformDirection(Vector3.left * _steeringFactor * Mathf.Clamp(Mathf.Log(rb.velocity.magnitude, 3), 1, 10)));
        }
        else if (rawInput.x > 0) 
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector3.right * _steeringFactor * Mathf.Clamp(Mathf.Log(rb.velocity.magnitude, 3), 1, 10)));
        }

        if(rawInput.y < 0)
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector3.back * rb.velocity.magnitude * _accelerationFactor));
        }
        if (rawInput.y > 0) 
        {
            rb.AddForce(graphics.transform.TransformDirection(Vector3.forward * rb.velocity.magnitude * _decelerationFactor));
        }


        currentSpeed = rb.velocity.magnitude;

        graphics.transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }

    void OnMoveInput(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }
}
