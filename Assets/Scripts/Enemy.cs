using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    

    public GameObject Target { get; set; }

    private Rigidbody2D myRidigbody;
    Animator myAnimator;

    



    void Start()
    {
        
        myRidigbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            myRidigbody.velocity = new Vector2(-moveSpeed, 0f);

        }
        else
        {
            myRidigbody.velocity = new Vector2(moveSpeed, 0f);
        }

        Attack();
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Vector2 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
        
    }
    void Attack()
    {
        if(Target != null)
        {
            myAnimator.SetTrigger("Attacking");
            myRidigbody.velocity = new Vector2(0f, 0f);

        }

    }
    

}
