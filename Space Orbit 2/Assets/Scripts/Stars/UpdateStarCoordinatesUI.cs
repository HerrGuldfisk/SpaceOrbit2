using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UpdateStarCoordinatesUI : MonoBehaviour {
    [SerializeField] TextMeshProUGUI starCoordinatesText;
    private readonly List<Vector2> _detectedStarPositions = new List<Vector2>();

    private void Awake() {
        starCoordinatesText.text = "";
    }

    private void OnEnable() {
        SonarWave.OnObjectDetected += OnObjectDetected;
        Star.OnStarCollected += OnStarCollected;
    }

    private void OnDisable() {
        SonarWave.OnObjectDetected -= OnObjectDetected;
        Star.OnStarCollected -= OnStarCollected;
    }

    private void OnObjectDetected(GameObject objectDetected) {
        bool isStar = objectDetected.CompareTag("Star");
        if (!isStar) {
            return;
        }

        Vector2 starPosition = objectDetected.transform.position;
        bool starAlreadyListed = _detectedStarPositions.Contains(starPosition);
        if (!starAlreadyListed) {
            _detectedStarPositions.Add(starPosition);
            UpdateCoordinatesUI();
        }
    }

    private void OnStarCollected(GameObject starCollected) {
        Vector2 starPosition = starCollected.transform.position;
        _detectedStarPositions.Remove(starPosition);
        UpdateCoordinatesUI();
    }

    private void UpdateCoordinatesUI() {
        string coordinatesText = "";
        foreach (Vector2 starPosition in _detectedStarPositions) {
            string coordinateX = starPosition.x.ToString("F2");
            string coordinateY = starPosition.y.ToString("F2");
            coordinatesText += $"({coordinateX}, {coordinateY})\n";
        }

        starCoordinatesText.text = coordinatesText;
    }
}