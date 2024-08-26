using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace SolarSystem {
    //spawns planets in an orbit inside of the solar system
    [RequireComponent(typeof(PlanetSpawner))]
    [RequireComponent(typeof(SolarSystemDifficulty))]
    public class SolarSystemSpawner : MonoBehaviour {
        [SerializeField] private GameObject sunOrbitField;
        [SerializeField] private GameObject orbitCenterPrefab;
        [SerializeField] private GameObject hubPlanetPrefab;
        private PlanetSpawner _planetSpawner;
        private Transform _sunT;

        private float _orbitFieldSizeMin = 1;
        private float _orbitFieldSizeMax = 5;
        [SerializeField] private float planetOrbitSpeed = 5;

        public float _sunRadius;
        private float _sunOrbitFieldRadius;
        public float _spawnAreaThickness;

        public readonly List<GameObject> planets = new List<GameObject>();
        public readonly List<GameObject> planetOrbitCenters = new List<GameObject>();

        private void Awake() {
            _planetSpawner = GetComponent<PlanetSpawner>();
            _sunT = transform;
        }

        public void SpawnSolarSystem(float minOrbitFieldSize, float maxOrbitFieldSize) {
            _orbitFieldSizeMin = minOrbitFieldSize;
            _orbitFieldSizeMax = maxOrbitFieldSize;
            RandomizeOrbitFieldSize();
            SpawnPlanetsInOrbit();
            SpawnCollectibleOnRandomPlanet();
        }

        private void SpawnCollectibleOnRandomPlanet() {
            int randomPlanetIndex = Random.Range(0, planets.Count);
            GameObject randomPlanet = planets[randomPlanetIndex];
            randomPlanet.GetComponent<PlanetCollectibleSpawner>().SpawnCollectible();
        }

        private void RandomizeOrbitFieldSize() {
            //randomize orbit field size
            float randomOrbitFieldSize = Random.Range(_orbitFieldSizeMin, _orbitFieldSizeMax);
            Vector3 orbitFieldSizeVector = Vector3.one * randomOrbitFieldSize;
            sunOrbitField.transform.localScale = orbitFieldSizeVector;

            //save useful values
            _sunRadius = transform.lossyScale.x / 2;
            _sunOrbitFieldRadius = sunOrbitField.transform.lossyScale.x / 2;
            _spawnAreaThickness = _sunOrbitFieldRadius - _sunRadius;
        }

        private void SpawnPlanetsInOrbit() {
            //keep track of width taken up by planet fields
            float totalWidthOfSpawnedPlanets = 0;

            //spawn a hub planet
            GameObject hubPlanet = Instantiate(hubPlanetPrefab);
            PositionPlanetInSolarSystem(hubPlanet);
            StartPlanetOrbiting(hubPlanet);
            totalWidthOfSpawnedPlanets += GetPlanetFieldDiameter(hubPlanet);

            //spawn other planets
            while (totalWidthOfSpawnedPlanets < _spawnAreaThickness) {
                GameObject spawnedPlanet = _planetSpawner.SpawnPlanet(Vector2.one * 3000);
                PositionPlanetInSolarSystem(spawnedPlanet);
                StartPlanetOrbiting(spawnedPlanet);
                totalWidthOfSpawnedPlanets += GetPlanetFieldDiameter(spawnedPlanet);
                planets.Add(spawnedPlanet);
            }
        }

        private float GetPlanetFieldDiameter(GameObject planet) {
            float planetFieldDiameter = planet.GetComponentInChildren<PlanetGravity>().transform.lossyScale.x;
            return planetFieldDiameter;
        }

        private void PositionPlanetInSolarSystem(GameObject hubPlanet) {
            Vector2 randomSpawnPosWorld = GetRandomSpawnPosInOrbitField();
            hubPlanet.transform.position = randomSpawnPosWorld;
        }

        private void StartPlanetOrbiting(GameObject spawnedPlanet) {
            GameObject spawnedOrbitCenter = Instantiate(orbitCenterPrefab, transform.position, Quaternion.identity, _sunT);
            planetOrbitCenters.Add(spawnedOrbitCenter);
            spawnedPlanet.transform.parent = spawnedOrbitCenter.transform;
            float distanceFromSunCenter = Vector2.Distance(spawnedPlanet.transform.position, _sunT.position);
            spawnedOrbitCenter.GetComponent<RotateContinuously>().angularVelocity = planetOrbitSpeed * 50f / distanceFromSunCenter;
        }

        private Vector2 GetRandomSpawnPosInOrbitField() {
            float randomSpawnDistanceFromSunCenter = Random.Range(_sunRadius, _sunOrbitFieldRadius);
            float randomXValue = Random.Range(-1f, 1f);
            float randomYValue = Random.Range(-1f, 1f);
            Vector2 randomVec2 = new Vector2(randomXValue, randomYValue);
            Vector2 randomVec2Normalized = randomVec2.normalized;
            Vector2 randomVec2FromSunToPlanet = randomVec2Normalized * randomSpawnDistanceFromSunCenter;
            Vector2 randomSpawnPosWorld = (Vector2)transform.position + randomVec2FromSunToPlanet;
            return randomSpawnPosWorld;
        }
    }
}