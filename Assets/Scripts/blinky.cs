using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinky : MonoBehaviour
{
    public List<Light> lights = new List<Light>();

    public float blinkDelay = 1;
    public float blinkDuration = 0.05f;
    public int blinkSeries = 2;
    public float seriesDelay = 0.1f;

    // Update is called once per frame
    void Start()
    {
        blinkOnce();
        BlinkSeries();
    }

    void BlinkSeries()
    {
        StartCoroutine(Utils.ExecuteAfterSeconds(blinkDelay, () =>
        {
            for (int i = 0; i < blinkSeries; i++)
            {
                StartCoroutine( Utils.ExecuteAfterSeconds(i * seriesDelay, () =>
                {
                    blinkOnce();
                }));
            }
            BlinkSeries();
        }));
    }

    public void blinkOnce()
    {
        toggleLights(true);
        StartCoroutine(Utils.ExecuteAfterSeconds(blinkDuration, () =>
        {
            toggleLights(false);
        }));
    }

    public void toggleLights(bool on)
    {
        foreach(Light l in lights)
        {
            l.enabled = on;
        }
    }
}
