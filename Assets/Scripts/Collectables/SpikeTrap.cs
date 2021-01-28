using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : ACollectable
{
    public Single Damage;
    protected override void OnCollisionWithThisObject()
    {
        player.GetComponent<Player>().OnHit(Damage);
        Destroy(this.gameObject);
    }
}
