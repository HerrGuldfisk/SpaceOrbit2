//called when the player docks with the station!
//saves the docking station in the centralized docking script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dock : MonoBehaviour
{
    private InputAction dockAction;
    private bool canDock = false;
    [SerializeField] private GameObject dockInstructionCanvas;
    [SerializeField] private Transform playerSpawnPoint;
    private CentralizedDocking centralizedDocking;

    void Awake()
    {
        dockAction = new InputAction("Dock", binding: "<Keyboard>/e");
        dockAction.performed += ctx => DockToStation();
        dockAction.Enable();
        centralizedDocking = GameObject.Find("CentralizedDocking").GetComponent<CentralizedDocking>();
    }

    void DockToStation()
    {
        if (!canDock) return;
        centralizedDocking.SetCurrentStation(this.gameObject);
        setDockable(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (centralizedDocking.GetCurrentStation() == this.gameObject) return;
        if (!other.CompareTag("Player")) return;
        setDockable(true);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        setDockable(false);
    }

    void setDockable(bool dockable)
    {
        canDock = dockable;
        dockInstructionCanvas.SetActive(dockable);
    }

    public void GetPlayerSpawnPoint()
    {
        return playerSpawnPoint;
    }
}
