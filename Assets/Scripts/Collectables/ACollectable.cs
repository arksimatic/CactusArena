using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACollectable : MonoBehaviour
{
    protected GameObject player;

    private void Start()
    {
        player = GameObject.Find("objPlayer");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("objPlayer"))
        {
            AudioManager.Instance.Play("itemPickup");
            OnCollisionWithThisObject();
        }
    }

    protected abstract void OnCollisionWithThisObject();
}
