using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;


    public GameObject Target { get; set; }

    private Rigidbody2D myRidigbody;
   




    void Start()
    {

        myRidigbody = GetComponent<Rigidbody2D>();
        

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
   


}
