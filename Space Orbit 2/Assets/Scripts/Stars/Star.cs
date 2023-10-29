using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Star : MonoBehaviour
{
    private StarManager _starManager;

    private void Awake()
    {
        _starManager = FindObjectOfType<StarManager>();
        Debug.Assert((_starManager != null), "no star manager found in scene");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _starManager.AddStarToHeld();
            Destroy(gameObject);
        }
    }
}