using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlanetSettings))]
[RequireComponent(typeof(OrbitProvider))]
public class AsteroidSpawner : MonoBehaviour {
    [SerializeField] private GameObject asteroidPrefab;
    private PlanetSettings _planetSettings;
    private OrbitProvider _orbitProvider;

    [SerializeField] private int maxNumberOfAsteroids = 10;
    [SerializeField] private int minNumberOfAsteroids = 0;
    [SerializeField] private float maxAsteroidSize = 10;
    [SerializeField] private float minAsteroidSize = 1;

    private void Awake() {
        _planetSettings = GetComponent<PlanetSettings>();
        _orbitProvider = GetComponent<OrbitProvider>();
    }

    public void SpawnAsteroidsInOrbit() {
        float planetBodyDiameter = _planetSettings.GetBodySize();
        float gravityFieldDiameter = _planetSettings.GetGravityFieldSize();

        float planetBodyRadius = planetBodyDiameter / 2;
        float gravityFieldRadius = gravityFieldDiameter / 2;

        int numberOfAsteroidsToSpawn = GenerateNumberOfAsteroidsToSpawn(planetBodyRadius, gravityFieldRadius);

        for (int i = 0; i < numberOfAsteroidsToSpawn; i++) {
            SpawnAsteroidAtRandomPosition(planetBodyRadius, gravityFieldRadius);
        }
    }

    private int GenerateNumberOfAsteroidsToSpawn(float planetBodyRadius, float gravityFieldRadius) {
        int randomAsteroidNumber =
            Random.Range(minNumberOfAsteroids, maxNumberOfAsteroids);

        float distanceFromPlanetSurfaceToGravityFieldEdge = gravityFieldRadius - planetBodyRadius;

        int numberOfFullSizeAsteroidsThatCanFitInGravityField =
            Mathf.FloorToInt(distanceFromPlanetSurfaceToGravityFieldEdge / maxAsteroidSize);

        int numberOfAsteroidsToSpawn = Mathf.Clamp(randomAsteroidNumber, minNumberOfAsteroids,
            numberOfFullSizeAsteroidsThatCanFitInGravityField);
        return numberOfAsteroidsToSpawn;
    }

    private void SpawnAsteroidAtRandomPosition(float planetBodyRadius, float gravityFieldRadius) {
        //spawn at random position
        Vector3 spawnPos = GetRandomPosInGravityField(planetBodyRadius, gravityFieldRadius);
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        //randomize its size
        float asteroidLocalScaleValue = Random.Range(minAsteroidSize, maxAsteroidSize);
        asteroid.transform.localScale =
            new Vector3(asteroidLocalScaleValue, asteroidLocalScaleValue, asteroidLocalScaleValue);

        //set in orbit
        _orbitProvider.AddObjectInOrbit(asteroid);
    }

    private Vector3 GetRandomPosInGravityField(float planetBodyRadius, float gravityFieldRadius) {
        float minDistanceFromPlanetCenter = planetBodyRadius + maxAsteroidSize;
        float maxDistanceFromPlanetCenter = gravityFieldRadius;
        float randomDistanceFromPlanetCenter = Random.Range(minDistanceFromPlanetCenter, maxDistanceFromPlanetCenter);
        Vector3 directionVectorFromPlanetCenter =
            new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        Vector3 positionInRelationToPlanetCenter = directionVectorFromPlanetCenter * randomDistanceFromPlanetCenter;
        Vector3 worldPosition = transform.position + positionInRelationToPlanetCenter;
        return worldPosition;
    }
}