using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEndlessScore : MonoBehaviour
{
    public void NotifyAboutNewScore(Single newScore)
    {
        Single currentScore = PlayerPrefs.GetFloat("endless");
        if(newScore>currentScore)
        {
            Debug.Log("Set new highscore: " + newScore);
            PlayerPrefs.SetFloat("endless", newScore);
        }
    }
}
