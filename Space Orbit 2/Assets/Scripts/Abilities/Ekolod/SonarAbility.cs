using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SonarAbility : PlayerAbility
{
    [SerializeField] private float _maxCharge = 5;
    [SerializeField] private float _charge;

    [SerializeField] private SonarWave _sonarWave;

    private ChargeMode _chargeMode = ChargeMode.Empty;
    private BasicMovement _basicMovement;

    private Coroutine _chargingCoroutine;

    void Start()
    {
        _basicMovement = GetComponent<BasicMovement>();
    }


    void Update()
    {
        
    }

    void OnCharge(InputValue rawInput)
    {
        float input = rawInput.Get<float>();

        switch (input)
        {
            case 1:
                if (_chargeMode == ChargeMode.Empty)
                {
                    _chargingCoroutine = StartCoroutine(ChargingIEnumerator());
                }
                else if(_chargeMode == ChargeMode.Ready)
                {
                    SendSonarWave(_charge);
                    _charge = 0;
                    _chargeMode = ChargeMode.Empty;
                }
                    
                break;

            case 0:
                if(_chargingCoroutine != null)
                {
                    StopCoroutine(_chargingCoroutine);
                    _chargingCoroutine = null;
                    _chargeMode = ChargeMode.Ready;
                }
                break;
        }
        
    }

    IEnumerator ChargingIEnumerator()
    {
        while (_charge < _maxCharge)
        {
            if(_basicMovement.FuelSystem.GetCurrentFuel() > Time.deltaTime * 2)
            {
                _charge += Time.deltaTime;
                if(!_basicMovement.FuelSystem.TryRemoveFuel( Time.deltaTime * 2))
                {

                }
            }
            
            yield return null;
        }

        _charge = 5;
        _chargeMode = ChargeMode.Ready;
    }

    void SendSonarWave(float savedCharge)
    {
        Debug.Log("Spawning wave");
        SonarWave newWave = Instantiate(_sonarWave, transform.root);
        newWave.BeginSonar(savedCharge);
    }



    public enum ChargeMode
    {
        Empty,
        Charging,
        Ready
    }
}
