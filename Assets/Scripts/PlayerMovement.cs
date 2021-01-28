using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private float speed;
    private float slowStrength;
    private Rigidbody2D rb2d;

    private bool isSlowed = false;

    void Start()
    {
        animator = GetComponent<Animator>();


        //animator was magically detaching sometimes so this is legacy code but may be helpful if anything goes wrong in the future
        //gameObject.AddComponent<Animator>();
        //animator.runtimeAnimatorController = Resources.Load("objPlayer") as RuntimeAnimatorController;


        rb2d = GetComponent<Rigidbody2D>();
        rb2d.drag = 100000;
        rb2d.mass = 1;
        //speed = 100000;  //why would you like to hardcode speed?
    }

    void Update()
    {
        //Michal's code
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //rb2d.AddForce(movement * speed);

        var changeX = Input.GetAxisRaw("Horizontal");
        var changeY = Input.GetAxisRaw("Vertical");
        var actualMovementX =  changeX * speed * Time.deltaTime;
        var actualMovementY =  changeY * speed * Time.deltaTime;
        
        if(changeX!=0 || changeY!=0)
        {
            animator.SetFloat("moveX", changeX);
            animator.SetFloat("moveY", changeY);
            transform.position = new Vector2(transform.position.x + actualMovementX, transform.position.y + actualMovementY);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }    
    }

    public void SlowPlayer(Single slowDuration, Single slowStrength)
    {
        this.slowStrength = slowStrength;
        if(!isSlowed)
            StartCoroutine("Slow", slowDuration);
    }

    private IEnumerator Slow(Single slowDuration)
    {
        speed /= slowStrength;
        yield return new WaitForSeconds(slowDuration);
        speed *= slowStrength;
    }
}
