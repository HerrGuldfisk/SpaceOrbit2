using System;
using System.Collections.Generic;
using UnityEngine;

namespace SolarSystem {
    /// <summary>
    /// Spawns and respawns snek flocks in a solar system
    /// </summary>
    ///
    [RequireComponent(typeof(SolarSystemTransformMethods))]
    public class SolarSystemSnekManager : MonoBehaviour {
        private readonly List<GameObject> _snekFlocks = new();

        public void RespawnSnekFlocks(int newFlockCount, int newSnekPerFlockCount) {
            ClearAllFlocks();
            SpawnSnekFlocks(newFlockCount, newSnekPerFlockCount);
        }

        private void SpawnSnekFlocks(int numberOfFlocks, int numberOfSneksInFlock) {
            SolarSystemTransformMethods transformMethods = GetComponent<SolarSystemTransformMethods>();
            OthersSpawner othersSpawner = FindObjectOfType<OthersSpawner>();
            Debug.Assert(othersSpawner != null, "cant find an other spawner in the scene");
            while (numberOfFlocks > 0) {
                //pos should be random between sun and orbit field
                Vector2 spawnPos = transformMethods.GetRandomPosOutsideBody();
                GameObject flockInstance = othersSpawner.SpawnFlock(spawnPos, "Snek", numberOfSneksInFlock);
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
}