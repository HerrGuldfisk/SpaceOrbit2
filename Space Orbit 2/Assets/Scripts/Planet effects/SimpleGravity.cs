using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planets;

public class SimpleGravity : MonoBehaviour
{

    private List<Rigidbody> m_Rigidbodies = new List<Rigidbody>();

    public float _planetSize = 5f;

    [SerializeField] private float _gravity = 5f;

    [SerializeField] private float _gravityFieldSize = 3f;

    private Transform planetTransform;

    void Start()
    {
        planetTransform = transform.parent;

        SetPlanetSize(_planetSize);


        transform.localScale = new Vector3(_gravityFieldSize, _gravityFieldSize, 1);
    }

    void Update()
    {
        
    }

    
    private void OnValidate()
    {
        SetPlanetSize(_planetSize);
    }

    void SetPlanetSize(float planetSize)
    {
        if (planetTransform != null)
        {
            planetSize *= GeneralPlanetSettings.instance.gravityDistance;
            planetTransform.localScale = new Vector3(planetSize, planetSize, planetSize);
        }
    }
    

    void FixedUpdate()
    {
        foreach (Rigidbody rb in m_Rigidbodies)
        {
            rb.AddForce((transform.position - rb.position).normalized * _gravity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Rigidbody rb))
        {
            Debug.Log("Starship enter");
            m_Rigidbodies.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Rigidbody rb))
        {
            Debug.Log("Starship leave");
            rb.AddForce(other.transform.GetChild(0).transform.forward * 100);
            m_Rigidbodies.Remove(rb);
        }
    }
}
