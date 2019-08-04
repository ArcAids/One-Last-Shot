using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack : MonoBehaviour
{
    IWeaponInput input;
    [SerializeField] Transform target;
    [SerializeField] float damage;
    [SerializeField] float delay;

    ITakeDamage victim;
    bool close;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        victim = collision.GetComponent<ITakeDamage>();
        if(victim!=null)
        {
            close = true;
            StartCoroutine(AttemptHit(victim));
        }
    }

    IEnumerator AttemptHit(ITakeDamage victim)
    {
        yield return new WaitForSeconds(delay);
        if(close)
            victim.TakeDamage(damage);
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
