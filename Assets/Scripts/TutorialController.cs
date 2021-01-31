using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public List<GameObject> Pages;
    public GameObject PageNumber;
    public GameObject NextButton;
    public GameObject PreviousButton;
    private Int32 currentPage = 0;

    public void Start()
    {
        foreach (GameObject page in Pages)
        {
            page.SetActive(false);
        }
        Pages[currentPage].SetActive(true);

        UpdateButtonsVisibility();
        UpdateCurrentPageDisplay();
    }
     
    void UpdateButtonsVisibility()
    {
        if (currentPage == 0)
            PreviousButton.SetActive(false);
        else
            PreviousButton.SetActive(true);

        if (currentPage == Pages.Count-1)
            NextButton.SetActive(false);
        else
            NextButton.SetActive(true);
    }

    void UpdateCurrentPageDisplay()
    {
        PageNumber.GetComponent<Text>().text = $"{currentPage+1}/{Pages.Count}";
    }

    public void Next()
    {
        Pages[currentPage++].SetActive(false);
        Pages[currentPage].SetActive(true);
        UpdateButtonsVisibility();
        UpdateCurrentPageDisplay();
    }

    public void Previous()
    {
        Debug.Log("fdsjoi");
        Pages[currentPage--].SetActive(false);
        Pages[currentPage].SetActive(true);
        UpdateButtonsVisibility();
        UpdateCurrentPageDisplay();
    }
}
