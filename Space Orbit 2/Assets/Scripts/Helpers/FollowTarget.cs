using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;

    private Vector3 _offset;


    private void Start()
    {
        _offset = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, target.transform.position + _offset, 0.05f);   
    }
}
