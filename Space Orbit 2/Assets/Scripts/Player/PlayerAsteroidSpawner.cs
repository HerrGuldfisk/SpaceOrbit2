using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAsteroidSpawner : MonoBehaviour
{
    private BasicMovement _playerMovement;

    [SerializeField] private float _offsetPlayerDir = 100f;
    [SerializeField] private float _spawnTimer = 5f;
    // Could potentially scale based on player current velocity
    [SerializeField] private float _spawnRadius = 100f;

    void Start()
    {
        _playerMovement = GetComponent<BasicMovement>();

        StartCoroutine(SpawnAsteroidAfterDelay(_spawnTimer));
    }

    Vector2 FindSpawnPosition()
    {
        Vector2 spawnPointOnCircle = new Vector2(_spawnRadius * Random.onUnitSphere.x, _spawnRadius * Random.onUnitSphere.y); 
        return (Vector2)transform.position + spawnPointOnCircle + _playerMovement.GetDirection() * _offsetPlayerDir;
        //return (Vector2)transform.position + spawnPointOnCircle;
    }

    private void SpawnAsteroid()
    {
        GameObject asteroid = ObjectPooling.Instance.GetPooledAsteroid();

        if (asteroid == null)
        { return; }

        asteroid.transform.position = FindSpawnPosition();
        asteroid.SetActive(true);
        Vector2 giveDir = transform.position - asteroid.transform.position;
        asteroid.GetComponent<AsteroidMovementScript>().Setup(giveDir.normalized);
    }

    IEnumerator SpawnAsteroidAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SpawnAsteroid();
        StartCoroutine(SpawnAsteroidAfterDelay(_spawnTimer));
    }
}
