using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

//generates a sun that has planets orbiting around it
[RequireComponent(typeof(PlanetSpawner))]
public class SunSpawner : MonoBehaviour {
    [SerializeField] private GameObject sunPrefab;
    [SerializeField] private float sunSizeMin = 15f;
    [SerializeField] private float sunSizeMax = 100f;

    public void Spawn(Vector2 worldPosition) {
        GameObject sunInstance = Instantiate(sunPrefab, worldPosition, Quaternion.identity);
        RandomizeSunSettings(sunInstance);
        //++spawn planets orbiting around the sun
    }

    private void RandomizeSunSettings(GameObject sunInstance) {
        float randomSize = Random.Range(sunSizeMin, sunSizeMax);
        Vector3 sizeVector = new Vector3(randomSize, randomSize, randomSize);
        sunInstance.transform.localScale = sizeVector;
    }
}