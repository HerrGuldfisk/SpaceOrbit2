using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static LimbsBehaviour2;

public class SnekBehaviour : MonoBehaviour
{
    private GameObject _target;

    private CircleCollider2D _snekVision;
    private float _snekVisionRadius = 10f;

    [HideInInspector]
    public StateMachine _stateMachine = new StateMachine();
    [HideInInspector]
    public BaseState _currentState;
    [HideInInspector]
    public Dictionary<SnekState, BaseState> _availableStates = new Dictionary<SnekState, BaseState>();
    public enum SnekState
    {
        Idle,
        Chase,
        Circle
    }

    // Start is called before the first frame update
    void Start()
    {
        _snekVision = gameObject.AddComponent<CircleCollider2D>();
        _snekVision.radius = _snekVisionRadius;

        _availableStates.Add(SnekState.Idle, new SnekIdleState());
        _availableStates.Add(SnekState.Chase, new SnekChasingState(_target, gameObject));

        _stateMachine.ChangeState(_availableStates[SnekState.Idle]);
        _currentState = _stateMachine.CurrentState;
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.ExecuteState();
    }

    // add a game object and write this into a component that is added to the new object, it will handle vision instead...
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.gameObject.GetComponent<Collider2D>();

        // Check if snekVision collider is touching something
        if (col.IsTouching(_snekVision))
        {
            // is it seeing the player?
            if (col.gameObject.CompareTag("Player"))
            {
                _target = col.gameObject;
                _stateMachine.ChangeState(_availableStates[SnekState.Chase]);
            }
        }
    }
}
