using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour, ITakeDamage
{
    float health;
    [SerializeField] protected float maxHealth;
    [Space]
    [SerializeField] Image playerHealth;
    [SerializeField] UnityEvent onDeathEvent;
    public float Health { get => health; protected set { health = value; if(playerHealth!=null) playerHealth.fillAmount = (value / MaxHealth); } }

    public float MaxHealth => maxHealth;

    protected bool isAlive=true;

    private void Awake()
    {
        Health = MaxHealth;
        isAlive = true;
    }

    private void Start()
    {
        
    }

    public void DisableInASecond()
    {
        Invoke("Disable", 3);

    }

    void Disable()
    {
        GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public virtual void OnDeath()
    {
        isAlive = false;
        onDeathEvent.Invoke();
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        if (!isAlive)
            return;

        Health -= damage;
        if (Health <= 0)
        {

            OnDeath();
        }
    }
}
