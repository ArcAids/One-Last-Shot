public interface ITakeDamage
{

    float MaxHealth { get; }

    void TakeDamage(float damage);

    void OnDeath();

}

public interface ITakeElementalDamage
{
    Elements DamageType { get; }
    void TakeDamage(float damage, Elements element=Elements.Slash);
}


public enum Elements
{
    Fire, Ice, Slash
}