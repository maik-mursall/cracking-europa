using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreUIManager : MonoBehaviour
{
    [Header("Listen")]
    [SerializeField] List<Text> raengeTexte;
    [SerializeField] List<Text> verdiensteTexte;
    [SerializeField] List<Text> commanderTexte;
    [SerializeField] List<Text> datumTexte;

    [SerializeField] bool test = false;


    public void SetCommander(int rang, int verdienst, string name, string datum)
    {
        rang--;
        if(rang == -1)
            raengeTexte[rang].text = "";
        else
            raengeTexte[rang].text = "#" + (rang +1);

        if (verdienst != 0)
            verdiensteTexte[rang].text = verdienst.ToString("D8");
        else
            verdiensteTexte[rang].text = "";

        commanderTexte[rang].text = name;
        datumTexte[rang].text = datum;
    }

    private void Start()
    {
        EmptyList();

        if (test) // debug code
        {
            for (int i = 1; i <= 10; i++)
            {
                int verdienst = i * i * i * i * i;
                SetCommander(i, verdienst, "Commander" + verdienst, "" + System.DateTime.Today);
            }
        }
    }

    public void EmptyList()
    {
        for (int i = 1; i <= 10; i++)
        {
            int verdienst = i * i * i * i * i;
            SetCommander(i, 0, "", "");
        }
    }
}
