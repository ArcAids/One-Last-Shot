using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Sharpnel : MonoBehaviour, IElementalShootable
{
    [SerializeField] float speed = 15;
    [SerializeField] float lifeTime = 0.3f;
    [SerializeField] float damage = 1;
    Elements element;
    Rigidbody2D rigid;
    SpriteRenderer model;

    public Elements Element
    {
        get => element; private set
        {
            element = value;
            model.color = ElementalUtility.GetColor(value);
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        model = GetComponent<SpriteRenderer>();
    }



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
        if (target != null)
        {
            target.TakeDamage(damage, Element);
        }
    }

    public void SwitchElement(Elements element)
    {
        Element = element;
    }
}
