using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlanetState : BaseState
{
    public PlanetSettings Planet { get; set; }
    public Camera Camera { get; private set; }
    private float _startOrthoSize;
    private float _transitionTime = 1;
    private Vector3 _startPosition;

    public CameraFollowPlanetState(Camera camera) : base()
    {
        Camera = camera;
    }

    public override void Enter()
    {
        _transitionTime = 0;
        _startOrthoSize = Camera.orthographicSize;
        _startPosition = Camera.transform.position;
    }

    public override void Execute()
    {
        if (_transitionTime < 1) 
        {
            TransitionToPlanet();
            _transitionTime += Time.deltaTime / 0.8f;
        }
        else
        {
            FollowPlanet();
        }
    }

    public override void Exit()
    {
        
    }

    private void TransitionToPlanet()
    {
        if(Planet == null)
        {
            Debug.LogWarning("Planet value is null");
            return;
        }

        Camera.transform.position = Vector3.Lerp(_startPosition, Planet.transform.position, EaseInOutQuad(_transitionTime));
        Camera.orthographicSize = Mathf.Lerp(_startOrthoSize, Planet.GravityFieldSize * 0.6f, EaseInOutQuad(_transitionTime));
    }

    private void FollowPlanet()
    {
        Camera.transform.position = Planet.transform.position;
    }

    float EaseInOutQuad(float x)
    {
        return x < 0.5 ? 2 * x* x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
    }
}
