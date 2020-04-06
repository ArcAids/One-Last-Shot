﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour , ITakeDamage
{
    [SerializeField] bool isInvincible = false;
    [SerializeField] float health;
    [SerializeField] protected float maxHealth;

    [Space]
    [SerializeField] Image playerHealth;
    [SerializeField] HealthHUD playerHealthUI;
    [SerializeField] UnityEvent onDeathEvent;
    public float Health { get => health; protected set { health = value < 0 ? 0 : value; if (playerHealthUI != null)
                //playerHealth.fillAmount = (value / MaxHealth);
                playerHealthUI.UpdateHealth(health);
                } }

    public float MaxHealth => maxHealth;

    protected bool isAlive = true;
    public bool IsAlive { get => isAlive; set { isAlive = value; } }

    private void Awake()
    {
        Init();   
    }
    protected virtual void Init()
    {
        playerHealthUI?.SetMaxHealth(MaxHealth);
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

    public virtual void OnDeath()
    {
        IsAlive = false;
        onDeathEvent.Invoke();
        //gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        if (isInvincible)
            return;
        if (!IsAlive)
            return;
        Health -= damage;
        if (Health <= 0)
        {

            OnDeath();
        }
    }

}
