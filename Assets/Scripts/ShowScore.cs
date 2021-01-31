using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public Text SceneNumber;
    public Text ScoreText;
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
        Single score = PlayerPrefs.GetFloat("level" + SceneNumber.text + "score");

        ScoreText.text = $"Score:  {score}";
    }
}
