using UnityEngine;

public class ElementalHealthBehaviour : HealthBehaviour, ITakeElementalDamage
{
    SpriteRenderer body;
    [SerializeField] Elements notImmuneTo;
    public Elements Element { get => notImmuneTo; private set { notImmuneTo = value;
            //body.color=ElementalUtility.GetColor(value);
        } }

    private void Awake()
    {
        body = GetComponentInChildren<SpriteRenderer>();
        Element = notImmuneTo;
        Health = MaxHealth;
    }

    public void TakeDamage(float damage, Elements element)
    {
        if (element == Element)
        {
            TakeDamage(damage);
        }
    }

    public void SwitchElement(Elements element)
    {
        Element = element;
    }
}
