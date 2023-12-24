using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// attach to the hub planet
/// has the ability to send out a sonar wave to ping new solar systems
/// should be used once on revival
/// </summary>
public class HubPlanetDetector : MonoBehaviour {
    public delegate void DeadHubPlanetDetected(Transform deadHubPlanet);

    public static event DeadHubPlanetDetected OnDeadHubPlanetDetected;
    [SerializeField] private bool detectSelfOnStart = false;

    private void Start() {
        if (detectSelfOnStart) {
            OnDeadHubPlanetDetected?.Invoke(transform);
        }
    }

    public void PingTheClosestTwoDeadHubPlanets() {
        var deadHubPlanetsByDistance = GetDeadHubPlanetsSortedByDistance();
        bool foundAtLeastOnePlanet = deadHubPlanetsByDistance.Count > 0;
        bool foundAtLeastTwoPlanets = deadHubPlanetsByDistance.Count > 1;

        if (foundAtLeastOnePlanet) {
            Transform planetToPing = deadHubPlanetsByDistance[0];
            OnDeadHubPlanetDetected?.Invoke(planetToPing);
        }

        if (foundAtLeastTwoPlanets) {
            Transform planetToPing = deadHubPlanetsByDistance[1];
            OnDeadHubPlanetDetected?.Invoke(planetToPing);
        }
    }

    private List<Transform> GetDeadHubPlanetsSortedByDistance() {
        var deadHubPlanetsByDistance = new List<(Transform planet, float distance)>();

        Vector2 positionOfThisPlanet = transform.position;
        HubPlanet[] allHubPlanets = FindObjectsOfType<HubPlanet>();
        foreach (var hubPlanet in allHubPlanets) {
            bool isDead = !hubPlanet.isAlive;
            bool isNotThisPlanet = hubPlanet.transform != transform;
            if (isDead && isNotThisPlanet) {
                float distance = Vector2.Distance(hubPlanet.transform.position, positionOfThisPlanet);
                deadHubPlanetsByDistance.Add((hubPlanet.transform, distance));
            }
        }

        deadHubPlanetsByDistance.Sort((a, b) => a.distance.CompareTo(b.distance));

        return deadHubPlanetsByDistance.Select(item => item.planet).Take(2).ToList();
    }
}