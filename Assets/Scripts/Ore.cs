using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ore : MonoBehaviour
{
    public List<GameObject> OreGraficObjects = new List<GameObject>();
    void Start()
    {
        Enum.TryParse("" + UnityEngine.Random.Range(-1, 2), out Type value);
        SetOreType(value);
    }

    public enum Type
    {
        Normal = -1,
        Red = 0,
        Green = 1,
        Blue = 2
    }

    Color red = new Color(1, 0.9181263f, 0.8156863f);
    Color green = new Color(0.8447818f, 1, 0.8160377f);
    Color blue = new Color(0.8156863f, 0.9952657f, 1);
    public void SetOreType(Type t)
    {
        //set random ore type
        foreach (GameObject go in OreGraficObjects)
        {
            go.SetActive(false);
        }
        GameObject oreGo = OreGraficObjects[UnityEngine.Random.Range(0, OreGraficObjects.Count)];
        oreGo.SetActive(true);

        //set color depending on type
        Color color;
        switch (t)
        {
            case Type.Red:
                color = red;
                break;
            case Type.Blue:
                color = blue;
                break;
            case Type.Green:
                color = green;
                break;
            default:
                color = Color.white;
                break;      
        }
        oreGo.GetComponent<Renderer>().material.color = color;
    }
}