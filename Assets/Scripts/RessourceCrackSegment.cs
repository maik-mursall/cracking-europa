using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceCrackSegment : MonoBehaviour
{
    private RessourceCrack origin;
    public void initialize(RessourceCrack origin)
    {
        this.origin = origin;
        origin.AddSegment(this);

        if(origin.segments.Count < origin.crackLength)
        {
            SpawnNext();
        }
    }
    private void SpawnNext()
    {
        float randAngle = Random.Range(-0.5f * origin.crackAngleChange, 0.5f * origin.crackAngleChange);
        
        float z = Mathf.Abs( Mathf.Cos(randAngle) * origin.segmentDistance);
        float x = Mathf.Sin(randAngle) * origin.segmentDistance;

        Vector3 newPos = transform.position + new Vector3(x, 0, z);
        Debug.DrawLine(transform.position, newPos, Color.red, 5f);

        Debug.Log("" + randAngle +", x::"+ x +", z::"+ z);
        GameObject go = SphereSurfaceRessourceSpawner.instance.SpawnObejctAtPosition(transform.TransformPoint( newPos), origin.segmentObject, transform.rotation);
        go.GetComponent<RessourceCrackSegment>().initialize(origin);
    }
}
