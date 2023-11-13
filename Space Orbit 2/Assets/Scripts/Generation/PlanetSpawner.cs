using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//spawns a planet at a given location, and randomizes a bunch of settings for it
public class PlanetSpawner : MonoBehaviour {
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private float bodySizeMin = 2f;
    [SerializeField] private float bodySizeMax = 15f;
    [SerializeField] private float gravityFieldSizeMin = 10f;
    [SerializeField] private float gravityFieldSizeMax = 100f;

    public void SpawnPlanet(Vector2 worldPosition) {
        GameObject planetInstance = Instantiate(planetPrefab, worldPosition, Quaternion.identity);
        RandomizePlanetSettings(planetInstance);
    }

    private void RandomizePlanetSettings(GameObject planetInstance) {
        PlanetSettings planetSettings;
        planetInstance.TryGetComponent(out planetSettings);
        Debug.Assert(planetSettings != null, "no planet settings component found on planet instance");
        if (planetSettings != null) {
            float bodySize = Random.Range(bodySizeMin, bodySizeMax);
            float gravityFieldSize = Random.Range(gravityFieldSizeMin, gravityFieldSizeMax);
            planetSettings.SetBodySize(bodySize);
            planetSettings.SetGravityFieldSize(gravityFieldSize);
        }
    }
}