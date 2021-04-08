using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private float _credits;

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
    }
}