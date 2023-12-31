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
        private SolarSystemTransformMethods _solarSystemTransformMethods;
        private OthersSpawner _othersSpawner;

        private void Awake() {
            _solarSystemTransformMethods = GetComponent<SolarSystemTransformMethods>();
            _othersSpawner = FindObjectOfType<OthersSpawner>();
            Debug.Assert(_othersSpawner != null, "cant find an other spawner in the scene");
        }

        public void RespawnSnekFlocks(int newFlockCount, int newSnekPerFlockCount) {
            ClearAllFlocks();
            SpawnSnekFlocks(newFlockCount, newSnekPerFlockCount);
        }

        private void SpawnSnekFlocks(int numberOfFlocksWeWantInSolarSystem, int numberOfSneksPerFlock) {
            while (numberOfFlocksWeWantInSolarSystem > _snekFlocks.Count) {
                SpawnFlock(numberOfSneksPerFlock);
            }
        }

        private void SpawnFlock(int numberOfSneks) {
            Vector2 spawnPos = _solarSystemTransformMethods.GetRandomPosOutsideBody();
            GameObject flockInstance = _othersSpawner.SpawnFlock(spawnPos, "Snek", numberOfSneks);
            _snekFlocks.Add(flockInstance);
        }

        private void ClearAllFlocks() {
            foreach (var snekFlock in _snekFlocks) {
                _snekFlocks.Remove(snekFlock);
                Destroy(snekFlock);
            }
        }
    }
}