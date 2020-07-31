 using UnityEngine;

public class ElementalHealthBehaviour : HealthBehaviour, ITakeElementalDamage
{
   // SpriteRenderer body;
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
        //body = GetComponentInChildren<SpriteRenderer>();
        Element = notImmuneTo;
    }

    public bool TakeDamage(float damage, Elements element)
    {
        if (!IsAlive)
            return false;
        if (element == Element)
        {
            return TakeDamage(damage); 
        }
        return false;
    }

    public void SwitchElement(Elements element)
    {
        Element = element;
    }
}
