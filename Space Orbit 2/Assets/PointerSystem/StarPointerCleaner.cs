using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cleans out star pointers from the canvas when the stars are collected
[RequireComponent(typeof(PointerSpawner))]
public class StarPointerCleaner : MonoBehaviour {
    private PointerSpawner _pointerSpawner;

    private void Awake() {
        _pointerSpawner = GetComponent<PointerSpawner>();
    }

    private void OnEnable() {
        Star.OnStarCollected += HandleStarCollected;
    }

    private void OnDisable() {
        Star.OnStarCollected -= HandleStarCollected;
    }

    private void HandleStarCollected(GameObject starCollected) {
        foreach (GameObject pointer in _pointerSpawner.pointersSpawned) {
            if (pointer.GetComponent<Pointer>().target == starCollected.transform) {
                _pointerSpawner.RemovePointer(pointer.GetComponent<Pointer>());
                break;
            }
        }
    }
}