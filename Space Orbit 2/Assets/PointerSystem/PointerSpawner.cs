using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PointerSpawner : MonoBehaviour {
    [SerializeField] private GameObject pointerPrefab;
    [SerializeField] private Transform pointerCanvas;

    public List<GameObject> objectsPointedAt = new List<GameObject>();
    public List<GameObject> pointersSpawned = new List<GameObject>();

    // private void OnEnable() {
    //     SonarWave.OnObjectDetected += OnObjectDetected;
    // }
    //
    // private void OnDisable() {
    //     SonarWave.OnObjectDetected -= OnObjectDetected;
    // }
    //
    // private void OnObjectDetected(GameObject objectDetected) {
    //     //separate a class that spawns pointers for stars. this is just for pointer control
    //     bool isStar = objectDetected.CompareTag("Star");
    //
    //     if (isStar) {
    //         TrySpawnPointer(objectDetected, Color.yellow);
    //     }
    // }

    public void TrySpawnPointer(GameObject target, Color color) {
        bool alreadyPointingAtObject = objectsPointedAt.Contains(target);
        bool shouldAddPointer = !alreadyPointingAtObject;
        if (shouldAddPointer) {
            GameObject newPointer = Instantiate(pointerPrefab, pointerCanvas.transform);
            newPointer.GetComponent<Image>().color = color;
            newPointer.GetComponent<Pointer>().target = target.transform;
            objectsPointedAt.Add(target);
            pointersSpawned.Add(newPointer);
        }
    }

    public void RemovePointer(Pointer pointer) {
        GameObject pointerGO = pointer.gameObject;
        GameObject targetGO = pointer.target.gameObject;
        objectsPointedAt.Remove(targetGO);
        Destroy(targetGO);
        pointersSpawned.Remove(pointerGO);
        Destroy(pointerGO);
    }
}