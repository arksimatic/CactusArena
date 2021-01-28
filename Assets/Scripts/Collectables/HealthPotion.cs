using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : ACollectable
{
    public Single HealthReturn;
    protected override void OnCollisionWithThisObject()
    {
        player.GetComponent<Player>().OnHealthPotionCollected(HealthReturn);
        Destroy(this.gameObject);
    }
}
