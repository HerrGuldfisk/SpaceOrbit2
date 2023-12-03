using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerSpawner : MonoBehaviour {
    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private Transform pointerCanvas;

    private void OnEnable() {
        SonarWave.OnStarPositionDetected += OnStarPositionDetected;
    }

    private void OnDisable() {
        SonarWave.OnStarPositionDetected -= OnStarPositionDetected;
    }

    private void OnStarPositionDetected(Transform starTransform) {
        GameObject pointer = Instantiate(pointerPrefab, pointerCanvas.transform);
        pointer.GetComponent<Pointer>().target = starTransform;
        pointer.GetComponent<Image>().color = Color.yellow;
    }
}