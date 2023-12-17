using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//generates a sun that has planets orbiting around it
public class SunSpawner : MonoBehaviour {
    [SerializeField] private GameObject sunPrefab;
    [SerializeField] private float sunSizeMin = 15f;
    [SerializeField] private float sunSizeMax = 100f;
    [SerializeField] private float sunGravityFieldSizeMin = 2f;
    [SerializeField] private float sunGravityFieldSizeMax = 5f;

    public GameObject Spawn(Vector2 worldPosition) {
        GameObject sunInstance = Instantiate(sunPrefab, worldPosition, Quaternion.identity);
        RandomizeSunSettings(sunInstance);
        sunInstance.GetComponent<SolarSystemSpawner>().SpawnSolarSystem(sunGravityFieldSizeMin, sunGravityFieldSizeMax);
        return sunInstance;
    }

    private void RandomizeSunSettings(GameObject sunInstance) {
        float randomSize = Random.Range(sunSizeMin, sunSizeMax);
        Vector3 sizeVector = new Vector3(randomSize, randomSize, randomSize);
        sunInstance.transform.localScale = sizeVector;
    }
}