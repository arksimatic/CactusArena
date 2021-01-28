using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Single MaxHealth;
    [HideInInspector]
    public Single Health;

    public void Start()
    {
        Health = MaxHealth;
    }

    public void OnHit(Single damage)
    {
        Health -= damage;

        UpdateHealthStatus();
    }

    private void UpdateHealthStatus()
    {
        if(Health <= 0) GetComponent<SpriteRenderer>().color = Color.red;
    }
}
