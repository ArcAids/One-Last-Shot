using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BulletBehaviour : Damage, IElementalShootable 
{
    [SerializeField] protected float speed=15;
    [SerializeField] protected float lifeTime =3;
    [SerializeField] protected float penetration=5;
    [SerializeField] protected float shotImpact=2;

    protected Rigidbody2D rigid;
    protected SpriteRenderer model;
    protected TrailRenderer tail;
    protected bool isAlive=true;

    protected void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
        model=GetComponent<SpriteRenderer>();
        tail = GetComponent<TrailRenderer>();
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
        ITakeDamage target = collision.GetComponent<ITakeDamage>();
        if (penetration > 0)
        {
            if (target != null && target.IsAlive)
            {
                DoDamage(target);
                penetration--;
            }

            ApplyForce(collision.GetComponent<Rigidbody2D>());
        }
        else
            Disable();
    }

    void ApplyForce(Rigidbody2D target)
    {
        if (target == null)
            return;
        target.velocity += rigid.velocity.normalized * shotImpact;
    }

    protected void ChangeElement(Elements value)
    {
        model.color = ElementalUtility.GetColor(value);
        Gradient colorGradient = new Gradient();
        colorGradient.colorKeys = new GradientColorKey[] { new GradientColorKey(ElementalUtility.GetColor(value), 0), new GradientColorKey(Color.white, 1) };
        tail.colorGradient = colorGradient;
    }

    public override void SwitchElement(Elements element)
    {
        base.SwitchElement(element);
        //ChangeElement(element);
    }

}

