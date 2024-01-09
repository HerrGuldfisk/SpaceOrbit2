using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekChasingState : BaseState
{
    [SerializeField]
    private float _rotationSpeed = 3;

    [SerializeField]
    private float _moveSpeed = 20;

    private Vector2 _direction;

    public GameObject Target { get; set; }

    public GameObject Snek { get; private set; }

    public SnekChasingState(GameObject target, GameObject snek) : base()
    {
        Target = target;
        Snek = snek;
    }

    public override void Enter()
    {
        if (Target == null)
        {
            Exit();
        }

        Target = Snek.GetComponent<SnekBehaviour>()._target;
    }

    public override void Execute()
    {
        _direction = Target.transform.position - Snek.transform.position;

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Snek.transform.rotation = Quaternion.Slerp(Snek.transform.rotation, rotation, _rotationSpeed * Time.deltaTime);

        Snek.transform.position = Vector2.MoveTowards(Snek.transform.position, Target.transform.position, _moveSpeed * Time.deltaTime);
    }

    public override void Exit()
    {

    }
}
