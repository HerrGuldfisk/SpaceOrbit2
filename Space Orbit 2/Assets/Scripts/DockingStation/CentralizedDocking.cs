//holds the value of the players last docking station!
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralizedDocking : MonoBehaviour
{
  private GameObject currentStation;

  public void SetCurrentStation(GameObject station)
  {
    this.currentStation = station;
    Debug.Log("[CentralizedDocking] Player docked to station: " + station.name);
  }

  public GameObject GetCurrentStation()
  {
    return currentStation;
  }

}