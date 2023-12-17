using UnityEngine;

[RequireComponent(typeof(FuelTank))]
public class FuelRecharger : MonoBehaviour {
    private Collider2D _currentFuelRechargeArea;
    private bool _isFuelRecharging;
    private float fuelRechargePerSecond = 50f;
    private FuelTank _fuelTank;

    private void Awake() {
        _fuelTank = GetComponent<FuelTank>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        bool isFuelRechargeArea = other.TryGetComponent<FuelRechargeArea>(out var fuelRechargeArea);
        if (isFuelRechargeArea) {
            if (fuelRechargeArea.canRechargeFuel) {
                //!!!!!!!!will not work if it is activated while a player is already inside
                _currentFuelRechargeArea = other;
                _isFuelRecharging = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other == _currentFuelRechargeArea) {
            _currentFuelRechargeArea = null;
            _isFuelRecharging = false;
        }
    }

    private void Update() {
        if (_isFuelRecharging) {
            _fuelTank.AddFuel(fuelRechargePerSecond * Time.deltaTime);
        }
    }
}