using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PlanetSettings))]
public class AsteroidSpawner : MonoBehaviour {
    [SerializeField] private GameObject asteroidPrefab;
    private PlanetSettings _planetSettings;

    private void Awake() {
        _planetSettings = GetComponent<PlanetSettings>();
    }

    public void SpawnAsteroidsInOrbit() {
        int numberOfAsteroids = Random.Range(0, 10); //should number depend on gravity field size???
        float bodySize = _planetSettings.GetBodySize();
        float gravityFieldSize = _planetSettings.GetGravityFieldSize();
        for (int i = 0; i < numberOfAsteroids; i++) {
            SpawnAsteroidAtRandomPosition(bodySize, gravityFieldSize);
        }
    }

    private void SpawnAsteroidAtRandomPosition(float bodySize, float gravityFieldSize) {
        //spawn at random position
        Vector3 spawnPos = GetRandomPosInGravityField(bodySize, gravityFieldSize);
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        //randomize its size
        float localScaleValue = Random.Range(1, 10); // depend on gravity field size?
        asteroid.transform.localScale = new Vector3(localScaleValue, localScaleValue, localScaleValue);

        //++set in orbit
    }

    private Vector3 GetRandomPosInGravityField(float bodySize, float gravityFieldSize) {
        float distanceFromPlanetCenter = Random.Range(bodySize/2, gravityFieldSize/2);
        Vector3 directionVectorFromPlanetCenter = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        Vector3 positionInRelationToPlanetCenter = directionVectorFromPlanetCenter * distanceFromPlanetCenter;
        Vector3 worldPosition = transform.position + positionInRelationToPlanetCenter;
        return worldPosition;
        //problem here is that asteroids can be RIGHT on the edge. should at least the distance of the biggest possible asteroid size
    }
}