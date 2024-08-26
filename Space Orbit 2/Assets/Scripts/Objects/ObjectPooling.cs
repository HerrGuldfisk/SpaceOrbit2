using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;

    private List<GameObject> _pooledAsteroids = new List<GameObject>();

    private int _poolAsteroidAmount = 10;

    [SerializeField] private GameObject _asteroidPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < _poolAsteroidAmount; i++) 
        {
            GameObject obj = Instantiate(_asteroidPrefab);
            obj.SetActive(false);
            _pooledAsteroids.Add(obj);
        }
    }

    public GameObject GetPooledAsteroid()
    {
        for (int i = 0; i < _pooledAsteroids.Count; i++)
        {
            if (!_pooledAsteroids[i].activeInHierarchy)
            {
                return _pooledAsteroids[i];
            }
        }

        return null;
    }
}
