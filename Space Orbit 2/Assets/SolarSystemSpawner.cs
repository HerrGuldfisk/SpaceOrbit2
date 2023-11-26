using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

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
        //calc the spawn area
        float sunRadius = transform.lossyScale.x / 2;
        float sunOrbitFieldRadius = sunOrbitField.transform.lossyScale.x / 2;
        float spawnAreaThickness = sunOrbitFieldRadius - sunRadius;

        //spawn the planets
        float totalWidthOfSpawnedPlanets = 0;
        while (totalWidthOfSpawnedPlanets < spawnAreaThickness) {
            GameObject spawnedPlanet = SpawnOrbitingPlanet(sunRadius, sunOrbitFieldRadius);
            float spawnedPlanetFieldDiameter = spawnedPlanet.GetComponentInChildren<PlanetGravity>().transform.lossyScale.x;
            totalWidthOfSpawnedPlanets += spawnedPlanetFieldDiameter;
        }
    }

    private GameObject SpawnOrbitingPlanet(float sunRadius, float sunOrbitFieldRadius) {
        GameObject spawnedPlanet = _planetSpawner.SpawnPlanet(Vector2.one * 3000);
        Vector2 randomSpawnPosWorld = GetRandomSpawnPosInOrbitField(sunRadius, sunOrbitFieldRadius);
        spawnedPlanet.transform.position = randomSpawnPosWorld;
        StartPlanetOrbiting(spawnedPlanet);
        return spawnedPlanet;
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