using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gravitable : MonoBehaviour
{
    /*[HideInInspector]*/ public GameObject rootObject;

    public Rigidbody2D rb;

    void Start()
    {
        rootObject = transform.root.gameObject;

        rb = GetComponent<Rigidbody2D>();
    }
}
