using System;
using Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;

public class SphereSurfaceRessourceSpawner : MonoBehaviour
{
    public static SphereSurfaceRessourceSpawner instance;
    [SerializeField]
    private float sphereRadius = 50f;

    [SerializeField] private Resource[] resource;

    [SerializeField] private bool spawnContinuously = false;
    [SerializeField] private float spawnDelay = 5f;
    private float _timeSinceLastSpawn;

    [SerializeField] private int amountToSpawnOnStart = 10;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            DestroyImmediate(this);

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
        Vector3 ownPosition = transform.position;
        Vector3 randomPositionOnSphere = Random.onUnitSphere * sphereRadius + ownPosition;

        Instantiate(resource[Random.Range(0, resource.Length)].prefab, randomPositionOnSphere, Quaternion.identity, transform).transform.up = (randomPositionOnSphere - ownPosition).normalized;
    }

    public void SpawnRessourceAtPosition(Vector3 pos)
    {
        Vector3 ownPosition = transform.position;
        Vector3 position = (pos - ownPosition).normalized * sphereRadius + ownPosition;

        Instantiate(resource[Random.Range(0, resource.Length)].prefab, position, Quaternion.identity, transform).transform.up = (position - ownPosition).normalized;
    }

    public GameObject SpawnObejctAtPosition(Vector3 pos, GameObject spawnable) => SpawnObejctAtPosition(pos, spawnable, Quaternion.identity);
    public GameObject SpawnObejctAtPosition(Vector3 pos, GameObject spawnable, Quaternion rotation)
    {
        Vector3 ownPosition = transform.position;
        Vector3 position = (pos - ownPosition).normalized * sphereRadius + ownPosition;
        GameObject go = Instantiate(spawnable, position, rotation, transform);
        //go.transform.up = (position - ownPosition).normalized;
        return go;
    }
}
