using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosive : Damage
{
    [SerializeField] float blastRadius;
    [SerializeField] float blastImpact;
    float blastRadiusSqr;
    RaycastHit2D[] targets;
    [SerializeField] UnityEvent onExplosion;
    bool isAlive = true;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isAlive)
            return;

        Explode();
    }

    void Explode()
    {
        Debug.DrawLine(transform.position + (Vector3.down * blastRadius), transform.position + (Vector3.up * blastRadius), Color.red, 5);
        Debug.DrawLine(transform.position + (Vector3.left * blastRadius), transform.position + (Vector3.right * blastRadius), Color.red, 5);
        // Debug.DrawLine(transform.position - (new Vector3(blastRadius,-blastRadius)), transform.position + (new Vector3(blastRadius, -blastRadius)), Color.red, 5);

        //todo asdas 

        Transform particleSystem = GetComponentInChildren<ParticleSystem>().transform;
        particleSystem.parent = null;
        particleSystem.localScale = Vector3.one;

        onExplosion.Invoke();

        targets = Physics2D.CircleCastAll(transform.position, blastRadius, Vector2.down);

        if (targets != null && targets.Length > 0)
        {
            foreach (var target in targets)
            {
                Debug.Log(target.collider.name, target.collider.gameObject);
                if (!target.collider.CompareTag("Player") && target.collider.isTrigger)
                    DoDamage(target.transform.GetComponent<ITakeDamage>());
                ApplyForce(target.transform.GetComponent<Rigidbody2D>());
            }
        }
        Disable();
    }

    protected void Disable()
    {
        //GetComponent<Collider2D>().enabled = true;
        isAlive = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    } 

    void ApplyForce(Rigidbody2D target)
    {
        if (target == null)
            return;
        blastRadiusSqr = Mathf.Pow(blastRadius, 2);
        Vector2 forceDirection = (target.position - new Vector2(transform.position.x, transform.position.y));
        float distanceSqr = forceDirection.sqrMagnitude;
        forceDirection = forceDirection.normalized;
        distanceSqr = distanceSqr / blastRadiusSqr;
        distanceSqr = Mathf.Max(0, 1 - Mathf.Min(1, distanceSqr));
        distanceSqr = distanceSqr * distanceSqr;
        target.velocity += (forceDirection * (1 + blastImpact * distanceSqr));
    }
}
