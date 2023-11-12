using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSettings : MonoBehaviour
{
    [Header("Planet Settings")]
    [SerializeField] float _bodySize = 15f;

    [SerializeField] PlanetGravityMode _gravityMode;
    [SerializeField] float _gravityFieldSize = 50f;
    [SerializeField] float _gravityStrength;

    public float BodySize { get; }
    public PlanetGravityMode GravityMode { get { return _gravityMode; } }
    public float GravityFieldSize { get { return _gravityFieldSize; } set { _gravityFieldSize = value; } }
    public float GravityStrength { get { return _gravityStrength; } set { _gravityStrength = value; } }
    


    [Header("Do not change")]
    [SerializeField] Transform _body;
    [SerializeField] Transform _gravityField;
    

    void Start()
    {

    }


    void Update()
    {
        
    }


    private void OnValidate()
    {
        if (_body != null)
        {
            _body.localScale = new Vector3(_bodySize, _bodySize, _bodySize);
        }

        if(_gravityField != null)
        {
            _gravityField.localScale = new Vector3(_gravityFieldSize, _gravityFieldSize, 1);
        }
    }

    public enum PlanetGravityMode
    {
        Constant,
        Linear,
        Logarithmic
    }
}
