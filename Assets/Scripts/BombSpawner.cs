using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public Transform bombSpawnPosition;

    public Vector3 movementDirection = new Vector3();

    public GameObject BombPrefab;
    public Transform EuropaMoon;
    public Transform BombSpawn;
    public Transform RacineOrbit;

    public void LateUpdate()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            GameObject go = GameObject.Instantiate<GameObject>(BombPrefab, EuropaMoon.position, RacineOrbit.rotation);
            go.transform.parent = EuropaMoon.transform;

            Bomb b = go.GetComponentInChildren<Bomb>();
            b.Initialize(transform.forward, EuropaMoon, BombSpawn);
        }
    }
}
