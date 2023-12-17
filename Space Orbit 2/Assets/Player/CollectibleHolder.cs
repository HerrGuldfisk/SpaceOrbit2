using UnityEngine;

public class CollectibleHolder : MonoBehaviour {
    bool _isHoldingCollectible;
    [SerializeField] private GameObject collectibleHeldVisuals;

    private void OnTriggerEnter2D(Collider2D other) {
        HubPlanet hubPlanet = other.GetComponentInParent<HubPlanet>();
        bool isHubPlanet = hubPlanet != null;

        if (_isHoldingCollectible && isHubPlanet) {
            bool hubPlanetNeedsToBeRevived = !hubPlanet.isAlive;

            if (hubPlanetNeedsToBeRevived) {
                hubPlanet.Revive();
                _isHoldingCollectible = false;
                collectibleHeldVisuals.SetActive(false);
            }
        }
        else {
            bool isCollectible = other.CompareTag("Star");

            if (isCollectible) {
                Destroy(other.gameObject);
                _isHoldingCollectible = true;
                collectibleHeldVisuals.SetActive(true);
            }
        }
    }
}