using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbsBehaviour : MonoBehaviour
{
    public int segmentAmount;
    public LineRenderer lineRenderer;
    public Vector3[] segmentPositions;
    private Vector3[] segmentVel;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;
    public float trailSpeed;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = segmentAmount;
        segmentPositions = new Vector3[segmentAmount];
        segmentVel = new Vector3[segmentAmount];
    }

    void Update()
    {
        segmentPositions[0] = targetDir.position;

        for (int i = 1; i < segmentPositions.Length; i++) 
        {
            // check out smooth damp function for more info...
            // Lower smooth damp value => faster
            segmentPositions[i] = Vector3.SmoothDamp(segmentPositions[i], segmentPositions[i - 1] + targetDir.right * targetDist, ref segmentVel[i], smoothSpeed + i / trailSpeed);
        }

        lineRenderer.SetPositions(segmentPositions);
    }
}
