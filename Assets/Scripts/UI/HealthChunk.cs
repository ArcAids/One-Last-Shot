using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthChunk : MonoBehaviour
{
    [SerializeField] Image healthBar;
    bool showWhenEmpty;
    public void Init(bool showWhenEmpty)
    {
        this.showWhenEmpty = showWhenEmpty;
        UpdateHealth(0);
    }

    public void UpdateHealth(float percentage)
    {
        healthBar.fillAmount = percentage;
        if (showWhenEmpty)
            return;

        if(percentage==0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
