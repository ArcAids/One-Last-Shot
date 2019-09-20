using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BulletBehaviour : MonoBehaviour, IElementalShootable 
{
    [SerializeField] protected float speed=15;
    [SerializeField] protected float damage =1;
    [SerializeField] protected float lifeTime =3;
    [SerializeField] protected float penetration=5;
    protected Elements element;
    protected Rigidbody2D rigid;
    protected SpriteRenderer model;
    protected TrailRenderer tail;

    public Elements Element { get => element; private set { element = value;
            model.color = ElementalUtility.GetColor(value);
            Gradient colorGradient = new Gradient();
            colorGradient.colorKeys= new GradientColorKey[] { new GradientColorKey(ElementalUtility.GetColor(value),0), new GradientColorKey(Color.white, 1) };
            tail.colorGradient = colorGradient;
        } }

    protected void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
        model=GetComponent<SpriteRenderer>();
        tail = GetComponent<TrailRenderer>();
    }



    //private void Start()
    //{
    //    Shoot();
    //}

    public virtual void Shoot()
    {
        rigid.velocity = transform.right * speed;
        Invoke("Disable", lifeTime);
    }

    void Disable()
    {
        //GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.GetComponent<ITakeElementalDamage>();
        if (target != null && target.IsAlive)
        {
            target.TakeDamage(damage, Element);
            penetration--;
            Debug.Log(penetration);
            if(penetration <=0)
                Disable();
        }
        else
        {
            var simpleTarget = collision.GetComponent<ITakeDamage>();
            if (simpleTarget != null && simpleTarget.IsAlive)
            {
                simpleTarget.TakeDamage(damage);
                penetration--;
                if (penetration<= 0)
                    Disable();
            }
        }
    }

    public void SwitchElement(Elements element)
    {
        Element = element;
    }
}
