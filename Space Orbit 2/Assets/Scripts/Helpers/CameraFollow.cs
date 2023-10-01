using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private Camera _mainCamera;
    private Vector3 _offset;
    [SerializeField] const float _BASEZOOMLEVEL = 8;
    
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

        _mainCamera.orthographicSize = _BASEZOOMLEVEL;
        CurrentZoomLevelTarget = _BASEZOOMLEVEL;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, target.transform.position + _offset, Time.deltaTime);
        _mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, CurrentZoomLevelTarget, Time.deltaTime);
        
    }

    public void ChangeTarget(GameObject newTarget, FollowType type)
    {
        target = newTarget;

        if(type == FollowType.PlayerShip)
        {
            CurrentZoomLevelTarget = _BASEZOOMLEVEL;
        }
        else if(type == FollowType.Planet)
        {
            CurrentZoomLevelTarget = target.GetComponent<PlanetSettings>().BodySize;
        }
    }


    public enum FollowType
    {
        PlayerShip,
        Planet
    }
}
