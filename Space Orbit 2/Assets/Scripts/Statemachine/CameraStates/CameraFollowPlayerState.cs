using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayerState : BaseState {
    public BasicMovement Player { get; private set; }
    public Camera Camera { get; private set; }

    private float _cameraSizetarget = 40;
    private float _startOrthoSize;

    private float _transitionTime = 1;
    private Vector3 _startPosition;

    public CameraFollowPlayerState(BasicMovement player, Camera camera) : base() {
        Player = player;
        Camera = camera;
    }

    public override void Enter() {
        _transitionTime = 0;
        _startOrthoSize = Camera.orthographicSize;
        _startPosition = Camera.transform.position;
    }

    public override void Execute() {
        if (Player) {
            if (_transitionTime < 1) {
                TransitionToPlayer();
                _transitionTime += Time.deltaTime / 1.2f;
            }
            else {
                FollowPlayer();
            }
        }
    }

    public override void Exit() {
    }

    private void TransitionToPlayer() {
        Camera.transform.position =
            Vector3.Lerp(_startPosition, LerpPosition(_transitionTime), EaseInOutQuad(_transitionTime));
        Camera.orthographicSize = Mathf.Lerp(_startOrthoSize, _cameraSizetarget, EaseInOutQuad(_transitionTime));
    }

    private void FollowPlayer() {
        Camera.transform.position = LerpPosition();
    }

    private Vector3 LerpPosition() {
        return Vector3.Lerp(Camera.transform.position,
            Player.transform.position + (Vector3)Player.rb.velocity.normalized * Mathf.Pow(Player.currentSpeed, 0.98f),
            Time.deltaTime * 2);
    }

    private Vector3 LerpPosition(float transitionTime) {
        return Vector3.Lerp(Camera.transform.position,
            Player.transform.position + (Vector3)Player.rb.velocity.normalized * Mathf.Pow(Player.currentSpeed, 0.98f),
            1 - transitionTime);
    }

    float EaseInOutQuad(float x) {
        return x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
    }
}