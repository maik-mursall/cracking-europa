using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class RessourceCrackSegment : Ore
{
    private RessourceCrack origin;
    public float health = 100f;
    public float timeTillRefreeze = 5f;
    private float timeSinceLiquid = 0f;
    private harvestState _harvestState = harvestState.frozen;
    public harvestState currentState { get { return _harvestState; } 
        set
        {
            _harvestState = value;
            Renderer renderer = GetComponent<Renderer>();
            switch (_harvestState)
            {
                case harvestState.frozen:
                    renderer.material.color = colorFrozen;
                    break;
                case harvestState.fluid:
                    renderer.material.color = colorfluid;
                    break;
                case harvestState.empty:
                    renderer.material.color = colorEmpty;
                    break;
            }
        } 
    }
    private Color colorFrozen = Color.blue;
    private Color colorfluid = Color.green;
    private Color colorEmpty = Color.black;
    public enum harvestState
    {
        frozen,
        fluid,
        empty
    }
    #region initAndSpawn
    public void initialize(RessourceCrack origin, float eulerAngle = 0, Vector3 upDirection = new Vector3())
    {
        //transform.up = upDirection;
        //transform.Rotate(new Vector3(0,eulerAngle,0));
        transform.rotation = Quaternion.AngleAxis(eulerAngle, upDirection);

        this.origin = origin;
        origin.AddSegment(this);

        if(origin.segments.Count < origin.crackLength)
        {
            SpawnNext();
        }
    }
    private void SpawnNext()
    {
        //calculate new spawn direction based on angle range
        float randAngle = Random.Range(-0.5f * origin.crackAngleChange, 0.5f * origin.crackAngleChange);
        randAngle = randAngle / 90 * Mathf.PI / 2;
        float z = Mathf.Cos(randAngle);
        float x = Mathf.Sin(randAngle);

        Transform europa = SphereSurfaceRessourceSpawner.instance.transform;
        Vector3 globalUpDirection = (transform.position - europa.position).normalized;
        // calc new position
        Vector3 directionForNextSpawn = transform.TransformDirection(new Vector3(x, 0, z));
        Vector3 spawnPosInfrontSegment = transform.position + directionForNextSpawn * origin.segmentDistance;
        Debug.DrawLine(transform.position, spawnPosInfrontSegment, Color.red, 5f);
        Debug.DrawLine(transform.position, transform.position + globalUpDirection, Color.green, 5f);

        //Debug.Log("" + randAngle +", x::"+ x +", z::"+ z);
      
        Quaternion q = Quaternion.identity;
        q.SetLookRotation(spawnPosInfrontSegment.normalized, globalUpDirection);
        

        GameObject go = SphereSurfaceRessourceSpawner.instance.SpawnObejctAtPosition( spawnPosInfrontSegment, origin.segmentObject, Quaternion.LookRotation(spawnPosInfrontSegment.normalized, globalUpDirection));
        go.transform.parent = origin.transform;
        go.GetComponent<RessourceCrackSegment>().initialize(origin, randAngle, globalUpDirection);
    }
    #endregion
    public override float HarvestOre(float mAmount)
    {
        Debug.Log("Harvesting - " + gameObject.name + ", amount:" + mAmount);
        float harvestAmount = 0;
        switch (currentState)
        {
            case harvestState.frozen:
                health -= mAmount;
                //GetComponent<Renderer>().material.color =  health / 100;
                if(health < 0)
                {
                    currentState = harvestState.fluid;
                }
                if (checkCrackOpen())
                {
                    harvestAmount = mAmount;
                }
                else harvestAmount = 0;
                break;

            case harvestState.fluid:
                harvestAmount = 0;
                break;

            case harvestState.empty:
                harvestAmount = 0;
                break;
            default:
                harvestAmount = 0;
                break;
        }
        GameManager.instance.AddCredits(harvestAmount);
        return harvestAmount;
    }
    protected override void Start()
    {
        
    }
    protected virtual void Update()
    {
        if(currentState == harvestState.fluid)
        {
            timeSinceLiquid += Time.deltaTime;
            if(timeSinceLiquid >= timeTillRefreeze)
            {
                currentState = harvestState.frozen;
            }
        }
    }
    private bool checkCrackOpen()
    {
        int index = origin.segments.IndexOf(this);
        RessourceCrackSegment upperSegment = null;
        RessourceCrackSegment lowerSegment = null;
        try { 
            upperSegment = origin.segments[index + 1];
            lowerSegment = origin.segments[index - 1];
        } catch{ Debug.Log("segment null-- upper:" + upperSegment + ", lower" + lowerSegment); }

        if (upperSegment == null || upperSegment.currentState == harvestState.empty || upperSegment.currentState == harvestState.fluid)
        {
            return true;
        }
        if (lowerSegment == null || lowerSegment.currentState == harvestState.empty || lowerSegment.currentState == harvestState.fluid)
        {
            return true;
        }
        return false;
    }
}
