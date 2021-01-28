using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierControl : MonoBehaviour
{
    public Int32 AmountOfSpikesToDeflect;

    private GameObject Player;
    private Int32 _deflectedSpikes = 0;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("spike"))
        {
            if (_deflectedSpikes < AmountOfSpikesToDeflect)
            {
                AudioManager.Instance.Play("shieldHit");
                _deflectedSpikes++;
                Destroy(collision.gameObject);
            }
            else
            {
                AudioManager.Instance.Play("shieldBreak");
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
        else
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void Start()
    {
        Player = GameObject.Find("objPlayer");
    }

    void Update()
    {
        Vector3 playerPosition = Player.transform.position;
        this.gameObject.transform.position = new Vector3(playerPosition.x, playerPosition.y-0.2f, playerPosition.z);
    }
}
