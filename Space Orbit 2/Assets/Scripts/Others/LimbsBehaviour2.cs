using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbsBehaviour2 : MonoBehaviour
{
    public int segmentAmount;
    public LineRenderer lineRenderer;
    public Vector3[] segmentPositions;
    private Vector3[] segmentVel;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    public Transform endSegment;
    public Transform[] segmentObjects;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        segmentAmount = segmentObjects.Length + 1;

        lineRenderer.positionCount = segmentAmount;
        segmentPositions = new Vector3[segmentAmount];
        segmentVel = new Vector3[segmentAmount];

        ResetPos();
    }

    void Update()
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
