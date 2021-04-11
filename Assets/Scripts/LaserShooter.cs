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
        if (lineRenderer && Gameplay.GameManager.instance.gameIsRunning)
        {
            bool mousePressed = Input.GetMouseButton(0);
            lineRenderer.enabled = mousePressed && laserEnergie > 1f;
            if(mousePressed)
                laserEnergie -= Time.deltaTime * laserDrainPerSecond;
            else
                laserEnergie += Time.deltaTime * laserEnergyRegenPerSecond;

            laserEnergie = Mathf.Clamp(laserEnergie, 0, 100);
            laserSlider.value = laserEnergie; // setting energie UI

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {

                transform.parent.parent.DOLookAt(hit.point, rotationSpeedInverse);

                if (mousePressed)
                {
                    lineRenderer.SetPosition(0, transform.position);

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
                    impactEffects.SetActive(false);
                }
            }
            else
            {
                //lineRenderer.SetPosition(1, ray.origin + ray.direction * 50f);
                impactEffects.SetActive(false);
            }

            /*if(ray hit)
             *  look at
             *  if(Mousepressed)
             *      activate laser
             *  else
             *      deactivate laser
             *      
            */
        }
        else
        {
            impactEffects.SetActive(false);
            lineRenderer.enabled = false;
        }
    }
}
