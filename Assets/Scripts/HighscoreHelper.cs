using System;
using UnityEngine;

[Serializable]
public class HighscoreEntry
{
    public string commanderName;
    public float score;
    public float date;

    public HighscoreEntry(string mCommanderName, float mScore, float mDate)
    {
        commanderName = mCommanderName;
        score = mScore;
        date = mDate;
    }
}

public class HighscoreHelper
{
    public static HighscoreEntry[] GetHighScores()
    {
        int amount = PlayerPrefs.GetInt("highscore_length", 0);

        HighscoreEntry[] entries = new HighscoreEntry[amount];

        for (int i = 0; i < amount; i++)
        {
            var key = $"highscore_{i}";
            if (PlayerPrefs.HasKey(key))
            {
                entries[i] = JsonUtility.FromJson<HighscoreEntry>(PlayerPrefs.GetString(key));
            }
        }
        
        return entries;
    }

    public static void SaveHighScores(HighscoreEntry[] entries)
    {
        var amount = entries.Length;
        PlayerPrefs.SetInt("highscore_length", amount);

        for (int i = 0; i < amount; i++)
        {
            var key = $"highscore_{i}";
            JsonUtility.FromJson<HighscoreEntry>(PlayerPrefs.GetString(key));

            PlayerPrefs.SetString(key, JsonUtility.ToJson(entries[i]));
        }
    }
}