using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbsBehaviour2 : MonoBehaviour
{
    public int segmentAmount = 6;
    [HideInInspector]
    public LineRenderer lineRenderer;
    [HideInInspector]
    public Vector3[] segmentPositions;
    private Vector3[] segmentVel;

    public GameObject limbSegmentPrefab;

    [HideInInspector]
    public Transform targetDir;
    public float targetDist = 3;
    public float smoothSpeed = 0.005f;

    [HideInInspector]
    public Transform endSegment;
    public Transform[] segmentObjects;

    [HideInInspector]
    public Flock flock;

    private BaseState _currentState;

    StateMachine _stateMachine = new StateMachine();

    Dictionary<SegmentState, BaseState> _availableStates = new Dictionary<SegmentState, BaseState>();

    public enum SegmentState
    {
        Tail,
        Boid,
        Chase
    }

    void Start()
    {
        targetDir = InitTargetDir();

        lineRenderer = GetComponent<LineRenderer>();

        InitSegments();

        InitFlock();

        _availableStates.Add(SegmentState.Tail, new SnekTailState(targetDir, targetDist, smoothSpeed, segmentPositions, segmentVel, segmentAmount, lineRenderer, segmentObjects, endSegment));

        _stateMachine.ChangeState(_availableStates[SegmentState.Tail]);
        _currentState = _stateMachine.CurrentState;
    }

    void Update()
    {
        _stateMachine.ExecuteState();
    }

    private Transform InitTargetDir()
    {
        GameObject targetDirObj = new GameObject("TargetDir");
        targetDirObj.transform.Rotate(Vector3.forward, 180f);
        targetDirObj.transform.SetParent(transform, false);

        return targetDirObj.transform;
    }

    private void InitSegments()
    {
        segmentObjects = new Transform[segmentAmount];

        for(int i = 0; i < segmentObjects.Length; i++) 
        {
            segmentObjects[i] = Instantiate(limbSegmentPrefab, gameObject.transform.parent).transform;
        }

        //segmentAmount = segmentObjects.Length + 1;

        lineRenderer.positionCount = segmentAmount;
        segmentPositions = new Vector3[segmentAmount];
        segmentVel = new Vector3[segmentAmount];
    }

    private void InitFlock()
    {
        // Is this a way to inherit flock to SnekFlockState????? how?
        //flock = new Flock();

    }
}
