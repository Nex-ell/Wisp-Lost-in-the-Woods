using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    Rigidbody2D rb;
    
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
             
                
     }
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(1f);
        rb.isKinematic = false;
    }



}

