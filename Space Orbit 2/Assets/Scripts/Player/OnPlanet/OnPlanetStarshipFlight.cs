using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OnPlanet
{
    public class OnPlanetStarshipFlight : MonoBehaviour
    {

        Vector2 _rawMoveInput;
        float _normalizedMoveInputX;
        float _thrustInput;

        Rigidbody2D _rb;

        [SerializeField] float _thrustPower = 200f;
        [SerializeField] float _steerPower = 50f;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        
        void FixedUpdate()
        {
            if(_thrustInput > 0)
            {
                _rb.AddForceAtPosition(transform.up * _thrustPower * _thrustInput, transform.localPosition + Vector3.up * 0.5f);
            }

            if(_normalizedMoveInputX != 0) 
            {
                if (_normalizedMoveInputX > 0)
                {
                    _rb.AddForce(Vector3.right * _steerPower);
                }
                else
                {
                    _rb.AddForce(Vector3.left * _steerPower);
                }
            }
            else
            {

            }

            _rb.velocity = new Vector2(Mathf.Clamp(_rb.velocity.x, -20f, 20f), Mathf.Clamp(_rb.velocity.y, -40, 40));
        }

        void OnStarshipMove(InputValue value)
        {
            _rawMoveInput = value.Get<Vector2>();
            
            if(_rawMoveInput.x == 0)
            {
                _normalizedMoveInputX = 0;
            }
            else
            {
                _normalizedMoveInputX = _rawMoveInput.x > 0 ? 1 : -1;
            }
        }

        void OnStarshipThrust(InputValue value)
        {
            _thrustInput = value.Get<float>();
        }
    }
}

