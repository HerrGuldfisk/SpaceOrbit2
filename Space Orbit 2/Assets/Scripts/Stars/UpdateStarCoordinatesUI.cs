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
        SonarWave.OnStarPositionDetected += OnStarPositionDetected;
        Star.OnStarCollected += OnStarCollected;
    }

    private void OnDisable() {
        SonarWave.OnStarPositionDetected -= OnStarPositionDetected;
        Star.OnStarCollected -= OnStarCollected;
    }

    private void OnStarPositionDetected(Vector2 starPosition) {
        bool starAlreadyListed = _detectedStarPositions.Contains(starPosition);
        if (!starAlreadyListed) {
            _detectedStarPositions.Add(starPosition);
            UpdateCoordinatesUI();
        }
    }

    private void OnStarCollected(Vector2 starPosition) {
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