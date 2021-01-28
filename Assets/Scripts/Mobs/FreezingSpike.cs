using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FreezingSpike : Spike
{
    public Single SlowDuration;
    public Single SlowStrength;
    void Start()
    {
        base.Start();
        Destroy(gameObject.GetComponent<NormalSpike>());
        Destroy(gameObject.GetComponent<ToxicSpike>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        
        if (collision.gameObject.name.Contains("objPlayer"))
        {
            player.GetComponent<Player>().OnHitWithSlow(Damage, SlowDuration, SlowStrength);
            Destroy(this.gameObject);
        }
    }

    protected override void setSpikesAfterExplosion(List<GameObject> spikes, List<int> angles)
    {
        for (int i = 0; i < spikes.Count; i++)
        {
            spikes[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            spikes[i].GetComponent<FreezingSpike>().SetGraphicalAngleAndFlyInAngle(angles[i]);
        }
    }
}
