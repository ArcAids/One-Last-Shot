using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthChunk : MonoBehaviour
{
    [SerializeField] Image healthBar;
    HealthHUD healthHUD;
    public void Init(HealthHUD healthHUD)
    {
        if (!healthHUD) return;

        this.healthHUD = healthHUD;
        healthHUD.SetOriginalColor(healthBar.color);
        UpdateHealth(0);
    }

    public void UpdateHealth(float percentage)
    {
        healthBar.fillAmount = percentage;
        if (healthHUD.FadeIfNotFull)
        {
            if (percentage >= 0.99f)
                healthBar.color = healthHUD.OriginalColor;
            else
                healthBar.color = healthHUD.InActiveColor;
        }

        if (healthHUD.ShowWhenEmpty)
            return;

        if (percentage <= 0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
