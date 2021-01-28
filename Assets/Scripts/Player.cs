using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject damageTextPref;
    public Single MaxHealth;
    [HideInInspector]
    public Single Health;
    private bool isToxic = false;
    private bool gotToxicDamageAlready = false;
    private Single toxicDamage;

    public void Start()
    {
        Health = MaxHealth;
    }

    public void FixedUpdate()
    {
        CheckAndReactToToxicness();
        CheatHeal();
    }

    #region ToxicnessControl
    private void CheckAndReactToToxicness()
    {
        if (isToxic && !gotToxicDamageAlready)
        {
            Health -= toxicDamage;
            UpdateHealthStatus();

            ShowDamageText(toxicDamage, Color.green);
            StartCoroutine("MakePurpleOnHit");
            StartCoroutine("ResetToxicTimer");
            gotToxicDamageAlready = true;
        }
    }
    IEnumerator ResetToxicTimer()
    {
        yield return new WaitForSeconds(2f);
        gotToxicDamageAlready = false;
    }
    #endregion

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("cactus"))
            OnHit(0.5f);
    }

    #region OnHitControl

    public void BaseHit(Single damage)
    {
        Health -= damage;
        UpdateHealthStatus();
        AudioManager.Instance.Play("hit");
        ShowDamageText(damage, Color.red);
    }
    public void OnHit(Single damage)
    {
        BaseHit(damage);
        StartCoroutine("MakeRedOnHit");
    }

    public void OnHitWithSlow(Single damage, Single slowDuration, Single slowStrength)
    {
        BaseHit(damage);

        StartCoroutine("MakeBlueOnHit", slowDuration);
        GetComponent<PlayerMovement>().SlowPlayer(slowDuration, slowStrength);
    }

    public void OnHitWithToxin(Single damage, Single toxicDamage)
    {
        BaseHit(damage);

        this.toxicDamage = toxicDamage;

        //make green until untoxinated
        //StartCoroutine("MakePurpleOnHit");
        GetComponent<SpriteRenderer>().color = Color.green;
        isToxic = true;
    }
    #endregion

    #region ColorControl
    IEnumerator MakeRedOnHit()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.2f);
        ReturnToNormalColor();
    }

    IEnumerator MakeBlueOnHit(Single slowDuration)
    {
        GetComponent<SpriteRenderer>().color = Color.blue;

        yield return new WaitForSeconds(slowDuration);
        ReturnToNormalColor();
    }

    IEnumerator MakePurpleOnHit()
    {
        GetComponent<SpriteRenderer>().color = Color.green; //purple?
        yield return new WaitForSeconds(0.2f);
        ReturnToNormalColor();
    }
    #endregion

    #region CollectablesControl
    public void OnMilkCollected()
    {
        isToxic = false;
        ReturnToNormalColor();

        gotToxicDamageAlready = false;
    }

    public void OnHealthPotionCollected(Single healthReturn)
    {
        Health += healthReturn;
        if (Health > MaxHealth)
            Health = MaxHealth;
        UpdateHealthStatus();
        ShowDamageText(healthReturn, Color.cyan);
    }

    #endregion

    private void UpdateHealthStatus()
    {
        if (Health <= 0) GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void ShowDamageText(Single damage, Color color)
    {
        //logically this should be in the text's script itself but let's pretend that it's okay if I left it here

        GameObject damageText = Instantiate(damageTextPref, transform.position, Quaternion.identity, transform);
        TextMeshPro actualText = damageText.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        Single randomAngle = UnityEngine.Random.Range(-45f, 45f);

        actualText.text = damage.ToString();
        actualText.color = color;
        damageText.transform.RotateAround(damageText.transform.position, Vector3.forward, randomAngle);

        Destroy(damageText, 1f);
    }


    public void CheatHeal()
    {
        if(Input.GetKeyDown("space"))
        {
            Health = MaxHealth;
        }
    }

    public void ReturnToNormalColor()
    {
        if (isToxic)
            GetComponent<SpriteRenderer>().color = Color.green;
        else
            GetComponent<SpriteRenderer>().color = Color.white;
    }
}
