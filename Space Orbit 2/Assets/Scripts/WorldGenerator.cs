using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// randomly places a set amount of planets and stars around the world
public class WorldGenerator : MonoBehaviour {
    public GameObject planetPrefab;
    public GameObject starPrefab;
    public int numberOfPlanets = 10;
    public int numberOfStars = 5;
    public float mapSize = 500f;
    [SerializeField] private float minGenerationDistanceFromOrigin = 50f;

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
            Instantiate(prefab, randomPos, Quaternion.identity);
        }
    }

    Vector2 GetRandomPosition(float minDistanceFromOrigin, float maxDistanceFromOrigin) {
        float distanceFromOrigin = Random.Range(minDistanceFromOrigin, maxDistanceFromOrigin);
        Vector2 directionFromOrigin = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Vector2 position = directionFromOrigin * distanceFromOrigin;
        return position;
    }
}