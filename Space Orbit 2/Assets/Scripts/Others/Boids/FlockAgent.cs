using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    private Flock _agentFlock;
    public Flock agentFlock { get { return _agentFlock; } }

    private Collider2D _agentCollider;
    public Collider2D AgentCollider { get { return _agentCollider; } }

    void Start()
    {
        _agentCollider = GetComponent<Collider2D>();
    }

    public void Init(Flock flock)
    {
        _agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
