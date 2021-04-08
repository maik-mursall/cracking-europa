using UnityEngine;
using UnityEngine.UI;

public class HighscoreEntryUI : MonoBehaviour
{
    [SerializeField] private Text rankText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text commanderNameText;
    [SerializeField] private Text dateText;

    public void PopulateEntry(HighscoreEntry entry, int rank)
    {
        rankText.text = rank.ToString();
        
        scoreText.text = entry.score.ToString("0.00");
        commanderNameText.text = entry.commanderName;
        dateText.text = entry.date.ToString("0.00");
    }
}
