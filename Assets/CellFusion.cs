using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFusion : MonoBehaviour
{
    [SerializeField] EnemyDeathEvent enemyDeath;
    [SerializeField] public int id;
    [SerializeField] public float delay;
    [SerializeField] GameObject healthBar;
    bool close=false, canFuse=true;
    CellFusion victim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        victim = collision.GetComponent<CellFusion>();
        if (victim != null && victim.id==id)
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
            Debug.Log("collision!");
        if(victim!=null)
        {
            transform.position = (transform.position + victim.transform.position) * 0.5f;
            TweenScale scale = GetComponent<TweenScale>();
            scale.xPercentage = 1.5f;
            scale.yPercentage = 1.5f;
            GetComponent<ElementalHealthBehaviour>().Heal(1);
            GetComponent<CharacterMovement>().AddSpeed(1);
            healthBar.SetActive(true);
            enemyDeath.OnDeath();
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
