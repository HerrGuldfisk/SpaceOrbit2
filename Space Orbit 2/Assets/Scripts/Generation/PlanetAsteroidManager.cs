using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlanetSettings))]
[RequireComponent(typeof(OrbitProvider))]
public class PlanetAsteroidManager : MonoBehaviour {
    [SerializeField] private GameObject asteroidPrefab;
    private PlanetSettings _planetSettings;
    private OrbitProvider _orbitProvider;
    
    [SerializeField] private float maxAsteroidVelocity = 20;
    [SerializeField] private float maxAsteroidSize = 10;
    [SerializeField] private float minAsteroidSize = 1;

    private readonly List<GameObject> _spawnedAsteroids = new List<GameObject>();
    private float _planetBodyRadius;
    private float _gravityFieldRadius;
    private float _difficultyLevel;

    private void Awake() {
        _planetSettings = GetComponent<PlanetSettings>();
        _orbitProvider = GetComponent<OrbitProvider>();
    }

    private void UpdatePlanetSizeValues() {
        _planetBodyRadius = _planetSettings.GetBodySize() / 2;
        _gravityFieldRadius = _planetSettings.GetGravityFieldSize() / 2;
    }

    public void SpawnAsteroidsInOrbit(float difficultyLevel) {
        _difficultyLevel = difficultyLevel;
        UpdatePlanetSizeValues();

        int numberOfAsteroidsToSpawn = GetNumberOfAsteroidsToSpawn();

        for (int i = 0; i < numberOfAsteroidsToSpawn; i++) {
            SpawnAsteroidAtRandomPosition();
        }
    }

    private int GetNumberOfAsteroidsToSpawn() {
        float distanceFromPlanetSurfaceToGravityFieldEdge = _gravityFieldRadius - _planetBodyRadius;
        int numberOfFullSizeAsteroidsThatCanFitInGravityField = Mathf.FloorToInt(distanceFromPlanetSurfaceToGravityFieldEdge / maxAsteroidSize);
        int numberOfAsteroidsToSpawn = (int)(numberOfFullSizeAsteroidsThatCanFitInGravityField * _difficultyLevel);
        return numberOfAsteroidsToSpawn;
    }

    private void SpawnAsteroidAtRandomPosition() {
        Vector3 spawnPos = GetRandomPosInGravityField();
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
        float asteroidLocalScaleValue = Random.Range(minAsteroidSize, maxAsteroidSize);
        asteroid.transform.localScale = new Vector3(asteroidLocalScaleValue, asteroidLocalScaleValue, asteroidLocalScaleValue);
        float asteroidVelocity = maxAsteroidVelocity * _difficultyLevel;
        _orbitProvider.AddObjectInOrbit(asteroid, asteroidVelocity);
        _spawnedAsteroids.Add(asteroid);
    }

    private Vector3 GetRandomPosInGravityField() {
        float minDistanceFromPlanetCenter = _planetBodyRadius + maxAsteroidSize;
        float maxDistanceFromPlanetCenter = _gravityFieldRadius;
        float randomDistanceFromPlanetCenter = Random.Range(minDistanceFromPlanetCenter, maxDistanceFromPlanetCenter);
        Vector3 directionVectorFromPlanetCenter =
            new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        Vector3 positionInRelationToPlanetCenter = directionVectorFromPlanetCenter * randomDistanceFromPlanetCenter;
        Vector3 worldPosition = transform.position + positionInRelationToPlanetCenter;
        return worldPosition;
    }

    private void ClearAllAsteroids() {
        foreach (var asteroid in _spawnedAsteroids) {
            Destroy(asteroid);
        }
    }

    public void RespawnAsteroidsByDifficulty(float difficultyLevel) {
        ClearAllAsteroids();
        SpawnAsteroidsInOrbit(difficultyLevel);
    }
}