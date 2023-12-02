using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OthersSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _snekPrefab;
    [SerializeField, Range(0.1f, 1000f)] private float _snekMoveSpeed = 20f;
    [SerializeField, Range(0f, 10f)] private float _snekRotSpeed = 3f;

    [SerializeField] private GameObject _bigSnekPrefab;
    [SerializeField, Range(0.1f, 1000f)] private float _bigSnekMoveSpeed = 5f;
    [SerializeField, Range(0f, 10f)] private float _bigSnekRotSpeed = 1f;

    [SerializeField] private GameObject _flockPrefab;

    private Dictionary<string, FlockAgent> _typesOfOthers;

    private void Start()
    {
        _typesOfOthers = new Dictionary<string, FlockAgent>()
        {
            ["Snek"] = _snekPrefab.GetComponent<FlockAgent>()
        };
     }

    public GameObject SpawnSnek(Vector2 worldPos)
    { 
        GameObject snekInstance = Instantiate(_snekPrefab, worldPos, Quaternion.identity);

        // Perhaps set init state or something similar here

        return snekInstance; 
    }

    public GameObject SpawnBigSnek(Vector2 worldPos) 
    {
        GameObject bigSnekInstance = Instantiate(_bigSnekPrefab, worldPos, Quaternion.identity);

        // Perhaps set init state or something similar here

        return bigSnekInstance;
    }

    public GameObject SpawnFlock(Vector2 worldPos, string typeOfOthers, int flockSize)
    {
        GameObject flockInstance = Instantiate(_flockPrefab, worldPos, Quaternion.identity);
        Flock flock = flockInstance.GetComponent<Flock>();

        // Set type of flock
        flock.agentPrefab = _typesOfOthers[typeOfOthers];

        // Set flock size
        flock.startCount = flockSize;

        return flockInstance;
    }

    public GameObject SpawnFlock(Vector2 worldPos, string typeOfOthers, int flockSize, float maxSpeed)
    {
        GameObject flockInstance = Instantiate(_flockPrefab, worldPos, Quaternion.identity);

        // Set type of flock

        // Set flock size

        // Set max speed

        return flockInstance;
    }

    public GameObject SpawnFlock(Vector2 worldPos, string typeOfOthers, int flockSize, int driveFactor, float maxSpeed, float neighbourRadius, float avoidanceRadiusMultiplier) 
    {
        GameObject flockInstance = Instantiate(_flockPrefab, worldPos, Quaternion.identity);

        // Set type of flock

        // Set flock size

        // Set drive factor (reactivity kind of)

        // Set max speed

        // Set neighbour radius

        // Set avoidance radius multiplier

        return flockInstance;
    }
}
