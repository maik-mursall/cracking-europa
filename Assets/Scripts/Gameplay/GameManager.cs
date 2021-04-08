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

        public float Credits
        {
            get => _credits;
            set
            {
                _credits = value;
                creditsText.text = value.ToString("0000000.00");;
            }
        }

        [SerializeField] private TMP_Text creditsText;
        [SerializeField] private MainUIManager mainUIManager;


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
            gameIsRunning = false;
        }
    }
}