using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//points to an object off-screen, and hides if the object is on-screen
public class Pointer : MonoBehaviour {
    [SerializeField] Transform target;
    Camera mainCamera;
    private float canvasWidth;
    private float canvasHeight;

    void Start() {
        mainCamera = Camera.main;
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasWidth = canvas.pixelRect.width;
        canvasHeight = canvas.pixelRect.height;
    }

    void Update() {
        Vector3 objectPosOnScreen = mainCamera.WorldToViewportPoint(target.position);
        float pointerX = Mathf.Clamp(objectPosOnScreen.x, 0.1f, 0.9f);
        float pointerY = Mathf.Clamp(objectPosOnScreen.y, 0.1f, 0.9f);
        float pointerXInCanvasScale = pointerX * canvasWidth;
        float pointerYInCanvasScale = pointerY * canvasHeight;
        transform.position = new Vector3(pointerXInCanvasScale, pointerYInCanvasScale, 0);
    }
}