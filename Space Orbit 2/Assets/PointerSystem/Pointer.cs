using System;
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
        Vector3 targetPosOnScreen = mainCamera.WorldToViewportPoint(target.position);
        bool targetIsOnScreen = targetPosOnScreen.x > 0 && targetPosOnScreen.x < 1 && targetPosOnScreen.y > 0 &&
                                targetPosOnScreen.y < 1;

        if (targetIsOnScreen) {
            pointerImage.enabled = false;
        }
        else {
            pointerImage.enabled = true;
            PositionPointer(targetPosOnScreen);
            UpdatePointerIntensity(targetPosOnScreen);
        }
    }

    private void PositionPointer(Vector3 targetPosOnScreen) {
        float pointerX = Mathf.Clamp(targetPosOnScreen.x, 0.05f, 0.95f);
        float pointerY = Mathf.Clamp(targetPosOnScreen.y, 0.05f, 0.95f);
        float pointerXInCanvasScale = pointerX * canvasWidth;
        float pointerYInCanvasScale = pointerY * canvasHeight;
        transform.position = new Vector3(pointerXInCanvasScale, pointerYInCanvasScale, 0);
    }

    //change size and opacity based on distance
    private void UpdatePointerIntensity(Vector2 targetPosOnScreen) {
        //change size based on distance
        Vector2 vecFromScreenCenterToObject = targetPosOnScreen - new Vector2(0.5f, 0.5f);
        float distanceToTarget = vecFromScreenCenterToObject.magnitude;
        float newScale = 1 / distanceToTarget;
        newScale = Mathf.Clamp(newScale, 0.6f, 1);
        transform.localScale = Vector3.one * newScale;

        //change opacity based on distance
        float opacity = 1 / distanceToTarget;
        float opacityClamped = Mathf.Clamp(opacity, 0.6f, 1);
        Color newPointerColor = pointerImage.color;
        newPointerColor.a = opacityClamped;
        pointerImage.color = newPointerColor;
    }
}