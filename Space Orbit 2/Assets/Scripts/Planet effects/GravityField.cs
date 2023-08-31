using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planets;

public class GravityField : MonoBehaviour
{
    PlanetSettings _planetSettings;

    private void Start()
    {
        _planetSettings = transform.parent.GetComponent<PlanetSettings>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Rigidbody rb))
        {
           _planetSettings.RigidbodiesInOrbit.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Rigidbody rb))
        {
            // rb.AddForce(other.transform.GetChild(0).transform.forward * 100);
            _planetSettings.RigidbodiesInOrbit.Remove(rb);
        }
    }
}
