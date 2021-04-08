using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class ResourceUI : MonoBehaviour
    {
        private float _mAmount;
        public float Amount
        {
            set
            {
                _mAmount = value;
                UpdateUI();
            }
            get => _mAmount;
        }

        [SerializeField] private Slider amountSlider;
        [SerializeField] private TMP_Text nameText;

        public void SetResourceName(string resourceName)
        {
            nameText.text = resourceName;
        }

        private void UpdateUI()
        {
            amountSlider.value = Mathf.Clamp01(_mAmount / 10000f);
        }
    }
}
