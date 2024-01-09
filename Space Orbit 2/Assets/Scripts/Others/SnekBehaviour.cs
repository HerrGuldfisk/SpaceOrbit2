using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static LimbsBehaviour2;

public class SnekBehaviour : MonoBehaviour
{
    public GameObject _target;

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
        Return,
        Circle
    }

    // Start is called before the first frame update
    void Start()
    {
        _availableStates.Add(SnekState.Idle, new SnekIdleState());
        _availableStates.Add(SnekState.Return, new SnekReturnState());
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
    public void PlayerWasSeen(Collider2D col)
    {
        print("player is seen");
        _target = col.gameObject;
        _stateMachine.ChangeState(_availableStates[SnekState.Chase]);
    }

    public void PlayerWasLost(Vector3 lastPosition)
    {
        _target = null;
    }
}