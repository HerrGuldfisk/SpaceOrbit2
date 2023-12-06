using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpawnedObjectType {
    Planet,
    Star,
    Sun
}

// randomly places a set amount of planets, stars, and suns around the world
[RequireComponent(typeof(PlanetSpawner))]
[RequireComponent(typeof(SunSpawner))]
[RequireComponent (typeof(OthersSpawner))]
public class WorldGenerator : MonoBehaviour {
    [SerializeField] private GameObject starPrefab;

    private PlanetSpawner _planetSpawner;
    private SunSpawner _sunSpawner;
    private OthersSpawner _othersSpawner;

    [SerializeField] private int numberOfPlanets = 30;
    [SerializeField] private int numberOfStars = 5;
    [SerializeField] private int numberOfSuns = 4;

    [SerializeField] private int mapSize = 1000;
    [SerializeField] private float minGenerationDistanceFromOrigin = 50f;
    [SerializeField] private float minDistanceBetweenObjects = 50f;

    private readonly List<Vector2> _positionsPlacedAt = new List<Vector2>();

    private readonly List<GameObject> _suns = new List<GameObject>();

    private void Awake() {
        _planetSpawner = GetComponent<PlanetSpawner>();
        _sunSpawner = GetComponent<SunSpawner>();
        _othersSpawner = GetComponent<OthersSpawner>();
    }

    void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        SpawnAllStars();
        SpawnAllPlanets();
        SpawnAllSuns();
        SpawnAllOthers();
    }

    private void SpawnAllStars() {
        for (int i = 0; i < numberOfStars; i++) {
            var spawnPosition = GetValidSpawnPosition(SpawnedObjectType.Star);
            Instantiate(starPrefab, spawnPosition, Quaternion.identity);
            _positionsPlacedAt.Add(spawnPosition);
        }
    }

    private void SpawnAllPlanets() {
        for (int i = 0; i < numberOfPlanets; i++) {
            var spawnPosition = GetValidSpawnPosition(SpawnedObjectType.Planet);
            _planetSpawner.SpawnPlanet(spawnPosition);
            _positionsPlacedAt.Add(spawnPosition);
        }
    }

    private void SpawnAllOthers()
    {
        SpawnSnekFlocks();
    }

    private void SpawnSnekFlocks()
    {
        foreach (GameObject sun in _suns)
        {
            int ranNumFlocks = Random.Range(0, 3);
            while(ranNumFlocks > 0)
            {
                int ranSneksInFlock = Random.Range(5, 15);
                _othersSpawner.SpawnFlock(sun.transform.position, "Snek", ranSneksInFlock);
                ranNumFlocks--;
            }
        }
    }

    private void SpawnAllSuns() {
        for (int i = 0; i < numberOfSuns; i++) {
            GameObject sunInstance = _sunSpawner.Spawn(Vector2.zero);
            Vector2 freeSunPosition = GetValidSpawnPosition(SpawnedObjectType.Sun, sunInstance);
            sunInstance.transform.position = freeSunPosition;
            _suns.Add(sunInstance);
        }
    }

    private Vector2 GetValidSpawnPosition(SpawnedObjectType type, GameObject spawnedObject = null) {
        Vector2 randomPos = GetRandomPosition(minGenerationDistanceFromOrigin, mapSize);
        int iterator = 0;
        while (PosIsTooCloseToExistingObject(randomPos, type, spawnedObject)) {
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

    bool PosIsTooCloseToExistingObject(Vector2 positionToCheck, SpawnedObjectType type, GameObject spawnedObject = null) {
        bool isTooClose = false;

        if (type == SpawnedObjectType.Planet) {
            float maxPlanetSize = GetComponent<PlanetSpawner>().GetMaxRadiusPlanetPlusGravity();
            float effectiveMinDistanceBetweenPoints = minDistanceBetweenObjects + maxPlanetSize;
            foreach (Vector2 positionPlaced in _positionsPlacedAt) {
                float distanceBetweenNewAndOldPosition = Vector2.Distance(positionToCheck, positionPlaced);
                if (distanceBetweenNewAndOldPosition < effectiveMinDistanceBetweenPoints) {
                    isTooClose = true;
                    break;
                }
            }
        }
        else if (type == SpawnedObjectType.Sun) {
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
        }

        return isTooClose;
    }
}