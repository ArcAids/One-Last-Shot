using UnityEngine;

public class ElementalHealthBehaviour : HealthBehaviour, ITakeElementalDamage
{
    SpriteRenderer body;
    [SerializeField] Elements notImmuneTo;
    public Elements DamageType { get => notImmuneTo; private set { notImmuneTo = value;
            //body.color=ElementalUtility.GetColor(value);
        } }

    private void Awake()
    {
        body = GetComponentInChildren<SpriteRenderer>();
        DamageType = notImmuneTo;
        Health = MaxHealth;
    }

    public void TakeDamage(float damage, Elements element)
    {
        if (element == DamageType)
        {
            TakeDamage(damage);
        }
    }

    public void SwitchElement(Elements element)
    {
        DamageType = element;
    }
}
