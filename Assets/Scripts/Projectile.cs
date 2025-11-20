using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] float damage = 33f;
    [SerializeField] AudioClip FireSound;

    
    private Rigidbody2D myRigidbody2D;
    private Vector2 direction;
    

    void Start()
    {
        StartCoroutine(Destroy());
        myRigidbody2D = GetComponent<Rigidbody2D>();
       
    }

    void FixedUpdate()
    {
        Initialize(direction);
        myRigidbody2D.velocity = direction * speed;

    }

    public void Initialize(Vector2 direction)
    {
        
        this.direction = direction;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    { 
        
        var health = otherCollider.GetComponent<Health>();
        if(health != null)
        {
            health.DealDamage(damage);

        }
        Destroy(gameObject);

    }

    public void Awake()
    {
        AudioSource.PlayClipAtPoint(FireSound, Camera.main.transform.position);

    }


}


 
