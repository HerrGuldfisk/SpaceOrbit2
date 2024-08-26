using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//points to an object off-screen, and hides if the object is on-screen
public class Pointer : MonoBehaviour {
    public Transform target;
    Renderer _targetRenderer;
    Camera _mainCamera;
    private float _canvasWidth;
    private float _canvasHeight;
    private Transform playerT;
    [SerializeField] private GameObject pointerImageGo;
    [SerializeField] private CanvasGroup pointerCanvasGroup;
    [SerializeField] private Image fillImage;
    [SerializeField] private float distanceBetweenPointerAndPlayer = 25f;
    [SerializeField] private float minPointerDistanceFromScreenEdge = 20;

    void Start() {
        _mainCamera = Camera.main;
        _targetRenderer = target.GetComponentInChildren<Renderer>();
        playerT = FindObjectOfType<BasicMovement>().transform;
        Debug.Assert(playerT != null, "no player found");
        Debug.Assert(_targetRenderer != null, "no renderer found on target");
        Canvas canvas = GetComponentInParent<Canvas>();
        _canvasWidth = canvas.pixelRect.width;
        _canvasHeight = canvas.pixelRect.height;
    }

    void Update() {
        if (!target || !_targetRenderer) {
            return;
        }

        //check if the target is on the main cam
        Vector3 targetPosOnScreen = _mainCamera.WorldToViewportPoint(target.position);
        bool targetIsOnScreen = targetPosOnScreen.x > 0 && targetPosOnScreen.x < 1 && targetPosOnScreen.y > 0 &&
                                targetPosOnScreen.y < 1;

        if (targetIsOnScreen) {
            pointerImageGo.SetActive(false);
        }
        else {
            pointerImageGo.SetActive(true);
            PositionPointer();
            UpdatePointerIntensity(targetPosOnScreen);
        }
    }

    private void PositionPointer() {
        if (!playerT) return;

        Vector2 targetPos = target.position;
        Vector2 playerPos = playerT.position;
        Vector2 vecFromPlayerToTarget = targetPos - playerPos;
        Vector2 vecFromPlayerToPointer = vecFromPlayerToTarget.normalized * distanceBetweenPointerAndPlayer;
        Vector2 pointerPosWorld = playerPos + vecFromPlayerToPointer;
        Vector2 pointerPosScreen = _mainCamera.WorldToScreenPoint(pointerPosWorld);

        // Calculate the scaling factor from screen to canvas
        float scaleX = _canvasWidth / Screen.width;
        float scaleY = _canvasHeight / Screen.height;

        // Clamp the screen position and scale to canvas
        float clampedX = Mathf.Clamp(pointerPosScreen.x, minPointerDistanceFromScreenEdge, Screen.width - minPointerDistanceFromScreenEdge);
        float clampedY = Mathf.Clamp(pointerPosScreen.y, minPointerDistanceFromScreenEdge, Screen.height - minPointerDistanceFromScreenEdge);
        float pointerXInCanvasScale = clampedX * scaleX;
        float pointerYInCanvasScale = clampedY * scaleY;

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
        pointerCanvasGroup.alpha = opacityClamped;
    }

    public void SetColor(Color newColor) {
        fillImage.color = newColor;
    }
}