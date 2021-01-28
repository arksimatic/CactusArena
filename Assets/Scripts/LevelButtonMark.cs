using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonMark : MonoBehaviour
{
    public Text levelNumber;
    public GameObject completion;

    void Start()
    {
        int? isCompleted = PlayerPrefs.GetInt("level" + levelNumber.text);
        if (isCompleted!=null && isCompleted == 1)
            completion.SetActive(true);
        else
            completion.SetActive(false);

    }

    public void NotifyAboutReset()
    {
        completion.SetActive(false);
    }

}
