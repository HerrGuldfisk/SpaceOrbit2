using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;

    [Range(5f, 500f)]
    public int startCount = 20;
    const float AgentDensity = 0.8f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    private float _squareMaxSpeed;
    private float _squareNeighbourRadius;
    private float _squareAvoidanceRadiusMultiplier;
    public float squareNeighbourRadius { get { return _squareNeighbourRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        _squareMaxSpeed = maxSpeed * maxSpeed;
        _squareNeighbourRadius = neighbourRadius * neighbourRadius;
        _squareAvoidanceRadiusMultiplier = _squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        
        for (int i = 0; i < startCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {

            /*
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > _squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
            */
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);
        foreach(Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
