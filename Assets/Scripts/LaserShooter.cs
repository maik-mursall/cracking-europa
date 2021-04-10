using System;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject impactEffects;

    private Camera _mainCamera;

    [SerializeField] private float harvestingSpeed = 100f;

    private void Start()
    {
        _mainCamera = Camera.main;
        impactEffects.SetActive(false);
    }
    public void EndIntro(Camera maincamera)
    {
        _mainCamera = maincamera;
    }

    private void LateUpdate()
    {
        if (lineRenderer)
        {
            lineRenderer.enabled = Input.GetMouseButton(0);
            
            if (lineRenderer.enabled)
            {
                lineRenderer.SetPosition(0, transform.position);
                
                Ray ray = _mainCamera.ScreenPointToRay (Input.mousePosition);

                if (Physics.Raycast (ray, out RaycastHit hit, Mathf.Infinity)) 
                {
                    lineRenderer.SetPosition(1, hit.point);
                    impactEffects.transform.position = hit.point;
                    impactEffects.transform.rotation = Quaternion.LookRotation(hit.normal);
                    impactEffects.SetActive(true);

                    if (hit.transform.TryGetComponent(out Ore ore))
                    {
                        float amountHarvested = ore.HarvestOre(harvestingSpeed * Time.deltaTime);
                        
                        Debug.Log($"Just harvested ${amountHarvested}");
                    }
                }
                else
                {
                    lineRenderer.SetPosition(1, ray.origin + ray.direction * 50f);
                }
            }
            else
            {
                impactEffects.SetActive(false);
            }
        }
    }
}
