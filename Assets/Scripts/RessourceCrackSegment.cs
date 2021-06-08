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
        randAngle = randAngle / 90 * Mathf.PI / 2;
        float z = Mathf.Cos(randAngle) ;
        float x = Mathf.Sin(randAngle) ;
        Vector3 rotation = transform.TransformDirection(new Vector3(x, 0, z));
        Vector3 newPos = transform.position+ rotation * origin.segmentDistance;
        Debug.DrawLine(transform.position, newPos, Color.red, 5f);

        //Debug.Log("" + randAngle +", x::"+ x +", z::"+ z);
        Transform europa = SphereSurfaceRessourceSpawner.instance.transform;
        GameObject go = SphereSurfaceRessourceSpawner.instance.SpawnObejctAtPosition(  newPos, origin.segmentObject, Quaternion.LookRotation(rotation, (transform.position - europa.position).normalized));
        go.transform.parent = origin.transform;
        go.GetComponent<RessourceCrackSegment>().initialize(origin);
    }
}
