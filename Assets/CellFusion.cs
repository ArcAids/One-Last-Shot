using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFusion : MonoBehaviour
{
    [SerializeField] public int id;
    [SerializeField] public int priority;
    [SerializeField] public float delay;
    [SerializeField] public float heal=1;
    [SerializeField] public float speedUp=1;
    [SerializeField] GameObject healthBar;
    bool close=false, canFuse=true;
    CellFusion victim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        victim = collision.GetComponent<CellFusion>();
        if (victim != null && victim.id==id && priority<=victim.priority && priority>0)
        {
            Debug.Log("collision!");
            close = true;
            StopAllCoroutines();
            StartCoroutine(AttemptHit());
        }
    }

    public void DisableAttack()
    {
        canFuse = false;
    }

    IEnumerator AttemptHit()
    {
        while (close)
        {
            yield return new WaitForSeconds(delay);
            if (close && canFuse)
                Fuse();
        }

    }

    void Fuse()
    {
            //Debug.Log("collision!");
        if(victim!=null)
        {
            transform.position = (transform.position + victim.transform.position) * 0.5f;
            TweenScale scale = GetComponent<TweenScale>();
            scale.xPercentage = 1.3f;
            scale.yPercentage = 1.3f;
            priority--;
            GetComponent<ElementalHealthBehaviour>().Heal(heal);
            GetComponent<CharacterMovement>().AddSpeed(speedUp);
            healthBar.SetActive(true);
            victim.GetComponent<ElementalHealthBehaviour>().OnDeath();
            Destroy(victim.gameObject);
            scale.StartTween();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CellFusion victim = collision.GetComponent<CellFusion>();
        if (victim == this.victim)
        {
            close = false;
        }
    }
}
