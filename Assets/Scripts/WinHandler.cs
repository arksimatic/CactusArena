using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinHandler : MonoBehaviour
{
    public int thisSceneId;
    public int thisLevelId;
    public GameObject PopupWindowPause;
    public GameObject PopupWindowWin;
    public GameObject PopupWindowLose;
    public Text countdown;
    private GameObject player;

    private bool paused = false;
    private bool alreadyWon = false;
    private bool alreadyLost = false;

    void Start()
    {
        player = GameObject.Find("objPlayer");
    }
    private void PauseSwitch()
    {
        if (paused)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;

        paused = !paused;
    }
    public void WinPopupWindow()
    {
        //this is off due to changing Time.timeScale in other scripts
        PauseSwitch(); 

        //Time.timeScale = 1;
        PopupWindowWin.SetActive(true);

        PlayerPrefs.SetInt("Level" + thisLevelId.ToString(), 1);
    }

    public void LosePopupWindow()
    {
        PauseSwitch();

        if(thisSceneId == 14) //endless
        {
            //it notify SaveEndlessScore about new score
            this.gameObject.GetComponent<SaveEndlessScore>().NotifyAboutNewScore(Convert.ToSingle(GameObject.Find("txtCountdown").GetComponent<Text>().text));
        }

        PopupWindowLose.SetActive(true);
    }

    public void PausePopupWindow()
    {
        PauseSwitch();

        PopupWindowPause.SetActive(true);
    }

    public void ReloadGame()
    {
        Debug.Log("reload");

        GetComponent<ChangeScene>().LoadOtherScene();

        ReturnToTheGame();
    }

    public void ExitGame()
    {
        PauseSwitch();

        Debug.Log("change scene to lvl selector");
    }

    public void ReturnToTheGame()
    {
        PauseSwitch();

        PopupWindowWin.SetActive(false);
        PopupWindowLose.SetActive(false);
        PopupWindowPause.SetActive(false);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
                PausePopupWindow();
            else
                ReturnToTheGame();
        }

        if(countdown.text == "0,00")
        {
            if (!alreadyWon)
            {
                alreadyWon = true;
                PlayerPrefs.SetInt("level" + thisLevelId, 1);
                AudioManager.Instance.Play("victory");
                WinPopupWindow();
            }
        }

        if(player.GetComponent<Player>().Health <= 0)
        {
            if (!alreadyLost && !alreadyWon)
            {
                alreadyLost = true;
                LosePopupWindow();
            }
        }
    }
}
