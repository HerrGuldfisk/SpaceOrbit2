using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//points to an object off-screen, and hides if the object is on-screen
[RequireComponent(typeof(Image))]
public class Pointer : MonoBehaviour {
    public Transform target;
    Renderer targetRenderer;
    Camera mainCamera;
    private float canvasWidth;
    private float canvasHeight;
    private Image pointerImage => GetComponent<Image>();

    void Start() {
        mainCamera = Camera.main;
        targetRenderer = target.GetComponentInChildren<Renderer>();
        Debug.Assert(targetRenderer != null, "no renderer found on target");
        Canvas canvas = GetComponentInParent<Canvas>();
        canvasWidth = canvas.pixelRect.width;
        canvasHeight = canvas.pixelRect.height;
    }

    void Update() {
        if (!target || !targetRenderer) {
            return;
        }

        //check if the target is on the main cam
        Vector3 objectPosOnScreen = mainCamera.WorldToViewportPoint(target.position);
        bool targetIsOnScreen = objectPosOnScreen.x > 0 && objectPosOnScreen.x < 1 && objectPosOnScreen.y > 0 &&
                                objectPosOnScreen.y < 1;

        if (targetIsOnScreen) {
            pointerImage.enabled = false;
        }
        else {
            pointerImage.enabled = true;
            PositionPointer(objectPosOnScreen);
        }
    }

    private void PositionPointer(Vector3 targetPosOnScreen) {
        float pointerX = Mathf.Clamp(targetPosOnScreen.x, 0.05f, 0.95f);
        float pointerY = Mathf.Clamp(targetPosOnScreen.y, 0.05f, 0.95f);
        float pointerXInCanvasScale = pointerX * canvasWidth;
        float pointerYInCanvasScale = pointerY * canvasHeight;
        transform.position = new Vector3(pointerXInCanvasScale, pointerYInCanvasScale, 0);
    }
}