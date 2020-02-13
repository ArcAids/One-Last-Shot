using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour, IElemental
{
    [SerializeField] protected float damage = 1;

    [SerializeField] protected Elements element;
    public Elements Element { get => element; set => element=value; }

    private void Awake()
    {
        Element = Element;
    }

    public virtual void SwitchElement(Elements element)
    {
        Element = element;
    }

    protected void DoDamage(ITakeDamage target)
    {
        if (target != null && target.IsAlive)
        {
            if (target is ITakeElementalDamage)
                (target as ITakeElementalDamage).TakeDamage(damage, Element);

            else if (target is ITakeDamage)
                target.TakeDamage(damage);


        }
    }
}
