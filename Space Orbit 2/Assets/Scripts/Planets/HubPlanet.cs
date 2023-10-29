using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach to a planet gameobjcet
//when the player enters the orbit, the player will deliver all its stars
public class HubPlanet : MonoBehaviour
{
    private StarManager _starManager;

    private void Awake()
    {
        _starManager = FindObjectOfType<StarManager>();
        Debug.Assert((_starManager != null), "no star manager found in scene");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _starManager.DeliverHeldStars();
        }
    }
}