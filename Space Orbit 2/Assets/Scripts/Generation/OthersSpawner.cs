using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OthersSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _snekPrefab;
    [SerializeField] private GameObject _bigSnekPrefab;
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
        Flock flock = flockInstance.GetComponent<Flock>();

        // Set type of flock
        flock.agentPrefab = _typesOfOthers[typeOfOthers];

        // Set flock size
        flock.startCount = flockSize;

        // Set max speed
        flock.maxSpeed = maxSpeed;

        return flockInstance;
    }

    public GameObject SpawnFlock(Vector2 worldPos, string typeOfOthers, int flockSize, int driveFactor, float maxSpeed, float neighbourRadius, float avoidanceRadiusMultiplier) 
    {
        GameObject flockInstance = Instantiate(_flockPrefab, worldPos, Quaternion.identity);
        Flock flock = flockInstance.GetComponent<Flock>();

        // Set type of flock
        flock.agentPrefab = _typesOfOthers[typeOfOthers];

        // Set flock size
        flock.startCount = flockSize;

        // Set drive factor (reactivity kind of)
        flock.driveFactor = driveFactor;

        // Set max speed
        flock.maxSpeed = maxSpeed;

        // Set neighbour radius
        flock.neighbourRadius = neighbourRadius;

        // Set avoidance radius multiplier
        flock.avoidanceRadiusMultiplier = avoidanceRadiusMultiplier;

        return flockInstance;
    }
}
