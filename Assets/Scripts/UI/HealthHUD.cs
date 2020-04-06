using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHUD : MonoBehaviour
{
    [SerializeField] HealthChunk heart;
    [SerializeField] bool showEmptyHearts;
    [SerializeField] float updateSpeed;
    List<HealthChunk> hearts=new List<HealthChunk>();

    float currentHealth;
    float currentUIHealth;
    float maxHealth=0;

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateUI());
    }

    public void UpdateHealth(float health)
    {

        currentHealth = Mathf.Clamp(health,0,maxHealth);
        if (gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            StartCoroutine(UpdateUI());
        }
    }

    public void TakeDamage(float damage)
    {
        UpdateHealth(Mathf.Max(currentHealth - damage, 0));
    }

    IEnumerator UpdateUI()
    {
        int index, lastIndex=0;
        while(currentUIHealth!=currentHealth)
        {
            currentUIHealth= Mathf.MoveTowards(currentUIHealth, currentHealth, Time.deltaTime * updateSpeed);
            index = Mathf.FloorToInt(currentUIHealth);
            if (lastIndex != index) hearts[lastIndex].UpdateHealth(index-lastIndex);
            if (index < hearts.Count)
            {
                hearts[index].UpdateHealth(currentUIHealth - index);
                lastIndex = index;
            }
            yield return null;
        }
    }

    public void SetMaxHealth(float value)
    {
        if (maxHealth < value)
        {
            for (int i = 0; i < Mathf.CeilToInt(value - maxHealth); i++)
            {
                HealthChunk health=Instantiate(heart, transform);
                health.Init(showEmptyHearts);
                hearts.Add(health);
            }
            maxHealth = value;
        }
    }
}
