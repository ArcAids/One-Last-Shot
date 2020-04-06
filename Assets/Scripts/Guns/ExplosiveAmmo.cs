using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplosiveAmmo : BulletBehaviour
{
    [SerializeField] float blastRadius;
    float blastRadiusSqr;
    RaycastHit2D[] targets;
    [SerializeField] UnityEvent onExplosion;
    [SerializeField] ParticleSystem fireBlast;
    [SerializeField] ParticleSystem freezeBlast;
    [SerializeField] ParticleSystem magicBlast;
    ParticleSystem selected;
    protected override void OnTriggerEnter2D(Collider2D collision)
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

        if (selected == null)
            SwitchBlastVFX(element);
        
        selected.transform.parent = null;
        selected.transform.localScale = Vector3.one;
        selected.Play();
        fireBlast.GetComponent<AudioSource>()?.Play(); 
        onExplosion.Invoke();

        targets = Physics2D.CircleCastAll(transform.position, blastRadius, Vector2.down);
        
        if (targets != null && targets.Length > 0)
        {
            foreach (var target in targets)
            {
                //Debug.Log(target.collider.name, target.collider.gameObject);
                if (!target.collider.CompareTag("Player") && target.collider.isTrigger)
                    DoDamage(target.transform.GetComponent<ITakeDamage>());
                ApplyForce(target.transform.GetComponent<Rigidbody2D>());
            }
        }
        base.Disable();
    }

    void ApplyForce(Rigidbody2D target)
    {
        if (target == null)
            return;
        blastRadiusSqr = Mathf.Pow(blastRadius,2);
        Vector2 forceDirection = (target.position - new Vector2(transform.position.x, transform.position.y));
        float distanceSqr = forceDirection.sqrMagnitude;
        forceDirection = forceDirection.normalized;
        distanceSqr = distanceSqr/blastRadiusSqr;
        distanceSqr = Mathf.Max(0,1-Mathf.Min(1,distanceSqr));
        distanceSqr = distanceSqr * distanceSqr;
        target.velocity += (forceDirection*(1+ shotImpact*distanceSqr));
    }

    public override void SwitchElement(Elements element)
    {
        base.SwitchElement(element);
        SwitchBlastVFX(element);
    }

    protected override void Disable()
    {
        Explode();
    }

    void SwitchBlastVFX(Elements element)
    {
        switch (element)
        {
            case Elements.Fire:
                selected = fireBlast;
                break;
            case Elements.Ice:
                selected = freezeBlast;
                break;
            case Elements.Slash:
                selected = magicBlast;
                break;
            default:
                break;
        }
    }
}
