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
        Init();
    }

    protected override void Init()
    {
        base.Init();
        body = GetComponentInChildren<SpriteRenderer>();
        Element = notImmuneTo;
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
