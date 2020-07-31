using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BulletBehaviour : Damage, IElementalShootable
{
    [SerializeField] protected float speed = 15;
    [SerializeField] protected float lifeTime = 3;
    [SerializeField] protected float penetration = 5;
    [SerializeField] protected float shotImpact = 2;

    protected Rigidbody2D rigid;
    protected SpriteRenderer model;
    protected TrailRenderer tail;
    protected bool isAlive = true;

    protected void Awake()
    {
        TryGetComponent(out rigid);
        TryGetComponent(out model);
        TryGetComponent(out tail);
    }


    public virtual void Shoot()
    {
        rigid.velocity = transform.right * speed;
        Invoke("Disable", lifeTime);
    }

    protected virtual void Disable()
    {
        //GetComponent<Collider2D>().enabled = true;
        isAlive = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
            return;
        if (penetration > 0)
        {
            ITakeDamage target;
            collision.TryGetComponent(out target);
            if (target != null && target.IsAlive)
            {
                DoDamage(target);
                penetration--;
            }
            Rigidbody2D rigid;
            if(collision.TryGetComponent(out rigid))
                ApplyForce(rigid);
        }
        else
            Disable();
    }

    protected void ApplyForce(Rigidbody2D target)
    {
        if (target == null)
            return;
        target.velocity += rigid.velocity.normalized * shotImpact;
    }

    protected virtual void ChangeElement(Elements value)
    {
        if (tail != null)
        {
            model.color = ElementalUtility.GetColor(value);
            Gradient colorGradient = new Gradient();
            colorGradient.colorKeys = new GradientColorKey[] { new GradientColorKey(ElementalUtility.GetColor(value), 0), new GradientColorKey(Color.white, 1) };
            tail.colorGradient = colorGradient;
        }
    }

    public override void SwitchElement(Elements element)
    {
        base.SwitchElement(element);
        ChangeElement(element);
    }

}

