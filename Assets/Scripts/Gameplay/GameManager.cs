using System.Linq;
using Settings;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private float _credits;
        private string _commanderName = "Cooler Kommander";

        public bool gameIsRunning = true;

        public float Credits
        {
            get => _credits;
            set
            {
                _credits = value;
                creditsText.text = value.ToString("0000000.00");
                ;
            }
        }

        [SerializeField] private TMP_Text creditsText;
        [SerializeField] private MainUIManager mainUIManager;
        [SerializeField] private HighscoreUIManager highscoreUIManager;


        // Start is called before the first frame update
        void Start()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
            }

            instance = this;
        }

        public void AddCredits(float amount)
        {
            Credits += amount;
        }

        public void TimeRanOut()
        {
            mainUIManager.DisplayEndgameText("Cooler Kommander", Credits, 10);

            var highScores = HighscoreHelper.GetHighScores().ToList();

            highScores.Add(new HighscoreEntry(
                _commanderName,
                _credits,
                Time.time
            ));
            
            highScores.Sort((x, y) => x.score.CompareTo(y.score));

            var highScoreArray = highScores.ToArray();
            
            HighscoreHelper.SaveHighScores(highScoreArray);
            highscoreUIManager.DisplayHighscore(highScoreArray);

            gameIsRunning = false;
        }
    }
}