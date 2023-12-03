using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerSpawner : MonoBehaviour {
    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private Transform pointerCanvas;

    List<GameObject> objectsPointedAt = new List<GameObject>();

    private void OnEnable() {
        SonarWave.OnObjectDetected += OnObjectDetected;
    }

    private void OnDisable() {
        SonarWave.OnObjectDetected -= OnObjectDetected;
    }

    private void OnObjectDetected(GameObject objectDetected) {
        bool isStar = objectDetected.CompareTag("Star");

        if (isStar) {
            bool alreadyPointingAtObject = objectsPointedAt.Contains(objectDetected);
            bool shouldAddPointer = !alreadyPointingAtObject;
            if (shouldAddPointer) {
                GameObject pointer = Instantiate(pointerPrefab, pointerCanvas.transform);
                pointer.GetComponent<Image>().color = Color.yellow;
                pointer.GetComponent<Pointer>().target = objectDetected.transform;
                objectsPointedAt.Add(objectDetected);
            }
        }
    }
}