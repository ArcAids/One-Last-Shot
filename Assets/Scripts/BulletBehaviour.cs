using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour, IElementalShootable 
{
    [SerializeField] float speed=15;
    [SerializeField] float damage=1;
    [SerializeField] Elements element;
    Rigidbody2D rigid;
    SpriteRenderer model;

    public Elements Element { get => element; private set { element = value;
            model.color = ElementalUtility.GetColor(value);
        } }

    private void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
        model=GetComponent<SpriteRenderer>();
    }



    //private void Start()
    //{
    //    Shoot();
    //}

    public void Shoot()
    {
        rigid.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var target = collision.GetComponent<ITakeElementalDamage>();
        if(target!=null)
        {
            Debug.Log("Target:" + collision.name);
            target.TakeDamage(damage, Element);
        }
    }

    public void SwitchElement(Elements element)
    {
        Element = element;
    }
}
