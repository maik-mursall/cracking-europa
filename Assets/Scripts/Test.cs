using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform europa;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            float randAngle = Random.Range(-0.5f * 30, 0.5f * 30);
            randAngle = randAngle / 90 * Mathf.PI / 2;
            float z = Mathf.Cos(randAngle);
            float x = Mathf.Sin(randAngle);

            // calc new position
            Vector3 directionForNextSpawn = transform.TransformDirection(new Vector3(x, 0, z));
            Vector3 newPos = transform.position + directionForNextSpawn * 1;
            Debug.DrawLine(transform.position, newPos, Color.red, 0.05f);
            Debug.DrawLine(transform.position, transform.position + (transform.position - europa.position).normalized, Color.green, 0.05f);
        }
        transform.up = (transform.position - europa.position).normalized;
    }
}
