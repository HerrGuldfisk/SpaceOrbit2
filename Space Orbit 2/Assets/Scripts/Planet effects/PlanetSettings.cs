using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using Planets;

public class PlanetSettings : MonoBehaviour
{
    [Header("Planet Settings")]
    [SerializeField] float _planetSize = 5f;

    [Header("Gravity Settings")]
    [SerializeField] bool _sizeScaleWithPlanetSize = true;

    [Tooltip("Radius in number of planets")]
    [SerializeField] float _gravityFieldSize = 3f;

    [Space(30)]
    [SerializeField] bool _colorScaleWithGravityStrength = true;
    [Range(0f, 50f)]
    [SerializeField] float _gravityStrength = 10f;

    [SerializeField] Color32 _gravityColor = new Color32(124, 217, 217, 0);



    [Header("Game Objects")]
    [SerializeField] GameObject _body;
    [SerializeField] GameObject _gravityField;

    public List<Rigidbody> RigidbodiesInOrbit { get; set; } = new List<Rigidbody>();

    void Start()
    {
        
    }


    private void FixedUpdate()
    {
        if(RigidbodiesInOrbit.Count > 0)
        {
            foreach (Rigidbody rb in RigidbodiesInOrbit)
            {
                ApplyCorrectGravity(rb);
                
            }
        }
    }

    private void ApplyCorrectGravity(Rigidbody rb)
    {
        switch (GeneralPlanetSettings.Instance.gravityMode)
        {
            case GravityMode.CutOff:
                rb.AddForce((transform.position - rb.position).normalized * _gravityStrength);
                break;
                // TODO: Implement correct gravity for both modes. Max gravity at planet surface.
            case GravityMode.Linear:
                rb.AddForce((transform.position - rb.position).normalized * _gravityStrength);
                break;
            case GravityMode.Log:
                rb.AddForce((transform.position - rb.position).normalized * _gravityStrength);
                break;
        }
        
    }

    private void OnValidate()
    {
        _body.transform.localScale = new Vector3(_planetSize, _planetSize, _planetSize);

        if( _sizeScaleWithPlanetSize)
        {
            float factor = _planetSize * _gravityFieldSize;
            _gravityField.transform.localScale = new Vector3(factor, factor, factor);
        }

        if(_colorScaleWithGravityStrength)
        {
            _gravityColor.a = (byte)(255 * (_gravityStrength / 100));
        }

        _gravityField.GetComponent<SpriteRenderer>().color = _gravityColor;
    }
}
