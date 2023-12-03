using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private FollowType _currentFollowType;

    private Camera _mainCamera;
    private Vector3 _offset;
    [SerializeField] private float _baseZoomLevel = 40;

    StateMachine _stateMachine = new StateMachine();

    Dictionary<FollowType, BaseState> _availableStates = new Dictionary<FollowType, BaseState>();
    
    public float CurrentZoomLevelTarget { get; set; }

    private void Start()
    {
        _mainCamera = Camera.main;
        _offset = transform.position;

        _mainCamera.orthographicSize = _baseZoomLevel;
        CurrentZoomLevelTarget = _baseZoomLevel;

        target = GameObject.FindGameObjectWithTag("Player");

        // Initialize states
        _availableStates.Add(FollowType.PlayerShip, new CameraFollowPlayerState(target.GetComponent<BasicMovement>(), _mainCamera));
        _availableStates.Add(FollowType.Planet, new CameraFollowPlanetState(_mainCamera));

        _stateMachine.ChangeState(_availableStates[FollowType.PlayerShip]);
    }

    void Update()
    {
        _stateMachine.ExecuteState();
    }

    public void ChangeTarget(GameObject newTarget, FollowType type)
    {
        target = newTarget;
        _currentFollowType = type;

        if(type == FollowType.PlayerShip)
        {
            _stateMachine.ChangeState(_availableStates[FollowType.PlayerShip]);
        }
        else if(type == FollowType.Planet)
        {
            CameraFollowPlanetState tempSettings = _availableStates[FollowType.Planet] as CameraFollowPlanetState;
            tempSettings.Planet = newTarget.GetComponent<PlanetSettings>();
            _stateMachine.ChangeState(_availableStates[FollowType.Planet]);
        }
    } 

    public void ResetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        transform.position = target.transform.position + _offset;
    }

    public enum FollowType
    {
        PlayerShip,
        Planet
    }
}
