using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class BulletBehaviour : MonoBehaviour, IElementalShootable 
{
    [SerializeField] protected float speed=15;
    [SerializeField] protected float damage =1;
    [SerializeField] protected float lifeTime =3;
    protected Elements element;
    protected Rigidbody2D rigid;
    protected SpriteRenderer model;

    public Elements Element { get => element; private set { element = value;
            model.color = ElementalUtility.GetColor(value);
        } }

    protected void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
        model=GetComponent<SpriteRenderer>();
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
        GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.GetComponent<ITakeElementalDamage>();
        if(target!=null)
        {
            target.TakeDamage(damage, Element);
        }
    }

    public void SwitchElement(Elements element)
    {
        Element = element;
    }
}
