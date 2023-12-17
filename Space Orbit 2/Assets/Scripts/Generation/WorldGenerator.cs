using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SunSpawner))]
[RequireComponent(typeof(OthersSpawner))]
public class WorldGenerator : MonoBehaviour {
    private SunSpawner _sunSpawner;
    private OthersSpawner _othersSpawner;

    [SerializeField] private int numberOfSuns = 4;
    [SerializeField] private int mapSize = 1000;
    [SerializeField] private float minGenerationDistanceFromOrigin = 50f;
    [SerializeField] private float minDistanceBetweenObjects = 50f;

    private readonly List<Vector2> _positionsPlacedAt = new List<Vector2>();

    private readonly List<GameObject> _suns = new List<GameObject>();

    private void Awake() {
        _sunSpawner = GetComponent<SunSpawner>();
        _othersSpawner = GetComponent<OthersSpawner>();
    }

    void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        SpawnAllSuns();
        SpawnAllOthers();
    }

    private void SpawnAllOthers() {
        SpawnSnekFlocks();
    }

    private void SpawnSnekFlocks() {
        foreach (GameObject sun in _suns) {
            int ranNumFlocks = Random.Range(0, 3);
            while (ranNumFlocks > 0) {
                int ranSneksInFlock = Random.Range(5, 15);
                _othersSpawner.SpawnFlock(sun.transform.position, "Snek", ranSneksInFlock);
                ranNumFlocks--;
            }
        }
    }

    private void SpawnAllSuns() {
        for (int i = 0; i < numberOfSuns; i++) {
            GameObject sunInstance = _sunSpawner.Spawn(Vector2.zero);
            Vector2 freeSunPosition = GetValidSunPosition(sunInstance);
            sunInstance.transform.position = freeSunPosition;
            _suns.Add(sunInstance);
        }
    }

    private Vector2 GetValidSunPosition(GameObject spawnedSun = null) {
        Vector2 randomPos = GetRandomPosition(minGenerationDistanceFromOrigin, mapSize);
        int iterator = 0;
        while (SunPosIsTooCloseToExistingObject(randomPos, spawnedSun)) {
            randomPos = GetRandomPosition(minGenerationDistanceFromOrigin, mapSize);
            iterator++;
            if (iterator > 30) {
                Debug.LogWarning("iterated object placement position more than 30 times. gives back origin coordinates. Please make sure that there is enough space to generate all the objects");
                break;
            }
        }

        return randomPos;
    }

    Vector2 GetRandomPosition(float minDistanceFromOrigin, float maxDistanceFromOrigin) {
        float distanceFromOrigin = Random.Range(minDistanceFromOrigin, maxDistanceFromOrigin);
        Vector2 directionFromOrigin = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Vector2 position = directionFromOrigin * distanceFromOrigin;
        return position;
    }

    bool SunPosIsTooCloseToExistingObject(Vector2 positionToCheck, GameObject spawnedObject = null) {
        bool isTooClose = false;

        Debug.Assert(spawnedObject != null, "have to provide the ref to spawned object when checking sun positions!");
        Transform[] spawnedSunTransforms = spawnedObject.GetComponentsInChildren<Transform>();
        float radiusOfSpawnedSun = spawnedSunTransforms[1].lossyScale.x / 2;
        bool orbitFieldCoversWorldOrigin = (radiusOfSpawnedSun > positionToCheck.magnitude);

        if (orbitFieldCoversWorldOrigin) {
            isTooClose = true;
        }
        else {
            foreach (GameObject sun in _suns) {
                Transform[] existingSunTransforms = sun.GetComponentsInChildren<Transform>();
                float radiusOfExistingSun = existingSunTransforms[1].lossyScale.x / 2;
                float minDistanceBetweenSuns = radiusOfExistingSun + radiusOfSpawnedSun + minDistanceBetweenObjects;
                float distanceBetweenSuns = Vector2.Distance(sun.transform.position, positionToCheck);
                if (distanceBetweenSuns < minDistanceBetweenSuns) {
                    isTooClose = true;
                    break;
                }
            }
        }

        return isTooClose;
    }
}