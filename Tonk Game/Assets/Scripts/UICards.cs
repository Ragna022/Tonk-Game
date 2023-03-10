using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class UICards : MonoBehaviour
{
    public Image img_Cards;
    public GameObject gob_FrontCard;
    public int scoreCards;
    
    void Start()
    {
        /*string str = img_Cards.sprite.name;
        //scoreCards = int.Parse(str.Substring(1));
        string scoreStr = str.Substring(1);*/


        /*if (int.TryParse(scoreStr, out int score))
        {
            scoreCards = score;
        }
        else
        {
            Debug.LogError("Failed to parse score: " + scoreStr);
        }
        string str = img_Cards.sprite.name;
        try
        {
            scoreCards = int.Parse(str.Substring(1));
        }
        catch (FormatException e)
        {
            Debug.LogError("Failed to parse score: " + e.Message);
        }*/


        string str = img_Cards.sprite.name;
        if (!string.IsNullOrEmpty(str))
        {
            int.TryParse(str.Substring(1), out scoreCards);
        }
        else
        {
            Debug.LogError("Failed to parse score: Input string was empty");
        }





    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
