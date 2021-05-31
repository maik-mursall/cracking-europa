using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Transform europa;

    public float startSpeed;
    public float gravityFactor;
    public Vector3 velocity;

    public float destroyDelay = 20f;

    public float randomRotationSpeed = 0.5f;

    public void Initialize (Vector3 moveDirection, Transform europa, Transform spawnPos)
    {
        velocity = moveDirection;
        transform.parent.transform.parent = europa;
        transform.position = spawnPos.position;

        rotationEulers = Random.onUnitSphere;

        GameObject.Destroy(this, destroyDelay);
    }

    private Vector3 rotationEulers;
    private void Update()
    {
        transform.position += velocity * startSpeed * Time.deltaTime;
        transform.Rotate(rotationEulers * Time.deltaTime * randomRotationSpeed);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);

        if (other.gameObject.CompareTag("Europa"))
        {
            SphereSurfaceRessourceSpawner.instance.SpawnRessourceAtPosition(transform.position);
            Destroy(transform.parent.gameObject);
        }

    }
}
