public interface ITakeDamage
{
    bool IsAlive { get; }
    float MaxHealth { get; }

    void TakeDamage(float damage);

    void OnDeath();

}

public interface ITakeElementalDamage : ITakeDamage ,IElemental
{
    void TakeDamage(float damage, Elements element);
}


