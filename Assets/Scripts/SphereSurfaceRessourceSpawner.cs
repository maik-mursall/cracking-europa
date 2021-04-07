using System;
using Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;

public class SphereSurfaceRessourceSpawner : MonoBehaviour
{
    [SerializeField]
    private float sphereRadius = 50f;

    [SerializeField] private Resource resource;

    [SerializeField] private bool spawnContinuously = false;
    [SerializeField] private float spawnDelay = 5f;
    private float _timeSinceLastSpawn;

    [SerializeField] private int amountToSpawnOnStart = 10;

    private void Start()
    {
        for (int i = 0; i < amountToSpawnOnStart; i++)
        {
            SpawnRessource();
        }
    }

    private void Update()
    {
        if (spawnContinuously)
        {
            _timeSinceLastSpawn += Time.deltaTime;

            if (_timeSinceLastSpawn >= spawnDelay)
            {
                SpawnRessource();
                _timeSinceLastSpawn = 0f;
            }
        }
    }
    
    private void SpawnRessource()
    {
        Vector3 position = Random.onUnitSphere * sphereRadius + transform.position;

        Instantiate(resource.prefab, position, Quaternion.identity, transform).transform.up = (position - transform.position).normalized;
    }
}
