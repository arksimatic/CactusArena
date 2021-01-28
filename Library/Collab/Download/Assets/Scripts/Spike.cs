using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private GameObject player;
    public Color Color;
    public Single damage;

    //this.GetComponent<Rigidbody2D>()
    Single _spawnTime;
    Single? _explodeAfter = null;
    Boolean _exploded = false;

    void Awake()
    {
        _spawnTime = Time.realtimeSinceStartup;
    }
    void Start() 
    {
        player = GameObject.Find("objPlayer");
    }

    public void SetGraphicalAngleAndFlyInAngle(Single angle)
    {
        transform.eulerAngles = new Vector3(0, 0, angle);
        this.GetComponent<Rigidbody2D>().AddForce(transform.right * 250);
        transform.eulerAngles = new Vector3(0, 0, angle - 90);
    }

    public void SetGraphicalAngleAndFlyInAngleWithExplosion(Single angle, Single explosionAfterSeconds)
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        _explodeAfter = explosionAfterSeconds;

        transform.eulerAngles = new Vector3(0, 0, angle);
        this.GetComponent<Rigidbody2D>().AddForce(transform.right * 100);
        transform.eulerAngles = new Vector3(0, 0, angle - 90);
        StartCoroutine(ExplosionWaiter());
    }

    public void SetGraphicalAngleAndFlyInAngleWithAutoaim(Single angle)
    {
        this.GetComponent<SpriteRenderer>().color = Color.blue;
        transform.eulerAngles = new Vector3(0, 0, angle);
        this.GetComponent<Rigidbody2D>().AddForce(transform.right * 200);
        transform.eulerAngles = new Vector3(0, 0, angle - 90);
        StartCoroutine(Rotate());
    }

    private IEnumerator ExplosionWaiter()
    {
        if (!_exploded && Time.realtimeSinceStartup - _spawnTime >= _explodeAfter)
        {
            GameObject boi = new GameObject("boi");
            boi.transform.position = this.gameObject.transform.position;
            boi.transform.localScale = this.gameObject.transform.localScale;

            boi.transform.SetParent(GameObject.FindGameObjectWithTag("Spikes").transform);
            List<(GameObject spike, Int32 angle)> spikes = new List<(GameObject spike, Int32 angle)>();
            for (Int32 i = 0; i < 360; i += 15)
            {
                GameObject bullet = Instantiate(Resources.Load<GameObject>("Prefabs/spike"), Vector3.zero, Quaternion.identity);
                bullet.transform.position = boi.transform.position;
                bullet.transform.SetParent(boi.transform);
                bullet.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                spikes.Add((bullet, i));
            }

            foreach (var b in spikes)
            {
                b.spike.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                b.spike.GetComponent<Spike>().SetGraphicalAngleAndFlyInAngle(b.angle);
            }

            Destroy(boi.gameObject, 4f);
            _exploded = true;
        }

        yield return new WaitForSeconds(0.1f);
        if (!_exploded)
            StartCoroutine(ExplosionWaiter());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("spike"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true);
        }
        else if (collision.gameObject.name.Contains("Brick"))
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.name.Contains("objPlayer"))
        {
            player.GetComponent<Player>().OnHit(this.damage);
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < 0.9
            && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.9
            && Time.realtimeSinceStartup - _spawnTime >= 2)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Rotate()
    {
        Quaternion targetRotation = Quaternion.identity;
        do
        {
            GameObject p = GameObject.Find("objPlayer");
            Debug.Log(p.transform.position);
            Vector3 targetDirection = (p.transform.position - transform.position).normalized;
            targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 100);

            yield return null;

        } while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f);
    }
}
