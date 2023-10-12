using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gravitable : MonoBehaviour
{
    /*[HideInInspector]*/
    public GameObject rootObject;

    [HideInInspector] public Rigidbody2D rb;

    public bool InOrbit { get; set; }

    void Start()
    {
        rootObject = transform.root.gameObject;

        rb = GetComponent<Rigidbody2D>();
    }
}
