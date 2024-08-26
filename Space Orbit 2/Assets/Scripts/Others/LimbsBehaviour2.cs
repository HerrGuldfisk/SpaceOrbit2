using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbsBehaviour2 : MonoBehaviour
{
    public int segmentCount = 6;
    [HideInInspector]
    public int currentSegmentCount;
    [HideInInspector]
    public int newSegmentCount;
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

    [HideInInspector]
    public BaseState _currentState;

    [HideInInspector]
    public StateMachine _stateMachine = new StateMachine();

    [HideInInspector]
    public Dictionary<SegmentState, BaseState> _availableStates = new Dictionary<SegmentState, BaseState>();

    public enum SegmentState
    {
        Tail,
        Boid
    }

    void Start()
    {
        InitLimb();
    }

    void Update()
    {
        _stateMachine.ExecuteState();
    }

    public void InitLimb()
    {
        targetDir = InitTargetDir();

        lineRenderer = GetComponent<LineRenderer>();

        InitSegments();

        InitFlock();

        _availableStates.Add(SegmentState.Tail, new SegmentTailState(targetDir, targetDist, smoothSpeed, segmentPositions, segmentVel, segmentCount, lineRenderer, segmentObjects, endSegment));

        _stateMachine.ChangeState(_availableStates[SegmentState.Tail]);
        _currentState = _stateMachine.CurrentState;
    }

    public Transform InitTargetDir()
    {
        GameObject targetDirObj = new GameObject("TargetDir");
        targetDirObj.transform.Rotate(Vector3.forward, 180f);
        targetDirObj.transform.SetParent(transform, false);

        return targetDirObj.transform;
    }

    public void InitSegments()
    {
        if(segmentCount < 1)
        { return; }

        segmentObjects = new Transform[segmentCount];

        for(int i = 0; i < segmentObjects.Length; i++) 
        {
            segmentObjects[i] = Instantiate(limbSegmentPrefab, gameObject.transform.parent).transform;
        }

        //segmentCount = segmentObjects.Length + 1;

        lineRenderer.positionCount = segmentCount;
        segmentPositions = new Vector3[segmentCount];
        segmentVel = new Vector3[segmentCount];
    }

    private void InitFlock()
    {
        // Is this a way to inherit flock to SnekFlockState????? how?
        //flock = new Flock();

    }

    public void AddSegments(int add)
    {
        currentSegmentCount = segmentCount;
        newSegmentCount = currentSegmentCount + Mathf.Abs(add);
    }
}
