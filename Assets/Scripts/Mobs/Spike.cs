using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Spike : MonoBehaviour
{
    protected GameObject player;
    public Color Color;
    public Single Damage;
    public GameObject spikeObj;
    protected GameObject waterToIgnoreCollision;

    protected Single _spawnTime;
    protected Single? _explodeAfter = null;
    protected Boolean _exploded = false;

    void Awake()
    {
        _spawnTime = Time.realtimeSinceStartup;
        player = GameObject.Find("objPlayer");
    }

    protected void Start()
    {
        GameObject waterToIgnoreCollision = GameObject.Find("tlmWater");
        Physics2D.IgnoreCollision(waterToIgnoreCollision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        IgnoreCactiCollisions();

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Spikes"), LayerMask.NameToLayer("Collectables"));
        //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Collectables"), LayerMask.NameToLayer("Spikes"));

        GameObject collectableToIgnore = GameObject.Find("Collectable");
        if(collectableToIgnore != null)
            Physics2D.IgnoreCollision(collectableToIgnore.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        //GameObject collectablesToIgnoreCollision = GameObject.Find("Collectables");
        //Physics2D.IgnoreCollision(collectablesToIgnoreCollision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        GetComponent<SpriteRenderer>().color = Color;
    }
    protected void IgnoreCactiCollisions()
    {
        GameObject cactiFolder = GameObject.Find("Cacti");

        List<GameObject> listOfCacti = new List<GameObject>();
        for (int i = 0; i < cactiFolder.transform.childCount; i++)
            listOfCacti.Add(cactiFolder.transform.GetChild(i).gameObject);

        foreach (GameObject cactus in listOfCacti)
            Physics2D.IgnoreCollision(cactus.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        //GameObject cactus = GameObject.Find("cactus");
        //GameObject freezingCactus = GameObject.Find("freezingCactus");
        //GameObject toxicCactus = GameObject.Find("toxicCactus");

        //Physics2D.IgnoreCollision(cactus.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(freezingCactus.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(toxicCactus.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void SetGraphicalAngleAndFlyInAngle(Single angle)
    {
        transform.eulerAngles = new Vector3(0, 0, angle);
        this.GetComponent<Rigidbody2D>().AddForce(transform.right * 250);
        transform.eulerAngles = new Vector3(0, 0, angle - 90);
    }

    public void SetGraphicalAngleAndFlyInAngleWithExplosion(Single angle, Single explosionAfterSeconds)
    {
        _explodeAfter = explosionAfterSeconds;

        transform.eulerAngles = new Vector3(0, 0, angle);
        this.GetComponent<Rigidbody2D>().AddForce(transform.right * 100);
        transform.eulerAngles = new Vector3(0, 0, angle - 90);
        StartCoroutine(ExplosionWaiter());
    }

    public void SetGraphicalAngleAndFlyInAngleWithAutoaim(Single angle)
    {
        transform.eulerAngles = new Vector3(0, 0, angle);
        this.GetComponent<Rigidbody2D>().AddForce(transform.right * 100);
        transform.eulerAngles = new Vector3(0, 0, angle - 90);

        Single a = SMath.ReturnAngleFromPositions(this.transform.position, player.transform.position);
        transform.eulerAngles = new Vector3(0, 0, a + 90);

        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        var angle = player.transform.position - this.transform.position;
        this.GetComponent<Rigidbody2D>().AddForce(angle * 100);

        yield return null;
    }

    protected void FixedUpdate()
    {
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < 0.9
            && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.9
            && Time.realtimeSinceStartup - _spawnTime >= 2)
        {
            Destroy(this.gameObject);
        }
    }

    protected IEnumerator ExplosionWaiter()
    {
        if (!_exploded && Time.realtimeSinceStartup - _spawnTime >= _explodeAfter)
        {
            GameObject boi = new GameObject("boi");
            boi.transform.position = this.gameObject.transform.position;
            boi.transform.localScale = this.gameObject.transform.localScale;

            boi.transform.SetParent(GameObject.FindGameObjectWithTag("Spikes").transform);
            //List<(GameObject spike, Int32 angle)> spikes = new List<(GameObject spike, Int32 angle)>();

            AudioManager.Instance.Play("explosion");
            List<GameObject> spikes = new List<GameObject>();
            List<int> angles = new List<int>();
            for (Int32 i = 0; i < 360; i += 15)
            {
                //finding resource via name was causing null exception so I changed it
                //GameObject bulletLoaded = Resources.Load<GameObject>("Prefabs/spike"); 
                GameObject bulletLoaded = spikeObj;
                if (bulletLoaded == null) Debug.Log("Spike is null");

                GameObject bullet = Instantiate(bulletLoaded, Vector3.zero, Quaternion.identity);

                bullet.transform.position = boi.transform.position;
                bullet.transform.SetParent(boi.transform);
                bullet.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                //spikes.Add((bullet, i));

                spikes.Add(bullet);
                angles.Add(i);
            }

            //foreach (var spike in spikes)
            //{
            //    spike.spike.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            //    spike.spike.GetComponent<FrozingSpike>().SetGraphicalAngleAndFlyInAngle(spike.angle);
            //}

            setSpikesAfterExplosion(spikes, angles);

            Destroy(gameObject);
            Destroy(boi.gameObject, 4f);
            _exploded = true;
        }

        yield return new WaitForSeconds(0.1f);
        if (!_exploded)
            StartCoroutine(ExplosionWaiter());
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("spike"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else if (collision.gameObject.name.Contains("Brick"))
        {
            Destroy(this.gameObject);
        }
    }

    protected abstract void setSpikesAfterExplosion(List<GameObject> spikes, List<int> angles);
}