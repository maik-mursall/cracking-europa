using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timeText;
    [SerializeField] private Slider timeSlider;

    [SerializeField] private float time;

    private float _currentElapsedTime;
    
    void Update()
    {
        _currentElapsedTime += Time.deltaTime;
        
        timeText.text = TimeSpan.FromSeconds(time - _currentElapsedTime).ToString("mm':'ss");
        timeSlider.value = Mathf.Clamp01((time - _currentElapsedTime) / time);
    }
}
