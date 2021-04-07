using Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;

public class SphereSurfaceRessourceSpawner : MonoBehaviour
{
    [SerializeField]
    private float sphereRadius = 50f;

    [SerializeField] private Ressource ressource;

    [SerializeField] private float spawnDelay = 5f;
    private float _timeSinceLastSpawn;

    private void SpawnRessource()
    {
        Vector3 position = Random.onUnitSphere * sphereRadius + transform.position;

        Instantiate(ressource.prefab, position, Quaternion.identity, transform).transform.up = (position - transform.position).normalized;
    }

    private void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn >= spawnDelay)
        {
            SpawnRessource();
            _timeSinceLastSpawn = 0f;
        }
    }
}
