using UnityEngine;

/// <summary>
/// updates the solar system according to a difficulty level between 0 and 1
/// </summary>
public class SolarSystemDifficulty : MonoBehaviour {
    public float DifficultyLevel { get; private set; } = 0.5f;

    [SerializeField] float basePlanetSpeed = 100f;
    [SerializeField] float planetSpeedDifficultyMultiplier = 0.3f;
    [SerializeField] float snekFlockDensityMultiplier = 0.1f;
    [SerializeField] int maxSneksPerFlock = 15;
    [SerializeField] float maxPlanetGravityStrength = 1f;

    public void SetDifficulty(float newDifficulty) {
        Debug.Assert(newDifficulty >= 0, "difficulty must be greater than or equal to 0. It was set to " + newDifficulty);
        Debug.Assert(newDifficulty <= 1, "difficulty must be less than or equal to 1. It was set to " + newDifficulty);
        DifficultyLevel = newDifficulty;
        UpdateSolarSystem();
    }

    private void UpdateSolarSystem() {
        UpdatePlanets();
        UpdateSnekFlocks();
    }

    private void UpdateSnekFlocks() {
        float spawnAreaThickness = GetComponent<SolarSystemSpawner>()._spawnAreaThickness;
        int numberOfSnekFlocks = (int)(spawnAreaThickness * DifficultyLevel * snekFlockDensityMultiplier);
        int numberOfSneksPerFlock = (int)(DifficultyLevel * maxSneksPerFlock);
        GetComponent<SolarSystemSnekManager>().RespawnSnekFlocks(numberOfSnekFlocks, numberOfSneksPerFlock);
        //TODO: set snek speed based on difficulty
    }

    private void UpdatePlanets() {
        UpdateNormalPlanets();
        UpdateHubPlanet();
    }

    private void UpdateHubPlanet() {
        GameObject hubPlanet = GetComponentInChildren<HubPlanet>().gameObject;
        SetPlanetSpeedBasedOnDifficulty(hubPlanet);
    }

    private void UpdateNormalPlanets() {
        var planets = GetComponent<SolarSystemSpawner>().planets;
        foreach (var planet in planets) {
            SetPlanetSpeedBasedOnDifficulty(planet);
            planet.GetComponent<PlanetAsteroidManager>().RespawnAsteroidsByDifficulty(DifficultyLevel);
            planet.GetComponent<PlanetSettings>().GravityStrength = DifficultyLevel * maxPlanetGravityStrength;
        }
    }

    private void SetPlanetSpeedBasedOnDifficulty(GameObject planet) {
        RotateContinuously rotationCenter = planet.GetComponentInParent<RotateContinuously>();
        Vector2 rotationPos = rotationCenter.transform.position;
        Vector2 planetPos = planet.transform.position;
        float distanceFromSun = Vector2.Distance(rotationPos, planetPos);
        float planetSpeed = basePlanetSpeed + DifficultyLevel * planetSpeedDifficultyMultiplier;
        float centerRotationSpeed = planetSpeed / distanceFromSun;
        rotationCenter.angularVelocity = centerRotationSpeed;
    }
}