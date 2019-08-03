using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour, IElementalShootable 
{
    [SerializeField] float speed=15;
    [SerializeField] float damage=1;
    [SerializeField] Elements element;
    Rigidbody2D rigid;

    public Elements Element => element;

    private void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
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
}
