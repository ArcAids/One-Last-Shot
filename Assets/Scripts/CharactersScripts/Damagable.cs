using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour , ITakeDamage
{
    public bool IsAlive => throw new System.NotImplementedException();

    public float MaxHealth => throw new System.NotImplementedException();

    public void OnDeath()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}

[System.Serializable]
public class DamageData
{
    public float slash;
    public float cold;
    public float fire;

    public float stunChance;
    public float stunDuration;
}