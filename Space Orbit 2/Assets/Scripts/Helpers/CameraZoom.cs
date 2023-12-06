using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float _zoom;
    private float _zoomMultiplier = 0.8f;
    private float _minZoom = 32f;
    private float _maxZoom = 128f;
    private float _velCam = 0f;
    private float _smoothTime = 0.25f;

    [SerializeField] private Camera _cam;
    [SerializeField] private BasicMovement _playerData;
    [SerializeField] private float _playerSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        _zoom = _cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        _playerSpeed = _playerData.currentSpeed;
        _zoom = _minZoom + _playerSpeed * _zoomMultiplier;
        _zoom = Mathf.Clamp(_zoom, _minZoom, _maxZoom);
        _cam.orthographicSize = Mathf.SmoothDamp(_cam.orthographicSize, _zoom, ref _velCam, _smoothTime);
    }
}
