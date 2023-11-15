using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Steered Cohesion")]
public class SteeredCohesionBehaviour : FilteredFlockBehaviour
{
    Vector2 currentVel;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, return no adjustment
        if (context.Count == 0)
        { return Vector2.zero; }

        //add all points together and average
        Vector2 steeredCohesionMove = Vector2.zero;

        List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            steeredCohesionMove += (Vector2)item.position;
        }

        steeredCohesionMove /= context.Count;

        //create offset from agent position
        steeredCohesionMove -= (Vector2)agent.transform.position;

        steeredCohesionMove = Vector2.SmoothDamp(agent.transform.up, steeredCohesionMove, ref currentVel, agentSmoothTime);

        return steeredCohesionMove;
    }
}
