using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float delay;

    bool canAttack=false;

    ITakeDamage victim;
    bool close;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || !canAttack)
            return;
        victim = collision.GetComponent<ITakeDamage>();
        if(victim!=null)
        {
            close = true;
            StopAllCoroutines();
            StartCoroutine(AttemptHit(victim));
        }
    }

    public void DisableAttack()
    {
        canAttack = false;
    }
     public void EnableAttack()
    {
        canAttack = true;
    }

    IEnumerator AttemptHit(ITakeDamage victim)
    {
        while(close)
        {
            yield return new WaitForSeconds(delay);
            if(close && canAttack)
                victim.TakeDamage(damage);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ITakeDamage victim = collision.GetComponent<ITakeDamage>();
        if (victim == this.victim)
        {
            close = false;
        }
    }
}
