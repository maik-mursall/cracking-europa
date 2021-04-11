using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    /// <summary>
    /// must set maxTimeLeft first, timeLeftOver sets Timer and slider. Contains EndGame Object + BackToMenu Button
    /// </summary>
    public class MainUIManager : MonoBehaviour
    {
        public static MainUIManager instance;
        [Header("Endgame Stuff")]
        public GameObject endgameObject;
        [SerializeField] Text endgameText;
            
        [SerializeField] private float midThreshold = 10000f;
        [SerializeField] private float goodThreshold = 1000000f;

        public enum GameAbschluss
        {
            Gut,
            Mittel,
            Schlecht
        }

        public void BackToMenu()
        {
            AppManager.instance.OpenMenu();
        }
        public void DisplayEndgameText(string commanderName, float crypto, int anzahlMonate)
        {
            goodThreshold = HighscoreHelper.GetHighScores()[0].score;
            GameAbschluss gameAbschluss = crypto >= midThreshold ? (crypto >= goodThreshold ? GameAbschluss.Gut : GameAbschluss.Mittel) : GameAbschluss.Schlecht;
            
            string text =
                $"Commander {commanderName} \noperated 1 months onboard RACINE-II, \nsmelting and selling metallic salts out of Europa."
                + $"\n\n {commanderName} earned {crypto:0.00} Crypto. \n\n"
                + "Mother station FLORAISON and the investors ";
            switch (gameAbschluss)
            {
                case GameAbschluss.Gut:
                    text += "congratulate on this outstanding achievement in service of humanity";
                    break;
                case GameAbschluss.Mittel:
                    text += "re thankful for your reliable effort in service of humanity.";
                    break;
                case GameAbschluss.Schlecht:
                    text += "value this effort considering the obvious limits of capabilities. Some investors are laying low to avoid their liabilities.";
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
