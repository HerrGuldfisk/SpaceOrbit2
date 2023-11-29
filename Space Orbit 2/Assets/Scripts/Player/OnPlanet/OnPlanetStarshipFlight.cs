using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;

namespace OnPlanet
{
    public class OnPlanetStarshipFlight : MonoBehaviour
    {

        StateMachine _stateMachine = new StateMachine();
        Dictionary<StarshipStates, BaseState> _availableStates = new Dictionary<StarshipStates, BaseState>();

        Vector2 _rawMoveInput;
        float _normalizedMoveInputX;
        float _thrustInput;

        Rigidbody2D _rb;

        [SerializeField] float _thrustPower = 300f;
        [SerializeField] float _steerPower = 100f;


        [SerializeField] VisualEffect _thruster;
        [SerializeField] Light2D _thrustLight;
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();

            _availableStates[StarshipStates.Docked] = new CameraFollowPlanetState(Camera.main);
        }

        
        void FixedUpdate()
        {
            // If docked do nothing.
            if(_stateMachine.CurrentState == _availableStates[StarshipStates.Docked]) { return; }

            // Add all this to separate state
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

            // Visual fire and light for throttle
            _thruster.SetFloat("Throttle", Mathf.Lerp(_thruster.GetFloat("Throttle"), _thrustInput, 0.08f));
            _thrustLight.intensity = Mathf.Lerp(_thrustLight.intensity, _thrustInput, 0.08f);

            // Clamping max velocity in x and y
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

        public enum StarshipStates
        {
            Docked
        }
    }
}

