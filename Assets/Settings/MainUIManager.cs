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
    [SerializeField] Text EndgameText;
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

    public enum GameAbschluss
    {
        gut,
        mittel,
        schlecht
    }
    public void SetEndgameText(string commanderName, int crypto, int anzahlMonate, GameAbschluss gameAbschluss)
    {
        string text =
        "Commander " + commanderName + " verdiente "+crypto+" Crypto \n\n" +
        " Er wirkte " + anzahlMonate + " Europa- Monate auf der Racine-II und crackte aus Europa wertvolle Metalle im Wert von "+crypto+" Crypto. " + "\n\n";
        switch (gameAbschluss)
        {
            case GameAbschluss.gut:
                text += "FLORAISON und die Investoren danken für seine herausragende Gewinnerhöhung.";
                break;
            case GameAbschluss.mittel:
                text += "mittel Leistung";
                break;
            case GameAbschluss.schlecht:
                text += "FLORAISON und die Investoren danken für seine Stetige Bemühung im Rahmen seiner begrenzten Möglichkeiten.";
                break;
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
