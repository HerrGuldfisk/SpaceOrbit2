using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{
    Rigidbody2D rb;
    LineRenderer lr;

    void Start()
    {
        if (GetComponent<LineRenderer>())
        {
            lr = GetComponent<LineRenderer>();
        }

        if (lr == null)
        {
            lr = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        }

        if (GetComponent<Rigidbody2D>())
        {
            rb = GetComponent<Rigidbody2D>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        Vector2[] trajectory = Plot(rb, (Vector2)transform.position, rb.velocity, 500);

        lr.positionCount = trajectory.Length;

        Vector3[] positions = new Vector3[trajectory.Length];
        for (int i = 0; i < trajectory.Length; i++)
        {
            positions[i] = trajectory[i];
        }
        lr.SetPositions(positions);
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 vel, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;

        //Vector2 acceleration = vel * timestep * timestep;
        //Vector2 moveStep = vel * timestep;

        Vector2 directionVector = (Vector2)transform.forward;
        print(directionVector);
        Vector2 nextPos = pos + (Vector2)transform.forward;
        print(nextPos);

        for (int i = 0; i < steps; i++)
        {
            Vector2 calculatedPos = nextPos + directionVector * vel * i * timestep;

            results[i] = calculatedPos;
        }

        return results;
    }
}
