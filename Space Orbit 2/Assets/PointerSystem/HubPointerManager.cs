using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PointerSpawner))]
/// <summary>
/// Spawns and manages pointers for the hub planets when they are revived or detected
/// Attach to the pointer canvas
/// </summary>
public class HubPointerManager : MonoBehaviour {
    private PointerSpawner _pointerSpawner;
    [SerializeField] private Color deadHubPlanetPointerColor = Color.grey;
    [SerializeField] private Color revivedHubPlanetPointerColor = Color.green;

    private void Awake() {
        _pointerSpawner = GetComponent<PointerSpawner>();
    }

    private void OnEnable() {
        HubPlanetDetector.OnDeadHubPlanetDetected += AddPointerToDeadHubPlanet;
        HubPlanet.OnHubPlanetRevived += ChangePointerColorForRevivedHubPlanet;
    }

    private void OnDisable() {
        HubPlanetDetector.OnDeadHubPlanetDetected -= AddPointerToDeadHubPlanet;
        HubPlanet.OnHubPlanetRevived -= ChangePointerColorForRevivedHubPlanet;
    }

    private void AddPointerToDeadHubPlanet(Transform deadHubPlanetTrans) {
        _pointerSpawner.TrySpawnPointer(deadHubPlanetTrans.gameObject, deadHubPlanetPointerColor);
    }

    private void ChangePointerColorForRevivedHubPlanet(GameObject hubPlanet) {
        Pointer pointer = _pointerSpawner.GetPointerForTargetIfItExists(hubPlanet);
        bool pointerExists = pointer != null;
        if (pointerExists) {
            pointer.SetColor(revivedHubPlanetPointerColor);
        }
        else {
            _pointerSpawner.TrySpawnPointer(hubPlanet, revivedHubPlanetPointerColor);
        }
    }
}