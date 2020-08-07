using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour , ITakeDamage
{
    [SerializeField] bool isInvincible = false;
    [SerializeField] float health;
    [SerializeField] protected float maxHealth;

    [Space]
    //[SerializeField] Image playerHealth;
    [SerializeField] HealthHUD playerHealthUI;
    [SerializeField] UnityEvent onDeathEvent;
    [SerializeField] UnityEvent onDamageTaken;
    public float Health { get => health; protected set { health = Mathf.Clamp(value, 0,MaxHealth); if (playerHealthUI != null)
                //playerHealth.fillAmount = (value / MaxHealth);
                playerHealthUI.UpdateHealth(health);
                } }

    public float MaxHealth => maxHealth;
    public bool IsAlive { get; set; }
    public bool IsInvincible { get => isInvincible; set => isInvincible = value; }

    private void Awake()
    {
        Init();   
    }
    protected virtual void Init()
    {
        playerHealthUI?.SetUpChunks(MaxHealth);//,Mathf.CeilToInt(MaxHealth));
        Health = health;
        //Health = MaxHealth;
        //Debug.Log(name);
        IsAlive = true;
    }
    public void DisableInASecond()
    {
        Invoke("Disable", 3);

    }

    public void Heal(float value)
    {
        Health = Mathf.Min(MaxHealth, value + Health);
    }

    void Disable()
    {
        GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    [ContextMenu("Kill")]
    public virtual void OnDeath()
    {
        IsAlive = false;
        onDeathEvent.Invoke();
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public virtual bool TakeDamage(float damage)
    {
        if (IsInvincible || !IsAlive)
            return false;

        Health -= damage;

        if (Health <= 0)
        {
            OnDeath();
        }else
            onDamageTaken.Invoke();
        return true;
    }

}
