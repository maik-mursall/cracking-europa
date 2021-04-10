using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LaserShooter : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject impactEffects;
    [SerializeField] private Slider laserSlider;

    private Camera _mainCamera;

    [SerializeField] private float harvestingSpeed = 100f;
    [SerializeField] private float rotationSpeedInverse = 0.5f;
    [SerializeField] private float laserEnergie = 100f;

    //[Range(0.1f, 5f)]
    [SerializeField] private float laserDrainPerSecond = 1f;
    //[Range(0.1f, 5f)]
    [SerializeField] private float laserEnergyRegenPerSecond = 1f;

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
            bool laserActive = Input.GetMouseButton(0);
            lineRenderer.enabled = laserActive && laserEnergie > 1f;
            if(laserActive)
                laserEnergie -= Time.deltaTime * laserDrainPerSecond;
            else
                laserEnergie += Time.deltaTime * laserEnergyRegenPerSecond;

            laserEnergie = Mathf.Clamp(laserEnergie, 0, 100);
            laserSlider.value = laserEnergie; // setting energie UI
            
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

                    transform.parent.parent.DOLookAt(hit.point, rotationSpeedInverse);
                    

                    if (hit.transform.TryGetComponent(out Ore ore))
                    {
                        float amountHarvested = ore.HarvestOre(harvestingSpeed * Time.deltaTime);
                        
                        Debug.Log($"Just harvested ${amountHarvested}");
                    }
                }
                else
                {
                    lineRenderer.SetPosition(1, ray.origin + ray.direction * 50f);
                    impactEffects.SetActive(false);
                }
            }
            else
            {
                impactEffects.SetActive(false);
            }
        }
    }
}
