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
        if(target == null) return;

        transform.position = Vector3.Slerp(transform.position, target.transform.position + _offset, 1f);   
    }
}
