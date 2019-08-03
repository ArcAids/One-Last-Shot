using UnityEngine;

public class HealthBehaviour : MonoBehaviour, ITakeDamage
{
    float health;
    [SerializeField]
    protected float maxHealth;

    public float Health { get => health; set { health = value; } }

    public float MaxHealth => maxHealth;

    private void Awake()
    {
        Health = MaxHealth;
    }

    public void OnDeath()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            OnDeath();
        }
    }
}
