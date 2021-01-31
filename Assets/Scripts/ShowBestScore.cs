using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBestScore : MonoBehaviour
{
    private Text scoreText;
    private Single currentScore;
    void Start()
    {
        SetScore();
    }

    public void NotifyAboutScoreChange()
    {
        SetScore();
    }

    private void SetScore()
    {
        scoreText = this.gameObject.GetComponent<Text>();
        currentScore = PlayerPrefs.GetFloat("endless");
        scoreText.text = $"Best: {currentScore}s";
    }
}
