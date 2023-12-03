using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PointerSpawner))]
public class StarPointerSpawner : MonoBehaviour {
    private PointerSpawner _pointerSpawner;

    private void Awake() {
        _pointerSpawner = GetComponent<PointerSpawner>();
    }

    private void OnEnable() {
        SonarWave.OnObjectDetected += OnObjectDetected;
    }

    private void OnDisable() {
        SonarWave.OnObjectDetected -= OnObjectDetected;
    }

    private void OnObjectDetected(GameObject objectDetected) {
        bool isStar = objectDetected.CompareTag("Star");
        if (isStar) {
            _pointerSpawner.TrySpawnPointer(objectDetected, Color.yellow);
        }
    }
}