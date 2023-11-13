using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// randomly places a set amount of planets and stars around the world
[RequireComponent(typeof(PlanetSpawner))]
public class WorldGenerator : MonoBehaviour {
    [SerializeField] private GameObject starPrefab;

    private PlanetSpawner _planetSpawner;

    [SerializeField] private int numberOfPlanets = 30;
    [SerializeField] private int numberOfStars = 5;

    [SerializeField] private float mapSize = 1000f;
    [SerializeField] private float minGenerationDistanceFromOrigin = 50f;
    [SerializeField] private float minDistanceBetweenObjects = 50f;

    private readonly List<Vector2> _positionsPlacedAt = new List<Vector2>();

    private void Awake() {
        _planetSpawner = GetComponent<PlanetSpawner>();
    }

    void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        SpawnAllStars();
        SpawnAllPlanets();
    }

    private void SpawnAllStars() {
        for (int i = 0; i < numberOfStars; i++) {
            var spawnPosition = GetValidSpawnPosition();
            Instantiate(starPrefab, spawnPosition, Quaternion.identity);
            _positionsPlacedAt.Add(spawnPosition);
        }
    }

    private void SpawnAllPlanets() {
        for (int i = 0; i < numberOfPlanets; i++) {
            var spawnPosition = GetValidSpawnPosition();
            _planetSpawner.SpawnPlanet(spawnPosition);
            _positionsPlacedAt.Add(spawnPosition);
        }
    }

    private Vector2 GetValidSpawnPosition() {
        Vector2 randomPos = GetRandomPosition(minGenerationDistanceFromOrigin, mapSize);
        int iterator = 0;
        while (PosIsTooCloseToExistingObject(randomPos)) {
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

    bool PosIsTooCloseToExistingObject(Vector2 positionToCheck) {
        bool isTooClose = false;

        foreach (Vector2 positionPlaced in _positionsPlacedAt) {
            float distanceBetweenNewAndOldPosition = Vector2.Distance(positionToCheck, positionPlaced);
            if (distanceBetweenNewAndOldPosition < minDistanceBetweenObjects) {
                isTooClose = true;
                break;
            }
        }

        return isTooClose;
    }
}