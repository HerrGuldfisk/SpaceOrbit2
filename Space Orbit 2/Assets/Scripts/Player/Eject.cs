using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Eject : MonoBehaviour
{
  private InputAction ejectAction;
  private CentralizedDocking centralizedDocking;

  void Awake()
  {
    ejectAction = new InputAction("Dock", binding: "<Keyboard>/e");
    ejectAction.performed += ctx => EjectToDockingStation();
    ejectAction.Enable();
    centralizedDocking = GameObject.Find("CentralizedDocking").GetComponent<CentralizedDocking>();
  }

  void EjectToDockingStation()
  {
    if (centralizedDocking.GetCurrentStation() == null)
    {
      Debug.Log("No station to eject to!");
      return;
    };
    Debug.Log("Ejecting to station: " + centralizedDocking.GetCurrentStation().name);
    Transform spawnPoint = centralizedDocking.GetCurrentStation().GetComponent<Dock>().GetPlayerSpawnPoint();
    Rigidbody rigidbody = GetComponent<Rigidbody>();
    rigidbody.velocity = rigidbody.velocity.normalized;
    rigidbody.angularVelocity = rigidbody.angularVelocity.normalized;
    transform.position = spawnPoint.position;
    transform.rotation = spawnPoint.rotation;
  }
}