using UnityEngine;

public class HighscoreUIManager : MonoBehaviour
{
    [SerializeField] private GameObject highscoreEntryPrefab;
    [SerializeField] private Transform highscoreContainer;
    
    public void DisplayHighscore(HighscoreEntry[] entries)
    {
        for (int i = 0; i < entries.Length; i++)
        {
            Instantiate(highscoreEntryPrefab, highscoreContainer).GetComponent<HighscoreEntryUI>().PopulateEntry(entries[i], i + 1);
        }
    }
}
