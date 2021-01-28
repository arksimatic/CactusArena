using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//add this bibliothek for the working with scenes
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int indexSceneToLoad;
    public Image blackScreen;
    public GameObject mainObjectToTurnOn;
    public float fadingTime = 0.3f;
    public void LoadOtherScene()
    {
        Time.timeScale = 1;
        FadeIn();
        //if (indexSceneToLoad == 1) GameType.SetAllFalse();
        StartCoroutine("LoadSceneAfterTime", indexSceneToLoad);
    }

    public void LoadNextScene()
    {
        Time.timeScale = 1;
        FadeIn();

        //remember to delete this possibility in the last level (or change to back to main menu)
        StartCoroutine("LoadSceneAfterTime", indexSceneToLoad+1);
    }

    public void LoadLevelSelector()
    {
        Time.timeScale = 1;
        FadeIn();
        StartCoroutine("LoadSceneAfterTime", 1);
    }

    void Start()
    {
        FadeOut();
    }

    void FadeIn()
    {
        mainObjectToTurnOn.SetActive(true);
        blackScreen.canvasRenderer.SetAlpha(0);
        blackScreen.CrossFadeAlpha(1, fadingTime, false);
    }

    void FadeOut()
    {
        mainObjectToTurnOn.SetActive(true);
        blackScreen.canvasRenderer.SetAlpha(1);
        blackScreen.CrossFadeAlpha(0, fadingTime, false);
        StartCoroutine("DisactiveBlackscreenAfterTime", fadingTime);
    }

    IEnumerator LoadSceneAfterTime(int indexOfSceneToLoad)
    {
        yield return new WaitForSeconds(fadingTime);
        SceneManager.LoadScene(indexOfSceneToLoad);
        yield return null;
    }

    IEnumerable DisactiveBlackscreenAfterTime()
    {
        yield return new WaitForSeconds(fadingTime);
        mainObjectToTurnOn.SetActive(false);
        yield return null;
    }

}