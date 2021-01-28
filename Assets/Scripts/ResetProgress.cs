using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    public GameObject PanelWithButtons;
    public void Reset()
    {
        LevelButtonMark[] allButtons = PanelWithButtons.GetComponentsInChildren<LevelButtonMark>();

        for (int i=1; i<allButtons.Length+1; i++)
            PlayerPrefs.SetInt("level" + i, 0);

        foreach(LevelButtonMark button in allButtons)
        {
            Debug.Log(button);
            button.NotifyAboutReset();
        }

    }
}
