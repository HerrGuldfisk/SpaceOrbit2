using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// randomly places a set amount of planets and stars around the world
public class WorldGenerator : MonoBehaviour {
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private GameObject starPrefab;

    [SerializeField] private int numberOfPlanets = 10;
    [SerializeField] private int numberOfStars = 5;

    [SerializeField] private float mapSize = 500f;
    [SerializeField] private float minGenerationDistanceFromOrigin = 50f;
    [SerializeField] private float minDistanceBetweenObjects = 25f;

    private List<Vector2> positionsPlacedAt = new List<Vector2>();

    void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        GenerateObjects(numberOfStars, starPrefab);
        GenerateObjects(numberOfPlanets, planetPrefab);
    }

    private void GenerateObjects(float numberOfObjects, GameObject prefab) {
        for (int i = 0; i < numberOfObjects; i++) {
            Vector2 randomPos = GetRandomPosition(minGenerationDistanceFromOrigin, mapSize);
            int iterator = 0;
            while (PosIsTooCloseToExistingObject(randomPos)) {
                randomPos = GetRandomPosition(minGenerationDistanceFromOrigin, mapSize);
                iterator++;
            }

            Debug.Assert(iterator < 30,
                "iterated object placement position more than 30 times. Please make sure that there is enough space to generate all the objects");
            Instantiate(prefab, randomPos, Quaternion.identity);
            positionsPlacedAt.Add(randomPos);
        }
    }

    Vector2 GetRandomPosition(float minDistanceFromOrigin, float maxDistanceFromOrigin) {
        float distanceFromOrigin = Random.Range(minDistanceFromOrigin, maxDistanceFromOrigin);
        Vector2 directionFromOrigin = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Vector2 position = directionFromOrigin * distanceFromOrigin;
        return position;
    }

    bool PosIsTooCloseToExistingObject(Vector2 positionToCheck) {
        bool isTooClose = false;

        foreach (Vector2 positionPlaced in positionsPlacedAt) {
            float distanceBetweenNewAndOldPosition = Vector2.Distance(positionToCheck, positionPlaced);
            if (distanceBetweenNewAndOldPosition < minDistanceBetweenObjects) {
                isTooClose = true;
                break;
            }
        }

        return isTooClose;
    }
}