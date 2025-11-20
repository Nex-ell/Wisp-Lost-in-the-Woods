using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] int damage = 0;
    [SerializeField] float Dps = 1f;
    Player playerHealth;
    bool HitBox;
   


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if (other.tag == "Player")
        {
            enemy.Target = enemy.gameObject;
            PlaySound();
        }
        HitBox = true;
        playerHealth = other.GetComponent<Player>();
        
        StartCoroutine(DamageContinously());
        

    }


    IEnumerator DamageContinously()
    {
        while (HitBox)
        {
            playerHealth.DealDamage(damage);
            yield return new WaitForSeconds(Dps);

        }
        
        
    }
        
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = null;

        }
        HitBox = false;
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }

}
