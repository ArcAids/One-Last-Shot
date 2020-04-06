public interface ITakeDamage
{
    //void TakeDamage(DamageData damage);

    bool IsAlive { get; }
    float Health { get; }
    void TakeDamage(float damage);
    void OnDeath();
}

public interface ITakeElementalDamage : ITakeDamage ,IElemental
{
    void TakeDamage(float damage, Elements element);
}


