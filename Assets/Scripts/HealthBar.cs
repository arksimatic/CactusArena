using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class HealthBar : MonoBehaviour
{
    private Single max;
    private Single destinated;
    public Image mask;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("objPlayer");
        max = player.GetComponent<Player>().MaxHealth;
        mask.fillAmount = player.GetComponent<Player>().Health;
    }
    void Update()
    {
        destinated = player.GetComponent<Player>().Health;
        Single difference = destinated - (mask.fillAmount * max);

        if (mask.fillAmount * max != destinated)
            mask.fillAmount += difference / 20000;
    }
}
