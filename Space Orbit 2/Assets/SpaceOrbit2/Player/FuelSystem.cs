using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelSystem : MonoBehaviour
{
    private float _currentFuel;
    private float _maxFuel;

    [SerializeField] private string _currentFuelText;
    [SerializeField] private TMP_Text _displayText;

    public void UseFuel(float fuelUsed)
    {
        _currentFuel -= fuelUsed;
        UpdateFuelText();
    }

    public void AddFuel(float fuelAdded)
    {
        _currentFuel += fuelAdded;

        if (_currentFuel > _maxFuel)
        {
            ResetFuel();
        }

        UpdateFuelText();
    }

    public bool TryRemoveFuel(float fuelRemoved)
    {
        if (IsFuelEmpty())
        {
            return false;   
        }
        else
        {
            _currentFuel -= fuelRemoved;
            UpdateFuelText();
            return true;
        }     
    }

    public void SetCurrentFuel(float fuelSet)
    {
        _currentFuel = fuelSet;

        if (_currentFuel > _maxFuel)
        {
            ResetFuel();
        }

        UpdateFuelText();
    }

    public float GetCurrentFuel()
    {
        return _currentFuel;
    }

    public void ResetFuel()
    {
        SetCurrentFuel(_maxFuel);
    }

    public void SetMaxFuel(float newMaxFuel)
    {
        _maxFuel = newMaxFuel;
        UpdateFuelText();
    }
    public float GetMaxFuel()
    {
        return _maxFuel;
    }

    public bool IsFuelEmpty()
    {
        if (_currentFuel <= 0) {return true;} else { return false; }
    }

    public void UpdateFuelText()
    {
        _currentFuelText = "Fuel: " + _currentFuel.ToString("F0") + " / " + _maxFuel.ToString("F0");
        _displayText.text = _currentFuelText;
    }
}
