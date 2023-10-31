using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Gravitable : MonoBehaviour
{
    /*[HideInInspector]*/
    public GameObject rootObject;

    [HideInInspector] public Rigidbody2D rb;

    private bool _inOrbit;
    public bool InOrbit 
    {
        get { return _inOrbit; }
        set 
        {
            _inOrbit = value;
            OnOrbit?.Invoke(value);  
        }
    }

    public BoolEvent OnOrbit;

    void Start()
    {
        rootObject = transform.root.gameObject;

        rb = GetComponent<Rigidbody2D>();
    }


}
