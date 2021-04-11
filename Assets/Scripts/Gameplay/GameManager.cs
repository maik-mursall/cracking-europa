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

        public bool gameIsRunning = true;
        public bool orbitsAreMoving = true;

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
        [SerializeField] private Timer timer;
        [SerializeField] private Intro intro;

        // Start is called before the first frame update
        void Start()
        {
            if (instance != null)
            {
                DestroyImmediate(gameObject);
            }

            instance = this;

            intro.StartIntro();
        }

        public void AddCredits(float amount)
        {
            Credits += amount;
        }

        public void TimeRanOut()
        {
            mainUIManager.DisplayEndgameText(PlayerManager.instance.currentPlayerName, Credits, 10);

            var highScores = HighscoreHelper.GetHighScores().ToList();

            highScores.Add(new HighscoreEntry(
                PlayerManager.instance.currentPlayerName,
                _credits,
                Time.time
            ));
            
            highScores.Sort((x, y) => y.score.CompareTo(x.score));

            var highScoreArray = highScores.ToArray();
            
            HighscoreHelper.SaveHighScores(highScoreArray);
            highscoreUIManager.DisplayHighscore(highScoreArray);

            gameIsRunning = false;
        }
    }
}