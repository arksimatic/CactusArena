using System;
using System.Collections;
using UnityEngine;

public abstract class Cactus : MonoBehaviour
{
    public GameObject objBullet;
    protected GameObject objSpikesFolder;
    public Animator animator;

    public Single MaxHealth = 100;
    private Single health;
    private Boolean isStasis = false;

    protected Boolean _isBursting = false;

    private void ResetHealth()
    {
        Debug.Log("hp reset");
        health = MaxHealth;
    }

    private void Update()
    {
        Debug.Log(health);
    }

    public void GetDamage(Single damage)
    {
        health -= damage;
        Debug.Log($"health = {health}");
        if (health <= 0 && !isStasis)
            StartCoroutine(Stasis());
    }

    private IEnumerator Stasis()
    {
        isStasis = true;

        yield return new WaitForEndOfFrame(); //i hope so

        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.3f);

        yield return new WaitForSeconds(10f);

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        isStasis = false;
        ResetHealth();
    }

    void Start()
    {
        ResetHealth();

        objSpikesFolder = GameObject.Find("Spikes");
        animator = GetComponent<Animator>();

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Spikes"), LayerMask.NameToLayer("Spikes"));
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        if (!_isBursting && !isStasis)
        {
            Int32 burstId = UnityEngine.Random.Range(0, 4);
            switch (burstId)
            {
                case 0:
                    {
                        animator.SetBool("isShooting", true);
                        StartCoroutine(RadialBurst()); break;
                    }
                case 1:
                    {
                        animator.SetBool("isShooting", true);
                        StartCoroutine(RandomBurst()); break;
                    }
                case 2:
                    {
                        animator.SetBool("isShooting", false);
                        animator.SetTrigger("oneShot");
                        StartCoroutine(ExplosionBurst());
                        yield return new WaitForSeconds(2f); break;
                    }
                case 3:
                    {
                        animator.SetBool("isShooting", false);
                        StartCoroutine(AutoaimBurst()); break;
                    }
            }
        }

        yield return new WaitForEndOfFrame();
        StartCoroutine(Run());
    }

    protected abstract GameObject SpawnNewBullet();
    private IEnumerator RandomBurst()
    {
        _isBursting = true;

        for (Int32 i = 0; i < 360; i += 6)
        {
            yield return new WaitForSeconds(0.04f);
            AudioManager.Instance.Play("shot");
            GameObject bullet = SpawnNewBullet();
            SetGraphicalAngleAndFlyInAngle(bullet, UnityEngine.Random.Range(0, 360));
        }
        _isBursting = false;

    }

    protected abstract void SetGraphicalAngleAndFlyInAngleWithAutoaim(GameObject bullet);
    private IEnumerator AutoaimBurst()
    {
        _isBursting = true;
        for (Int32 i = 0; i < 36; i += 6)
        {
            yield return new WaitForSeconds(0.4f);
            AudioManager.Instance.Play("shot");
            animator.SetTrigger("oneShot");
            GameObject bullet = SpawnNewBullet();

            SetGraphicalAngleAndFlyInAngleWithAutoaim(bullet); //implementation in specific cacti
            //bullet.GetComponent<NormalSpike>().SetGraphicalAngleAndFlyInAngleWithAutoaim(UnityEngine.Random.Range(0, 360));
        }
        _isBursting = false;
    }

    protected abstract void SetGraphicalAngleAndFlyInAngle(GameObject bullet, int angle);
    private IEnumerator RadialBurst()
    {
        _isBursting = true;
        for (Int32 i = 0; i < 360; i += 6)
        {
            yield return new WaitForSeconds(0.03f);
            AudioManager.Instance.Play("shot");
            GameObject bullet = SpawnNewBullet();
            SetGraphicalAngleAndFlyInAngle(bullet, i);
            //bullet.GetComponent<NormalSpike>().SetGraphicalAngleAndFlyInAngle(i);
        }
        _isBursting = false;

    }


    protected abstract void SetGraphicalAngleAndFlyInAngleWithExplosion(GameObject bullet, int time);
    private IEnumerator ExplosionBurst()
    {
        _isBursting = true;
        AudioManager.Instance.Play("shot");
        GameObject bullet = SpawnNewBullet();
        SetGraphicalAngleAndFlyInAngleWithExplosion(bullet, 1);
        _isBursting = false;

        yield return null;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Projectile"))
        {
            GetDamage(25f);
        }
    }
}
