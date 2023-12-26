using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns and respawns snek flocks in the solar system
/// </summary>
public class SolarSystemSnekManager : MonoBehaviour {
    private readonly List<GameObject> _snekFlocks = new();

    public void RespawnSnekFlocks(int newFlockCount, int newSnekPerFlockCount) {
        ClearAllFlocks();
        SpawnSnekFlocks(newFlockCount, newSnekPerFlockCount);
    }

    private void SpawnSnekFlocks(int numberOfFlocks, int numberOfSneksInFlock) {
        OthersSpawner othersSpawner = FindObjectOfType<OthersSpawner>();
        Debug.Assert(othersSpawner != null, "cant find an other spawner in the scene");
        while (numberOfFlocks > 0) {
            GameObject flockInstance = othersSpawner.SpawnFlock(transform.position, "Snek", numberOfSneksInFlock);
            numberOfFlocks--;
            _snekFlocks.Add(flockInstance);
        }
    }

    private void ClearAllFlocks() {
        foreach (var snekFlock in _snekFlocks) {
            _snekFlocks.Remove(snekFlock);
            Destroy(snekFlock);
        }
    }
}