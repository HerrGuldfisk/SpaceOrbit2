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

    void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        // Generate planets
        for (int i = 0; i < numberOfPlanets; i++) {
            Vector2 randomPos = GetRandomPosition();
            Instantiate(planetPrefab, randomPos, Quaternion.identity);
        }

        // Generate stars
        for (int i = 0; i < numberOfStars; i++) {
            Vector2 randomPos = GetRandomPosition();
            Instantiate(starPrefab, randomPos, Quaternion.identity);
        }
    }

    Vector2 GetRandomPosition() {
        float x = Random.Range(-mapSize, mapSize);
        float y = Random.Range(-mapSize, mapSize);
        return new Vector2(x, y);
    }
}