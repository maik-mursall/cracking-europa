using System;
using Gameplay;
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
        if (GameManager.instance.gameIsRunning)
        {
            _currentElapsedTime += Time.deltaTime;
            
            timeText.text = TimeSpan.FromSeconds(time - _currentElapsedTime).ToString("mm':'ss");
            timeSlider.value = Mathf.Clamp01((time - _currentElapsedTime) / time);

            if (_currentElapsedTime >= time)
            {
                GameManager.instance.TimeRanOut();
            }
        }
    }
}
