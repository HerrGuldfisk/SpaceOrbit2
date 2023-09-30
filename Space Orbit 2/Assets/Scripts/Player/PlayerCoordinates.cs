using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCoordinates : MonoBehaviour
{
    [SerializeField] private string _currentCoordinates;

    [SerializeField] private TMP_Text _displayText;

    [SerializeField] private Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        _target = transform;
    }

    // Update is called once per frame
    void Update()
    {
        _currentCoordinates = "[" + _target.position.x.ToString("F0") + " , " + _target.position.y.ToString("F0") + "]";
        _displayText.text = _currentCoordinates;
    }
}
