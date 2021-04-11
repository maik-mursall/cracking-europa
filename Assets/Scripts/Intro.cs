using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Intro : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas flyTextCanvas;

    public GameObject flyPosition;

    public Camera introCam;
    public Camera mainCam;
    public Camera endposCam;

    public GameObject racineStation;

    public LaserShooter laserShooter;

    public List<GameObject> IntroObjects = new List<GameObject>();

    public float flightDuration = 10;
    public float lookUpDuration = 10;
    public float rotationDuration = 2;
    public float introDuration = 40;

    Quaternion startRot;
    Vector3 startPos;

    public bool introRunning = false;
    private void Update()
    {
        if (introRunning)
        {
            IntroUpdate();
        }
    }

    private bool lookingAt = false;
    private void IntroUpdate()
    {
        if(lookingAt)
            introCam.transform.LookAt(racineStation.transform);
        if (Input.inputString != "")
            EndIntro();
    }

    public void StartIntro()
    {
        introRunning = true;
        mainCam.gameObject.SetActive(false);
        endposCam.gameObject.SetActive(false);
        introCam.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);

        Gameplay.GameManager.instance.gameIsRunning = false;
        Gameplay.GameManager.instance.orbitsAreMoving = false;

        startRot = introCam.transform.rotation;
        startPos = introCam.transform.position;

        laserShooter = MonoBehaviour.FindObjectOfType<LaserShooter>();

        flyTextCanvas.transform.DOMove(flyPosition.transform.position, lookUpDuration +2).SetEase(Ease.Linear);
        StartCoroutine(upDelay());

        StartCoroutine(EndIntroAfter());
    }

    IEnumerator upDelay()
    {
        yield return new WaitForSeconds(lookUpDuration);
        Flight();
    }

    public void Flight()
    {
        Gameplay.GameManager.instance.orbitsAreMoving = true;
        introCam.transform.DOLocalMove(endposCam.transform.localPosition, flightDuration);
        introCam.transform.DOLookAt(racineStation.transform.position, rotationDuration, AxisConstraint.None, Vector3.up);
        StartCoroutine(LookatAfter());
    }
    IEnumerator LookatAfter()
    {
        yield return new WaitForSeconds(2);
        lookingAt = true;
    }
    IEnumerator EndIntroAfter()
    {
        yield return new WaitForSeconds(introDuration);
        EndIntro();
    }

    public void EndIntro()
    {
        introCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(true);

        laserShooter.EndIntro(mainCam);

        foreach(GameObject go in IntroObjects)
        {
            go.SetActive(false);
        }

        Gameplay.GameManager.instance.gameIsRunning = true;
        Gameplay.GameManager.instance.orbitsAreMoving = true;
    }
}
