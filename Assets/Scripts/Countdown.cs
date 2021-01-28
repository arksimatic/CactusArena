using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float timeStart; //time you want to pass
    public Text textBox;
    public GameObject ObjectWithWin;
    public bool isTicking = false;
    void Start()
    {
        textBox.text = timeStart.ToString("F2");
    }

    void FixedUpdate()
    {
        if (timeStart > 0)
        {
            timeStart -= Time.deltaTime;
            textBox.text = timeStart.ToString("F2");
            if(timeStart<5 && !isTicking)
            {
                StartCoroutine("lastSecondsTicking");
                isTicking = true;
            }
        }
        else
        {
            timeStart = 0;
            textBox.text = timeStart.ToString("F2");

            //Time.timeScale = 0; //this should be done inside WinHandler but just cant
            //ObjectWithWin.GetComponent<WinHandler>().WinPopupWindow();
        }
    }

    IEnumerator lastSecondsTicking()
    {
        AudioManager.Instance.Play("timer");
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.Play("timer");
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.Play("timer");
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.Play("timer");
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.Play("timer");
        yield return new WaitForSeconds(1f);
    }
}
