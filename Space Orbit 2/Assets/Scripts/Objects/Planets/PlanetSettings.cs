using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSettings : MonoBehaviour {
    [Header("Planet Settings")] [SerializeField]
    float _bodySize = 15f;

    [SerializeField] PlanetGravityMode _gravityMode = PlanetGravityMode.Linear;
    [SerializeField] float _gravityFieldSize = 50f;
    [SerializeField] float _gravityStrength = 2f;

    [SerializeField] private float BodySize = 15f;

    public PlanetGravityMode GravityMode {
        get { return _gravityMode; }
    }

    public float GravityFieldSize {
        get { return _gravityFieldSize; }
        set { _gravityFieldSize = value; }
    }

    public float GravityStrength {
        get { return _gravityStrength; }
        set { _gravityStrength = value; }
    }


    [Header("Do not change")] [SerializeField]
    Transform _body;

    [SerializeField] Transform _gravityField;

    private void OnValidate() {
        SetBodySize(_bodySize);
        SetGravityFieldSize(_gravityFieldSize);
    }

    public void SetGravityFieldSize(float newSize) {
        if (_gravityField != null) {
            _gravityFieldSize = newSize;
            _gravityField.localScale = new Vector3(newSize, newSize, 1);
        }
    }

    public void SetBodySize(float newSize) {
        if (_body != null) {
            _bodySize = newSize;
            _body.localScale = new Vector3(newSize, newSize, newSize);
        }
    }


    public enum PlanetGravityMode {
        Constant,
        Linear,
        Logarithmic
    }
}