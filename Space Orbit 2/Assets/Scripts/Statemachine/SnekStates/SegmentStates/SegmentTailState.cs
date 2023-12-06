using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentTailState : BaseState
{
    public Transform TargetDir { get; private set; }
    public float TargetDist { get; private set; }
    public float SmoothSpeed { get; private set; }
    public Vector3[] SegmentPositions { get; private set; }
    public Vector3[] SegmentVel { get; private set; }
    public int SegmentAmount { get; private set; }
    public LineRenderer LR { get; private set; }
    public Transform[] SegmentObjects { get; private set; }
    public Transform EndSegment { get; private set; }


    public SegmentTailState(Transform targetDir, float targetDist, float smoothSpeed, Vector3[] segmentPositions, Vector3[] segmentVel, int segmentAmount, LineRenderer lineRenderer, Transform[] segmentObjects, Transform endSegment) : base()
    {
        TargetDir = targetDir;
        TargetDist = targetDist;
        SmoothSpeed = smoothSpeed;
        SegmentPositions = segmentPositions;
        SegmentVel = segmentVel;
        SegmentAmount = segmentAmount;
        LR = lineRenderer;
        SegmentObjects = segmentObjects;
        EndSegment = endSegment;
    }

    public override void Enter()
    {
        ResetPos();
    }

    public override void Execute()
    {
        if (TargetDir != null)
        {
            SegmentPositions[0] = TargetDir.position;
        }

        for (int i = 1; i < SegmentAmount; i++)
        {
            Vector3 targetPos = SegmentPositions[i - 1] + (SegmentPositions[i] - SegmentPositions[i - 1]).normalized * TargetDist;
            SegmentPositions[i] = Vector3.SmoothDamp(SegmentPositions[i], targetPos, ref SegmentVel[i], SmoothSpeed);
            SegmentObjects[i - 1].transform.position = SegmentPositions[i];
        }

        LR.SetPositions(SegmentPositions);

        if (EndSegment != null)
        {
            EndSegment.position = SegmentPositions[SegmentPositions.Length - 1];
        }
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    private void ResetPos()
    {
        if (TargetDir != null)
        {
            SegmentPositions[0] = TargetDir.position;
        }

        for (int i = 1; i < SegmentAmount; i++)
        {
            SegmentPositions[i] = SegmentPositions[i - 1] + TargetDir.right * TargetDist;
        }

        LR.SetPositions(SegmentPositions);
    }
}
