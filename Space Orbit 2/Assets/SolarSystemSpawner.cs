using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//spawns planets in an orbit inside of the solar system
[RequireComponent(typeof(PlanetSpawner))]
public class SolarSystemSpawner : MonoBehaviour {
    [SerializeField] private GameObject sunOrbitField;
    [SerializeField] private GameObject orbitCenterPrefab;
    private PlanetSpawner _planetSpawner;

    private float _orbitFieldSizeMin = 1;
    private float _orbitFieldSizeMax = 5;
    [SerializeField] private float planetOrbitSpeed = 5;

    private void Awake() {
        _planetSpawner = GetComponent<PlanetSpawner>();
    }

    public void SpawnSolarSystem(float minOrbitFieldSize, float maxOrbitFieldSize) {
        _orbitFieldSizeMin = minOrbitFieldSize;
        _orbitFieldSizeMax = maxOrbitFieldSize;
        RandomizeOrbitFieldSize();
        SpawnPlanetsInOrbit();
    }

    private void RandomizeOrbitFieldSize() {
        float randomOrbitFieldSize = Random.Range(_orbitFieldSizeMin, _orbitFieldSizeMax);
        Vector3 orbitFieldSizeVector = Vector3.one * randomOrbitFieldSize;
        sunOrbitField.transform.localScale = orbitFieldSizeVector;
    }

    //spawn all planets and set them in orbit
    private void SpawnPlanetsInOrbit() {
        float minSpawnDistanceFromSunCenter = transform.localScale.magnitude;
        float maxSpawnDistanceFromSunCenter = sunOrbitField.transform.localScale.magnitude;
        float spawnAreaThickness = maxSpawnDistanceFromSunCenter - minSpawnDistanceFromSunCenter;
        float maxPlanetRadiusWithGravityField = _planetSpawner.GetMaxRadiusPlanetPlusGravity();
        float maxPlanetDiameterWithGravityField = maxPlanetRadiusWithGravityField * 2;
        int numberOfPlanetsThatFit = Mathf.FloorToInt(spawnAreaThickness / maxPlanetDiameterWithGravityField);
        int randomNumberOfPlanetsToSpawn = Random.Range(numberOfPlanetsThatFit / 2, numberOfPlanetsThatFit);

        for (int i = 0; i < randomNumberOfPlanetsToSpawn; i++) {
            SpawnPlanetInOrbit(minSpawnDistanceFromSunCenter, maxSpawnDistanceFromSunCenter);
        }
    }

    //spawn planet at random position in the orbiting field
    private void SpawnPlanetInOrbit(float minSpawnDistanceFromSunCenter, float maxSpawnDistanceFromSunCenter) {
        var randomSpawnPosWorld =
            GetRandomSpawnPosInOrbitField(minSpawnDistanceFromSunCenter, maxSpawnDistanceFromSunCenter);
        GameObject spawnedPlanet = _planetSpawner.SpawnPlanet(randomSpawnPosWorld);
        StartPlanetOrbiting(spawnedPlanet);
    }

    private void StartPlanetOrbiting(GameObject spawnedPlanet) {
        GameObject spawnedOrbitCenter =
            GameObject.Instantiate(orbitCenterPrefab, transform.position, Quaternion.identity, transform);
        spawnedPlanet.transform.parent = spawnedOrbitCenter.transform;
        spawnedOrbitCenter.GetComponent<RotateContinuously>().angularVelocity = planetOrbitSpeed;
    }

    private Vector2
        GetRandomSpawnPosInOrbitField(float minSpawnDistanceFromSunCenter, float maxSpawnDistanceFromSunCenter) {
        float randomSpawnDistanceFromSunCenter =
            Random.Range(minSpawnDistanceFromSunCenter, maxSpawnDistanceFromSunCenter);
        float randomXValue = Random.Range(-1f, 1f);
        float randomYValue = Random.Range(-1f, 1f);
        Vector2 randomVec2 = new Vector2(randomXValue, randomYValue);
        Vector2 randomVec2Normalized = randomVec2.normalized;
        Vector2 randomVec2FromSunToPlanet = randomVec2Normalized * randomSpawnDistanceFromSunCenter;
        Vector2 randomSpawnPosWorld = (Vector2)transform.position + randomVec2FromSunToPlanet;
        return randomSpawnPosWorld;
    }
}