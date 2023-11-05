using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarAbilityTimed : MonoBehaviour
{
    [Tooltip("Speed it takes to charge in seconds")]
    [SerializeField] private float _chargeSpeed;

    [SerializeField] private SonarWave _sonarWave;

    private BasicMovement _basicMovement;

    private float _currentCharge = 0;

    private bool _shouldBeCharging = true;

    void Start()
    {
        _basicMovement = GetComponent<BasicMovement>();
        _basicMovement.OnOrbit.AddListener(OnOrbit);
    }


    void Update()
    {
        if (_basicMovement.InOrbit && _shouldBeCharging)
        {
            _currentCharge += Time.deltaTime * 100 / _chargeSpeed;
        }

        if(_currentCharge >= 100 && _shouldBeCharging)
        {
            _currentCharge = 0;
            _shouldBeCharging = false;
            SonarWave newWave = Instantiate(_sonarWave, transform.root);
            newWave.BeginSonar(4);
            newWave.transform.parent = null;
        }
    }

    
    private void OnOrbit(bool state)
    {
        _shouldBeCharging = state;
        if( !_shouldBeCharging ) 
        {
            _currentCharge = 0;
        }
    }
    
}
