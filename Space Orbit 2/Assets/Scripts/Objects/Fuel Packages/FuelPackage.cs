using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPackage : MonoBehaviour
{
    FuelTank _fuelTank;

    public float amountFuel = 20f;

    private void Update()
    {
        transform.Rotate(0f, 0f, amountFuel/20f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Player"))
            {
                _fuelTank = collision.GetComponent<FuelTank>();
                _fuelTank.AddFuel(amountFuel);

                Destroy(gameObject);
            }
        }
    }
}
