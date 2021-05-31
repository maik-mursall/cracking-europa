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

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SpawnFirst();

    }

    public int crackLength = 5;
    public float crackAngleChange = 60f;
    public float segmentDistance = 0.5f;
    public void SpawnFirst()
    {
        GameObject go = SphereSurfaceRessourceSpawner.instance.SpawnObejctAtPosition(transform.position, segmentObject);
        go.GetComponent<RessourceCrackSegment>().initialize(this);
    }


    public void AddSegment(RessourceCrackSegment segment)
    {
        segments.Add(segment);
        //Debug.Log(segments.Count);
        //Debug.Log(lineRenderer);
        segmentPositions.Add(segment.transform.localPosition - transform.localPosition);
        lineRenderer.positionCount = crackLength;
        lineRenderer.SetPositions(segmentPositions.ToArray());
    }
}
