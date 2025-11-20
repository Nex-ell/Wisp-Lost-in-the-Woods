using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] bool isBoss = false;
    Animator myAnimator;

    public HealthBar healthBar;
    float currentHealth;


    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        currentHealth = health;
        if (isBoss)
        {
            healthBar.SetMaxHealth(health);

        }

    }
    public void DealDamage(float damage)
    {
        myAnimator.SetTrigger("Enemydamage");
        health -= damage;
        if (isBoss)
        {
            currentHealth = health;
            healthBar.SetHealth(currentHealth);
        }
        if (health <= 0)
        {
          

            Destroy(gameObject);
            if (isBoss)
            {
                currentHealth = health;
                healthBar.SetHealth(currentHealth);
                Block block = GameObject.Find("Block").GetComponent<Block>();
                block.DestroyBlock();

            }

        }
    }

    
}
