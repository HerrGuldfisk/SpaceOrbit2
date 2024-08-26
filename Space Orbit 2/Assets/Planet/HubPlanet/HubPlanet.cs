using UnityEngine;

/// <summary>
/// Attach to a hub planet
/// sets the planet to be dead at starts
/// dead = no pings, no fuel, different color, empty socket
/// at adds the ability to revive it if a collectible is delivered
/// </summary>
public class HubPlanet : MonoBehaviour {
    [SerializeField] private Material deadPlanetMaterial;
    [SerializeField] private Material alivePlanetMaterial;
    [SerializeField] private Renderer planetBodyRenderer;
    [SerializeField] private GameObject collectibeInSocketVisual;
    [SerializeField] private FuelRechargeArea fuelRechargeArea;
    public bool isAlive;

    public delegate void HubPlanetRevived(GameObject hubPlanet);

    public static event HubPlanetRevived OnHubPlanetRevived;

    private void Awake() {
        SetDead();
    }

    private void SetDead() {
        isAlive = false;
        planetBodyRenderer.material = deadPlanetMaterial;
        fuelRechargeArea.canRechargeFuel = false;
        collectibeInSocketVisual.SetActive(false);
    }

    public void Revive() {
        isAlive = true;
        collectibeInSocketVisual.SetActive(true);
        planetBodyRenderer.material = alivePlanetMaterial;
        fuelRechargeArea.canRechargeFuel = true;
        GetComponent<HubPlanetDetector>().PingTheClosestTwoDeadHubPlanets();
        OnHubPlanetRevived?.Invoke(gameObject);
    }
}