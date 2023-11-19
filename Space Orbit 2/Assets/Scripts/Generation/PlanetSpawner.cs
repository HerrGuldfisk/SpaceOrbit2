using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

//spawns a planet at a given location, and randomizes a bunch of settings for it, and spawns asteroid around it
public class PlanetSpawner : MonoBehaviour {
    [SerializeField] private GameObject planetPrefab;

    [SerializeField] private float bodySizeMin = 2f;
    [SerializeField] private float bodySizeMax = 15f;
    [SerializeField] private float gravityFieldSizeMin = 10f;
    [SerializeField] private float gravityFieldSizeMax = 100f;

    private float _bodySize;
    private float _gravityFieldSize;

    public float GetMaxRadiusPlanetPlusGravity() {
        return gravityFieldSizeMax + bodySizeMax;
    }

    public void SpawnPlanet(Vector2 worldPosition) {
        GameObject planetInstance = Instantiate(planetPrefab, worldPosition, Quaternion.identity);
        RandomizePlanetSettings(planetInstance);
        planetInstance.GetComponent<AsteroidSpawner>().SpawnAsteroidsInOrbit();
    }

    private void RandomizePlanetSettings(GameObject planetInstance) {
        PlanetSettings planetSettings;
        planetInstance.TryGetComponent(out planetSettings);
        Debug.Assert(planetSettings != null, "no planet settings component found on planet instance");
        if (planetSettings != null) {
            _bodySize = Random.Range(bodySizeMin, bodySizeMax);
            _gravityFieldSize = Random.Range(gravityFieldSizeMin, gravityFieldSizeMax);
            planetSettings.SetBodySize(_bodySize);
            planetSettings.SetGravityFieldSize(_gravityFieldSize);
        }
    }
}