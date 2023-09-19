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
    Transform spawnPoint = centralizedDocking.GetCurrentStation().GetComponent<Dock>().GetPlayerSpawnPoint();

    transform.position = spawnPoint.position;
    transform.rotation = spawnPoint.rotation;
    transform.velocity = 0;
    trans
    //maybe set velocity to 0
    //and also set angular velocity to 0
    //and rotation to the rotation of the spawn point
  }
}