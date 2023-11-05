using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns asteroids around the planet on awake
/// </summary>
public class PlanetAsteroidSpawner : MonoBehaviour {
    [SerializeField] private GameObject asteroidPrefab;
    private readonly float _asteroidMinSize = 3f;
    private readonly float _asteroidMaxSize = 10f;
    private float _spawnRadius;
    private OrbitProvider _orbitProvider;

    private void Awake() {
        _orbitProvider = GetComponent<OrbitProvider>();

        //randomize the number of asteroids
        int numberOfAsteroids = Random.Range(0, 10);

        //set the max spawn radius to the gravity size
        PlanetSettings planetSettings = GetComponent<PlanetSettings>();
        _spawnRadius = planetSettings.GravityFieldSize / 2;

        //spawn asteroids
        for (int i = 0; i < numberOfAsteroids; i++) {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid() {
        //randomize the spawn position
        Vector2 positionDifference = Random.insideUnitCircle * -_spawnRadius;
        Vector2 spawnPosition = (Vector2)transform.position + positionDifference;
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

        //randomize the size of the asteroid
        float asteroidSize = Random.Range(_asteroidMinSize, _asteroidMaxSize);
        asteroid.transform.localScale = new Vector3(asteroidSize, asteroidSize, 1);

        //add the asteroid to the orbit provider
        //unclear how this works. all plabets destroy when this is called
        // float distanceFromPlanet = positionDifference.magnitude;
        // _orbitProvider.AddNewOrbiting(gameObject, transform, distanceFromPlanet, 10, 0, false, true, 0);
    }
}