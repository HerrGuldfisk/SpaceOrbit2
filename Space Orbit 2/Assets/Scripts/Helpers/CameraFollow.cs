using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private FollowType _currentFollowType;

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

        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (target == null)
        {
            return;
        }

        if(_currentFollowType == FollowType.Planet)
        {
            transform.position = Vector3.Slerp(transform.position, target.transform.position + _offset, Time.deltaTime * 2);
            _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, CurrentZoomLevelTarget, Time.deltaTime * 2);
        }
        else if(_currentFollowType == FollowType.PlayerShip)
        {
            Vector3 aheadDirection = target.GetComponent<BasicMovement>().rb.velocity.normalized;
            float aheadDistance = target.GetComponent<BasicMovement>().currentSpeed / 2; 

            transform.position = Vector3.Slerp(transform.position, target.transform.position + _offset + (aheadDirection * aheadDistance), Time.deltaTime * 5);
            _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, CurrentZoomLevelTarget, Time.deltaTime * 2);
        }
    }

    public void ChangeTarget(GameObject newTarget, FollowType type)
    {
        target = newTarget;
        _currentFollowType = type;

        if(type == FollowType.PlayerShip)
        {
            CurrentZoomLevelTarget = _baseZoomLevel;
        }
        else if(type == FollowType.Planet)
        {
            CurrentZoomLevelTarget = target.GetComponent<PlanetSettings>().GravityFieldSize / 1.8f;
        }
    }

    public void ResetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }


    public enum FollowType
    {
        PlayerShip,
        Planet
    }
}
