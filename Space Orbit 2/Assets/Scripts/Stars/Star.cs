using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Star : MonoBehaviour {
    private StarManager _starManager;

    public delegate void StarCollected(GameObject starCollected);

    public static event StarCollected OnStarCollected;

    public delegate void StarSpawned(Vector2 starPosition);

    public static event StarSpawned OnStarSpawned;

    private void Awake() {
        _starManager = FindObjectOfType<StarManager>();
        OnStarSpawned?.Invoke(transform.position);
        Debug.Assert((_starManager != null), "no star manager found in scene");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            _starManager.AddStarToHeld();
            OnStarCollected?.Invoke(gameObject);
            Destroy(gameObject);
        }
    }
}