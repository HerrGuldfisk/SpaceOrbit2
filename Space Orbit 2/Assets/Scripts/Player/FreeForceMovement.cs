using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeForceMovement : MonoBehaviour
{
    [Header("Acceleration")]
    public float frontBack = 4f;
    public float sides = 2f;

    [Header("No touchie")]
    public GameObject top;
    public GameObject topLeft;
    public GameObject topRight;
    public GameObject bottom;
    public GameObject bottomLeft;
    public GameObject bottomRight;

    public MeshRenderer topFlame;
    public MeshRenderer topLeftFlame;
    public MeshRenderer topRightFlame;
    public MeshRenderer bottomFlame;
    public MeshRenderer bottomLeftFlame;
    public MeshRenderer bottomRightFlame;

    private float topInput;
    private float topLeftInput;
    private float topRightInput;
    private float bottomLeftInput;
    private float bottomRightInput;
    private float bottomInput;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Debug.Log(topInput);

        if(topInput == 1)
        {
            AddForceInDirectionAndPoint(frontBack, bottom.transform.TransformDirection(Vector3.forward), bottom.transform.position);
        }
        if(topLeftInput == 1)
        {
            AddForceInDirectionAndPoint(sides, topRight.transform.TransformDirection(Vector3.left), topRight.transform.position);
        }
        if (topRightInput == 1)
        {
            AddForceInDirectionAndPoint(sides, topLeft.transform.TransformDirection(Vector3.right), topLeft.transform.position);
        }
        if (bottomInput == 1)
        {
            AddForceInDirectionAndPoint(frontBack, top.transform.TransformDirection(Vector3.back), top.transform.position);
        }
        if (bottomLeftInput == 1)
        {
            AddForceInDirectionAndPoint(sides, bottomRight.transform.TransformDirection(Vector3.right), bottomRight.transform.position);
        }
        if (bottomRightInput == 1)
        {
            AddForceInDirectionAndPoint(sides, bottomLeft.transform.TransformDirection(Vector3.left), bottomLeft.transform.position);
        }

        
    }

    void AddForceInDirectionAndPoint(float force, Vector3 direction, Vector3 point)
    {
        rb.AddForceAtPosition(direction * force, point);
    }

    void OnTop(InputValue value)
    {
        topInput = value.Get<float>();

        if (topInput == 1)
        {
            bottomFlame.enabled = true;
        }
        else
        {
            bottomFlame.enabled = false;
        }
    }

    void OnTopLeft(InputValue value)
    {
        topLeftInput = value.Get<float>();

        if(topLeftInput == 1 )
        {
            topRightFlame.enabled = true;
        }
        else
        {
            topRightFlame.enabled = false;
        }
    }

    void OnTopRight(InputValue value)
    {
        topRightInput = value.Get<float>();

        if (topRightInput == 1)
        {
            topLeftFlame.enabled = true;
        }
        else
        {
            topLeftFlame.enabled = false;
        }
    }

    void OnBottom(InputValue value)
    {
        bottomInput = value.Get<float>();

        if (bottomInput == 1)
        {
            topFlame.enabled = true;
        }
        else
        {
            topFlame.enabled = false;
        }
    }

    void OnBottomLeft(InputValue value)
    {
        bottomLeftInput = value.Get<float>();

        if (bottomLeftInput == 1)
        {
            bottomLeftFlame.enabled = true;
        }
        else 
        { 
            bottomLeftFlame.enabled = false; 
        }
    }

    void OnBottomRight(InputValue value)
    {
        bottomRightInput = value.Get<float>();

        if (bottomRightInput == 1)
        {
            bottomRightFlame.enabled = true;
        }
        else
        {
            bottomRightFlame.enabled = false;
        }
    }
}
