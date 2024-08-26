using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SolarSystem {
    /// <summary>
    /// Methods for getting information about the solar system geometry
    /// </summary>
    public class SolarSystemTransformMethods : MonoBehaviour {
        [SerializeField] private Transform sunT;
        [SerializeField] private Transform solarSystemT;

        public Vector2 GetRandomPosOutsideBody() {
            float bodyRadius = sunT.lossyScale.x / 2;
            float solarSystemRadius = solarSystemT.lossyScale.x / 2;
            float randomMagnitude = Random.Range(bodyRadius, solarSystemRadius);
            Vector2 randomVector = Random.insideUnitCircle.normalized * randomMagnitude;
            Vector2 randomPos = (Vector2)transform.position + randomVector;
            return randomPos;
        }
    }
}