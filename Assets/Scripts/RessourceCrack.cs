using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Gameplay;

public class RessourceCrack : MonoBehaviour
{
    public List<RessourceCrackSegment> segments = new List<RessourceCrackSegment>();
    private List<Vector3> segmentPositions = new List<Vector3>();
    public GameObject segmentObject;

    private LineRenderer lineRenderer;
    public bool debug = true;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SpawnFirst();

        if(debug)
            debugFunc();
    }
    #region debug
    private void debugFunc()
    {
        for (int i = 0; i < 10; i++)
        {
            float randAngle = UnityEngine.Random.Range(-0.5f * this.crackAngleChange, 0.5f * this.crackAngleChange);
            randAngle = randAngle / 90 * Mathf.PI / 2;
            float z = Mathf.Cos(randAngle) * this.segmentDistance;
            float x = Mathf.Tan(randAngle) * z;

            Vector3 newPos = transform.position + new Vector3(x, 0, z);
            Debug.DrawLine(this.transform.position, newPos - (transform.position - this.transform.position), Color.red, 5f);
        }
    }
    #endregion
    public int crackLength = 5;
    public float crackAngleChange = 60f;
    public float segmentDistance = 0.5f;
    public void SpawnFirst()
    {
        SphereSurfaceRessourceSpawner europaSpawner = SphereSurfaceRessourceSpawner.instance;
        
        GameObject go = europaSpawner.SpawnObejctAtPosition(transform.position, segmentObject, Quaternion.LookRotation(UnityEngine.Random.insideUnitSphere, transform.position - europaSpawner.transform.position));
        go.transform.parent = transform;
        go.GetComponent<RessourceCrackSegment>().initialize(this);
    }


    public void AddSegment(RessourceCrackSegment segment)
    {
        segments.Add(segment);
        //Debug.Log(segments.Count);
        //Debug.Log(lineRenderer);
        segmentPositions.Add(segment.transform.localPosition);
        lineRenderer.positionCount = crackLength;
        lineRenderer.SetPositions(segmentPositions.ToArray());
    }
}
