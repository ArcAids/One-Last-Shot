public interface ITakeDamage
{
    //void TakeDamage(DamageData damage);

    bool IsAlive { get; }
    float Health { get; }
    bool TakeDamage(float damage);
    void OnDeath();
}

public interface ITakeElementalDamage : ITakeDamage ,IElemental
{
    bool TakeDamage(float damage, Elements element);
}


