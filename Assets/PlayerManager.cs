using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject, true);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public string currentPlayerName = "";
    public float currentPlayerScore = 0;
}
