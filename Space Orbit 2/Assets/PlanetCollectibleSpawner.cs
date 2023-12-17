using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// spawns a collectible in orbit around a planet
/// </summary>
public class PlanetCollectibleSpawner : MonoBehaviour {
    [SerializeField] private Transform gravityField;
    [SerializeField] private Transform body;
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private GameObject orbitCenterPrefab;
    [SerializeField] private float orbitVelocity = 3;
    [SerializeField] private float minDistanceFromEdges = 5;
    private Transform _thisTransform;

    private void Awake() {
        _thisTransform = transform;
    }

    public void SpawnCollectible() {
        float gravityFieldRadius = gravityField.lossyScale.x / 2;
        float bodyRadius = body.lossyScale.x / 2;
        Vector3 planetPos = _thisTransform.position;

        GameObject orbitCenter = Instantiate(orbitCenterPrefab, planetPos, Quaternion.identity, _thisTransform);
        orbitCenter.GetComponent<RotateContinuously>().angularVelocity = orbitVelocity;

        GameObject collectible = Instantiate(collectiblePrefab, planetPos, Quaternion.identity, orbitCenter.transform);

        float minDistanceFromCenter = bodyRadius + minDistanceFromEdges;
        float maxDistanceFromCenter = gravityFieldRadius - minDistanceFromEdges;
        float distanceFromPlanet = Random.Range(minDistanceFromCenter, maxDistanceFromCenter);
        collectible.transform.position = planetPos + (Vector3.right * distanceFromPlanet);
    }
}