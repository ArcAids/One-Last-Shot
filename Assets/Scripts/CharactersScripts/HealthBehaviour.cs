using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour, ITakeDamage
{
    float health;
    [SerializeField]
    protected float maxHealth;
    [Space]
    [SerializeField] Image playerHealth;

    public float Health { get => health; protected set { health = value; if(playerHealth!=null) playerHealth.fillAmount = (value / MaxHealth); } }

    public float MaxHealth => maxHealth;

    private void Awake()
    {
        Health = MaxHealth;
    }

    private void Start()
    {
        
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
