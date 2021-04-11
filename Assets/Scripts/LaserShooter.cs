using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LaserShooter : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject impactEffects;
    [SerializeField] private Slider laserSlider;

    [Header("Audio Stuff")]
    [SerializeField] private List<AudioSource> laserAudioSources;
    [SerializeField] private AudioSource motors;
    [SerializeField] private AudioSource credits;

    private Camera _mainCamera;
    [Header("Values")]
    [SerializeField] private float harvestingSpeed = 100f;
    [SerializeField] private float rotationSpeedInverse = 0.5f;
    [SerializeField] private float laserEnergie = 100f;

    //[Range(0.1f, 5f)]
    [SerializeField] private float laserDrainPerSecond = 1f;
    //[Range(0.1f, 5f)]
    [SerializeField] private float laserEnergyRegenPerSecond = 1f;

    Quaternion lastRotation = Quaternion.identity;

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

            #region Energy
            lineRenderer.enabled = mousePressed && laserEnergie > 1f;
            if(mousePressed)
                laserEnergie -= Time.deltaTime * laserDrainPerSecond;
            else
                laserEnergie += Time.deltaTime * laserEnergyRegenPerSecond;

            laserEnergie = Mathf.Clamp(laserEnergie, 0, 100);
            laserSlider.value = laserEnergie; // setting energie UI
            #endregion

            /*if(ray hit)
                *  look at
                *  if(Mousepressed)
                *      activate laser
                *  else
                *      deactivate laser
                *      
            */
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {

                transform.parent.parent.DOLookAt(hit.point, rotationSpeedInverse);

                if (mousePressed && laserEnergie > 1f)
                {
                    lineRenderer.SetPosition(0, transform.position);

                    lineRenderer.SetPosition(1, hit.point);
                    impactEffects.transform.position = hit.point;
                    impactEffects.transform.rotation = Quaternion.LookRotation(hit.normal);
                    impactEffects.SetActive(true);
                    setLaserSound(true);

                    if (hit.transform.TryGetComponent(out Ore ore))
                    {
                        float amountHarvested = ore.HarvestOre(harvestingSpeed * Time.deltaTime);

                        Debug.Log($"Just harvested ${amountHarvested}");
                    }
                }
                else
                {
                    impactEffects.SetActive(false);
                    setLaserSound(false);
                    lineRenderer.enabled = false;
                }
            }
            else
            {
                //lineRenderer.SetPosition(1, ray.origin + ray.direction * 50f);
                impactEffects.SetActive(false);
                setLaserSound(false);
                lineRenderer.enabled = false;
            }
        }
        else
        {
            impactEffects.SetActive(false);
            setLaserSound(false);

            lineRenderer.enabled = false;
        }

        #region AudioMotors
        float pitch = 0.8f;
        float volume = 0.05f;
        Quaternion currentRotation = transform.parent.parent.localRotation;
        float motorPitchAdditive = (currentRotation.eulerAngles - lastRotation.eulerAngles).magnitude / 2 / 10;
        //Debug.Log(motorPitchAdditive);
        pitch = Mathf.Clamp(pitch + motorPitchAdditive, 0.6f, 0.85f);
        motors.volume = Mathf.Clamp(volume + motorPitchAdditive, 0, 0.1f) / 2;
        motors.pitch = pitch;
        if (currentRotation != lastRotation)
        {
            ActivateAudio(motors, true);
        }
        else
        {
            ActivateAudio(motors, false);
        }
        lastRotation = transform.parent.parent.localRotation;
        #endregion
    }

    private void ActivateAudio(AudioSource source, bool playing)
    {
        if (playing)
        {
            if (!source.isPlaying)
            {
                source.Play(0);
            }
        }
        else
        {
            source.Stop();
        }

    }
    private void setLaserSound(bool activated)
    {
        foreach (AudioSource source in laserAudioSources)
        {
            if (activated)
            {
                if (!source.isPlaying)
                {
                    //Debug.Log(source.clip.name);
                    source.Play(0);
                }
            }
            else
            {
                source.Stop();
            }

        }
    }
}
