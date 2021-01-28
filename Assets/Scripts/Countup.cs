using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countup : MonoBehaviour
{
    public Text textBox;
    private Single timePassed = 0.01f;
    void Start()
    {
        textBox.text = timePassed.ToString("F2");
    }

    void FixedUpdate()
    {
        timePassed += Time.deltaTime;
        textBox.text = timePassed.ToString("F2");
    }
}
