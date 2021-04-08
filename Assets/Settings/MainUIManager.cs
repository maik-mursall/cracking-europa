using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// must set maxTimeLeft first, timeLeftOver sets Timer and slider. Contains EndGame Object + BackToMenu Button
/// </summary>
public class MainUIManager : MonoBehaviour
{
    [Header("Init")]
    [SerializeField] Text cryptoText;
    [SerializeField] Text timeCounter;
    [SerializeField] Slider timeSlider;

    [Header("Endgame Stuff")]
    public GameObject EndgameObject;
    public Button BackToMenuText;

    /// <summary>
    /// total maximum time in milliseconds
    /// </summary>
    public float MaxTimeLeft { get { return timeSlider.maxValue; } set { timeSlider.maxValue = value; setCounterText(value); } }
    private float _timeLeftover = 0f;
    public float TimeLeftover { get { return _timeLeftover; } 
        set {
            timeSlider.value = value;
            _timeLeftover = value;
            setCounterText(value);
        } 
    }
    private void setCounterText (float milliseconds)
    {
        float v = milliseconds / 1000;
        int m = (int)v / 60;
        int s = (int)v % 60;
        timeCounter.text = "" + m + ":" + s;
    }
    private int _Crypto = 0;
    /// <summary>
    /// sets the UI Value
    /// </summary>
    public int Crypto { get { return _Crypto; } 
        set {
            _Crypto = value;
            cryptoText.text = "" + value.ToString("D8");
        } 
    }

    /*
    private void Start()
    {
        MaxTimeLeft = 1000 * 160;
        TimeLeftover = 1000 * 160;
        Crypto = 12345;
    }

    private void Update()
    {
        TimeLeftover = MaxTimeLeft - Time.time * 1000;
    }
    */
}
