using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    public GameObject PanelWithButtons;
    public GameObject Endless;
    public void Reset()
    {
        LevelButtonMark[] allButtons = PanelWithButtons.GetComponentsInChildren<LevelButtonMark>();
        ShowScore[] allButtonsScore = PanelWithButtons.GetComponentsInChildren<ShowScore>();


        for (int i = 1; i < allButtons.Length + 1; i++)
        {
            PlayerPrefs.SetInt("level" + i, 0);
            PlayerPrefs.SetFloat($"level{i}score", 0f);
        }
        PlayerPrefs.SetFloat("endless", 0.00f);

        foreach (LevelButtonMark button in allButtons)
        {
            Debug.Log(button);
            button.NotifyAboutReset();
        }

        foreach(ShowScore score in allButtonsScore)
        {
            score.NotifyAboutScoreChange();
        }
        Endless.GetComponent<ShowBestScore>().NotifyAboutScoreChange();
    }
}
