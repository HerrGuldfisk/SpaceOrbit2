using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekTailState : BaseState
{
    public override void Enter()
    {
        ResetPos();
    }

    public override void Execute()
    {
        if (targetDir != null)
        {
            segmentPositions[0] = targetDir.position;
        }

        for (int i = 1; i < segmentAmount; i++)
        {
            Vector3 targetPos = segmentPositions[i - 1] + (segmentPositions[i] - segmentPositions[i - 1]).normalized * targetDist;
            segmentPositions[i] = Vector3.SmoothDamp(segmentPositions[i], targetPos, ref segmentVel[i], smoothSpeed);
            segmentObjects[i - 1].transform.position = segmentPositions[i];
        }

        lineRenderer.SetPositions(segmentPositions);

        if (endSegment != null)
        {
            endSegment.position = segmentPositions[segmentPositions.Length - 1];
        }
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    private void ResetPos()
    {
        if (targetDir != null)
        {
            segmentPositions[0] = targetDir.position;
        }

        for (int i = 1; i < segmentAmount; i++)
        {
            segmentPositions[i] = segmentPositions[i - 1] + targetDir.right * targetDist;
        }

        lineRenderer.SetPositions(segmentPositions);
    }
}
