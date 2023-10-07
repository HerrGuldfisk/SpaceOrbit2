using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelPackage : MonoBehaviour
{
    FuelSystem fuelSystem;

    public float amountFuel = 20f;

    private void Start()
    {
        transform.localScale = new Vector3(amountFuel / 10f, amountFuel/ 10f, 1); 
    }

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
                fuelSystem = collision.GetComponent<FuelSystem>();
                fuelSystem.AddFuel(amountFuel);

                Destroy(gameObject);
            }
        }
    }
}
