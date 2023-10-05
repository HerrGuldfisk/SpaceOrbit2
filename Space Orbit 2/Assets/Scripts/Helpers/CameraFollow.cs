using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private Camera _mainCamera;
    private Vector3 _offset;
    [SerializeField] private float _baseZoomLevel = 40;
    
    public float CurrentZoomLevelTarget { get; set; }

    public static CameraFollow Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        _mainCamera = Camera.main;
        _offset = transform.position;

        _mainCamera.orthographicSize = _baseZoomLevel;
        CurrentZoomLevelTarget = _baseZoomLevel;
    }

    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, target.transform.position + _offset, Time.deltaTime);
        _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, CurrentZoomLevelTarget, Time.deltaTime * 2);
    }

    public void ChangeTarget(GameObject newTarget, FollowType type)
    {
        target = newTarget;

        if(type == FollowType.PlayerShip)
        {
            CurrentZoomLevelTarget = _baseZoomLevel;
        }
        else if(type == FollowType.Planet)
        {
            CurrentZoomLevelTarget = target.GetComponent<PlanetSettings>().GravityFieldSize;
        }
    }


    public enum FollowType
    {
        PlayerShip,
        Planet
    }
}
