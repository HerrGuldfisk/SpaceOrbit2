using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// randomly places a set amount of planets, stars, and suns around the world
[RequireComponent(typeof(PlanetSpawner))]
[RequireComponent(typeof(SunSpawner))]
public class WorldGenerator : MonoBehaviour {
    [SerializeField] private GameObject starPrefab;

    private PlanetSpawner _planetSpawner;
    private SunSpawner _sunSpawner;

    [SerializeField] private int numberOfPlanets = 30;
    [SerializeField] private int numberOfStars = 5;
    [SerializeField] private int numberOfSuns = 4;

    [SerializeField] private float mapSize = 1000f;
    [SerializeField] private float minGenerationDistanceFromOrigin = 50f;
    [SerializeField] private float minDistanceBetweenObjects = 50f;

    private readonly List<Vector2> _positionsPlacedAt = new List<Vector2>();

    private void Awake() {
        _planetSpawner = GetComponent<PlanetSpawner>();
        _sunSpawner = GetComponent<SunSpawner>();
    }

    void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        SpawnAllStars();
        SpawnAllPlanets();
        SpawnAllSuns();
    }

    private void SpawnAllStars() {
        for (int i = 0; i < numberOfStars; i++) {
            var spawnPosition = GetValidSpawnPosition(false);
            Instantiate(starPrefab, spawnPosition, Quaternion.identity);
            _positionsPlacedAt.Add(spawnPosition);
        }
    }

    private void SpawnAllPlanets() {
        for (int i = 0; i < numberOfPlanets; i++) {
            var spawnPosition = GetValidSpawnPosition(true);
            _planetSpawner.SpawnPlanet(spawnPosition);
            _positionsPlacedAt.Add(spawnPosition);
        }
    }

    private void SpawnAllSuns() {
        for (int i = 0; i < numberOfSuns; i++) {
            var spawnPosition = GetValidSpawnPosition(false);
            _sunSpawner.Spawn(spawnPosition);
            _positionsPlacedAt.Add(spawnPosition);
        }
    }

    private Vector2 GetValidSpawnPosition(bool isPlanet) {
        Vector2 randomPos = GetRandomPosition(minGenerationDistanceFromOrigin, mapSize);
        int iterator = 0;
        while (PosIsTooCloseToExistingObject(randomPos, isPlanet)) {
            randomPos = GetRandomPosition(minGenerationDistanceFromOrigin, mapSize);
            iterator++;
        }

        Debug.Assert(iterator < 30,
            "iterated object placement position more than 30 times. Please make sure that there is enough space to generate all the objects");
        return randomPos;
    }

    Vector2 GetRandomPosition(float minDistanceFromOrigin, float maxDistanceFromOrigin) {
        float distanceFromOrigin = Random.Range(minDistanceFromOrigin, maxDistanceFromOrigin);
        Vector2 directionFromOrigin = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Vector2 position = directionFromOrigin * distanceFromOrigin;
        return position;
    }

    bool PosIsTooCloseToExistingObject(Vector2 positionToCheck, bool isPlanet) {
        float effectiveMinDistanceBetweenPoints = minDistanceBetweenObjects;

        if (isPlanet) {
            float maxPlanetSize = GetComponent<PlanetSpawner>().GetMaxRadiusPlanetPlusGravity();
            effectiveMinDistanceBetweenPoints += maxPlanetSize;
        }

        bool isTooClose = false;

        foreach (Vector2 positionPlaced in _positionsPlacedAt) {
            float distanceBetweenNewAndOldPosition = Vector2.Distance(positionToCheck, positionPlaced);
            if (distanceBetweenNewAndOldPosition < effectiveMinDistanceBetweenPoints) {
                isTooClose = true;
                break;
            }
        }

        return isTooClose;
    }
}