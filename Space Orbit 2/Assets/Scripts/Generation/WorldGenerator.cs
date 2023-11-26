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
public class WorldGenerator : MonoBehaviour {
    [SerializeField] private GameObject starPrefab;

    private PlanetSpawner _planetSpawner;
    private SunSpawner _sunSpawner;

    [SerializeField] private int numberOfPlanets = 30;
    [SerializeField] private int numberOfStars = 5;
    [SerializeField] private int numberOfSuns = 4;

    [SerializeField] private int mapSize = 1000;
    [SerializeField] private float minGenerationDistanceFromOrigin = 50f;
    [SerializeField] private float minDistanceBetweenObjects = 50f;

    private readonly List<Vector2> _positionsPlacedAt = new List<Vector2>();
    private readonly List<Vector2> _sunPositions = new List<Vector2>();

    private GridMap _worldGridMap;

    private void Awake() {
        _planetSpawner = GetComponent<PlanetSpawner>();
        _sunSpawner = GetComponent<SunSpawner>();
        _worldGridMap = new GridMap(mapSize, mapSize);
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

    private void SpawnAllSuns() {
        /*
         ok so there are 2 other possible strats here
         we can save the exact positoin and sizes of the suns we have generated, and use that to "calculate" what positions are free to place a sun at
         in this case, it would also be good to know the size of the sun that we are generating, so that we can determine the real possible distance
         and then, we just get a random value out of those that are possible.
         this is an interesting idea, so i will see how it coul be done
         */

        //also, we DONT want the suns orbit field to be on top of the player
        //so we can only generate it outside of that sphere

        //probably, the world generation area might have to be expanded for this

        //or, the size of the world generation area can determine the max size of the suns, so that its populated correctly.

        for (int i = 0; i < numberOfSuns; i++) {
            // var spawnPosition = GetValidSpawnPosition(SpawnedObjectType.Sun); //++replace by a function that utilizes the gridmap
            Vector2 spawnPosition = _worldGridMap.GetRandomCellWithDistanceFromOccupiedZones(100); //the number we put in here should be the radius of the sun
            GameObject sunInstance = _sunSpawner.Spawn(spawnPosition); //should not spawn at a position, that will be determined later.
            // _positionsPlacedAt.Add(spawnPosition); //++replace with gridmap

            // use the sun 

            //set grid cells to occupied
            Transform sunFieldTransform = sunInstance.GetComponentsInChildren<Transform>()[1]; //a bit shaky if we modify the sun prefab
            Vector2 sunPosition = sunFieldTransform.position;
            float sunRadius = sunFieldTransform.lossyScale.x / 2;
            _worldGridMap.OccupyCellsInCircle(_worldGridMap.grid, (int)sunPosition.x, (int)sunPosition.y, (int)sunRadius);
        }
    }

    private Vector2 GetValidSpawnPosition(SpawnedObjectType type) {
        Vector2 randomPos = GetRandomPosition(minGenerationDistanceFromOrigin, mapSize);
        int iterator = 0;
        while (PosIsTooCloseToExistingObject(randomPos, type)) {
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

    bool PosIsTooCloseToExistingObject(Vector2 positionToCheck, SpawnedObjectType type) {
        float effectiveMinDistanceBetweenPoints = minDistanceBetweenObjects;

        if (type == SpawnedObjectType.Planet) {
            float maxPlanetSize = GetComponent<PlanetSpawner>().GetMaxRadiusPlanetPlusGravity();
            effectiveMinDistanceBetweenPoints += maxPlanetSize;
        }
        else if (type == SpawnedObjectType.Sun) {
            float maxSunRadius = GetComponent<SunSpawner>().GetSolarSystemMaxRadius();
            effectiveMinDistanceBetweenPoints += maxSunRadius * 2;
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