using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject nameDialogue;
    [SerializeField] GameObject mainButtons;
    [SerializeField] GameObject highscore;
    HighscoreUIManager highscoreManager;


    [SerializeField] Text nameField;
    public void PlayClicked()
    {
        nameDialogue.SetActive(true);
    }
    public void NameConfimed()
    {
        PlayerManager.instance.currentPlayerName = nameField.text;
        AppManager.instance.OpenMainScene();
    }
    public void NameCancelled()
    {
        if (nameDialogue.activeSelf)
            nameDialogue.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            if (nameDialogue.activeSelf)
                nameDialogue.SetActive(false);
        }
    }

    public void HighScoreClicked()
    {
        highscore.SetActive(true);
        mainButtons.SetActive(false);
        highscoreManager = highscore.GetComponent<HighscoreUIManager>();
        highscoreManager.DisplayHighscore(HighscoreHelper.GetHighScores());
    }
    public void BackToMenu()
    {
        highscore.SetActive(false);
        mainButtons.SetActive(true);
    }
    public void QuitClicked()
    {
        AppManager.instance.ExitApp();
    }
}
