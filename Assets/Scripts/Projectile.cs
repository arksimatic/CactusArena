using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Single livingTime = 10f;
    public Single Damage = 25;
    protected GameObject waterToIgnoreCollision;

    void Awake()
    {
        //credits for Gumichan01 for graphic of this projectile
    }

    protected void Start()
    {
        GameObject waterToIgnoreCollision = GameObject.Find("tlmWater");
        Physics2D.IgnoreCollision(waterToIgnoreCollision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Projectiles"), LayerMask.NameToLayer("Collectables"));

        GameObject collectableToIgnore = GameObject.Find("Collectable");
        if (collectableToIgnore != null)
            Physics2D.IgnoreCollision(collectableToIgnore.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    private void FixedUpdate()
    {
        livingTime -= Time.deltaTime;
        if (livingTime < 0)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("spike"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.name.Contains("Brick"))
        {
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.name.Contains("cactus"))
        {
            //collision.gameObject.GetComponent<NormalCactus>().GetDamage(Damage);
            Destroy(this.gameObject);
        }
    }

}
