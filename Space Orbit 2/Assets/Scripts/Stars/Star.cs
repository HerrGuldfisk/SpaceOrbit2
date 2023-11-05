using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Star : MonoBehaviour {
    private StarManager _starManager;

    public delegate void StarCollected(Vector2 starPosition);

    public static event StarCollected OnStarCollected;

    private void Awake() {
        _starManager = FindObjectOfType<StarManager>();
        Debug.Assert((_starManager != null), "no star manager found in scene");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _starManager.AddStarToHeld();
            OnStarCollected?.Invoke(transform.position);
            Destroy(gameObject);
        }
    }
}