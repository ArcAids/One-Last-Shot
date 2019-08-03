using UnityEngine;

public class ElementalHealthBehaviour : HealthBehaviour, ITakeElementalDamage
{
    float health;

    [SerializeField]
    Elements notImmuneTo;
    public Elements DamageType { get => notImmuneTo; set { notImmuneTo = value; } }

    private void Awake()
    {
        DamageType = notImmuneTo;
        Health = MaxHealth;
    }

    public void TakeDamage(float damage, Elements element = Elements.Slash)
    {
        if (element == DamageType)
        {
            TakeDamage(damage);
        }
    }

}
