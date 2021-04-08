using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    /// <summary>
    /// must set maxTimeLeft first, timeLeftOver sets Timer and slider. Contains EndGame Object + BackToMenu Button
    /// </summary>
    public class MainUIManager : MonoBehaviour
    {
        [Header("Endgame Stuff")]
        public GameObject endgameObject;
        [SerializeField] Text endgameText;
        public Button backToMenuText;

        [SerializeField] private float midThreshold = 10000f;
        [SerializeField] private float goodThreshold = 1000000f;

        public enum GameAbschluss
        {
            Gut,
            Mittel,
            Schlecht
        }
        public void DisplayEndgameText(string commanderName, float crypto, int anzahlMonate)
        {
            GameAbschluss gameAbschluss = crypto >= midThreshold ? (crypto >= goodThreshold ? GameAbschluss.Gut : GameAbschluss.Mittel) : GameAbschluss.Schlecht;
            
            string text =
                $"Commander {commanderName} verdiente {crypto} Crypto"
                + $"\n\n Er wirkte {anzahlMonate} Europa- Monate auf der Racine-II und crackte aus Europa wertvolle Metalle im Wert von {crypto:0.00} Crypto. \n\n";
            switch (gameAbschluss)
            {
                case GameAbschluss.Gut:
                    text += "FLORAISON und die Investoren danken für seine herausragende Gewinnerhöhung.";
                    break;
                case GameAbschluss.Mittel:
                    text += "mittel Leistung";
                    break;
                case GameAbschluss.Schlecht:
                    text += "FLORAISON und die Investoren danken für seine Stetige Bemühung im Rahmen seiner begrenzten Möglichkeiten.";
                    break;
            }

            endgameText.text = text;
            endgameObject.SetActive(true);
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
}
