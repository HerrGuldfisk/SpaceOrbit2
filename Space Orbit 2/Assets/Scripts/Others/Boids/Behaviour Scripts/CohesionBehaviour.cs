using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
    //Finds middle point between all our neighbours and tries to move there
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbours, return no adjustment
        if (context.Count == 0)
        { return Vector2.zero; }

        //add all points together and average
        Vector2 cohesionMove = Vector2.zero;

        List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context);
        foreach (Transform item in filteredContext) 
        {
            cohesionMove += (Vector2)item.position;
        }

        cohesionMove /= context.Count;

        //create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;

        return cohesionMove;
    }
}
