using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    private int _starsInScene = 0;
    private int _starsHeld = 0;
    private int _starsDelivered = 0;

    [SerializeField] private TextMeshProUGUI starsHeldText;
    [SerializeField] private TextMeshProUGUI starsDeliveredText;
    
    private void Awake()
    {
        _starsInScene = FindObjectsOfType<Star>().Length;
        UpdateStarCounterUI();
    }

    private void UpdateStarCounterUI()
    {
        starsHeldText.text = "Held: " + _starsHeld.ToString();
        starsDeliveredText.text = "Delivered: " + _starsDelivered.ToString() + "/" + _starsInScene.ToString();
    }

    // Called by the player when they enter the delivery zone
    public void DeliverHeldStars()
    {
        _starsDelivered += _starsHeld;
        _starsHeld = 0;
        UpdateStarCounterUI();
        if (_starsDelivered == _starsInScene)
        {
            WinScreen.Instance.Show();
        }
    }

    // Called by the player when they pick up a star
    public void AddStarToHeld()
    {
        _starsHeld++;
        UpdateStarCounterUI();
    }
}