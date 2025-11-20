using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.Mathematics;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private GameObject Projectile, FirePoint;
    [SerializeField] float maxHealth = 100f;

    public HealthBar healthBar;
    float currentHealth;
    int currentScene;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeet;


    private bool isAlive = true; 
    private bool Attack;
    public bool facingRight;
    bool hasStarted = false;
    private bool jumping;
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }



    // Update is called once per frame
    void Update()
    {
        
        if (!isAlive) { return; }
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to +1
        Run(controlThrow);
        FlipSprite(controlThrow);
        Jump();
        jumpAnimation();
        Fire();
        FallDie();

        ResetValues();


    }
    private void Run(float controlThrow)
    {
        switch (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            case true:
                myRigidBody.velocity = new Vector2(controlThrow * 0.5f, myRigidBody.velocity.y);
                break;
            case false:
                Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
                myRigidBody.velocity = playerVelocity;
                break;
        }



        bool playerHasMovementSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasMovementSpeed);


    }
    

    private void Jump()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {

            return;
        }
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            hasStarted = true;
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocity;
            
        }

    }

    private void jumpAnimation()
    {
        bool playerIsJumping = !myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        
        myAnimator.SetBool("Jumping", playerIsJumping);
    }

  

    private void FlipSprite(float controlThrow)
    {
        if (controlThrow > 0 && !facingRight || controlThrow < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector2 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }


    }

    public void Fire()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            Attack = true;
            FireAnimation();


        }

    }

    void FireAnimation()
    {
        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        else
        {
            myAnimator.SetTrigger("Attacking");

        }

    }

    public void Firing()
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject) Instantiate(Projectile, FirePoint.transform.position, transform.rotation);
            tmp.GetComponent<Projectile>().Initialize(Vector2.right);
        }
        if (!facingRight)
        {
            GameObject tmp = (GameObject) Instantiate(Projectile, FirePoint.transform.position, transform.rotation);
            tmp.GetComponent<Projectile>().Initialize(Vector2.left);
        }
    }
    public void DealDamage(float damage)
    {
        myAnimator.SetTrigger("Damage");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            
            Die();
        }
    }

    private void FallDie()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Spikes")))
        {
            Die();
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x , myRigidBody.velocity.y);
        }
    }
     
    private void Die()
    {
        isAlive = false;

        myAnimator.SetTrigger("Dying");
        
    }
    void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void jumpSound()
    {
        if (hasStarted)
        {
            GetComponent<AudioSource>().Play();

        }
    } 
    void ResetValues()
    {
        Attack = false;
    }

}
