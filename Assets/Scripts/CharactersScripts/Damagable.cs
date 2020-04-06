using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public void TakeDamage(DamageData damage)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        throw new System.NotImplementedException();
    }
}

[CreateAssetMenu]
public class DamageType : ScriptableObject
{
    public DamageData data; 
    [Header("Data")]
    public Color color;
    public ParticleSystem effectPrefab;
}


[System.Serializable]
public class DamageData
{
    public float value; //how much base damage

    public DamageEffectsStats effectsStats;

    public float stunChance;
    public float stunDuration;
}

[System.Serializable]
public class DamageEffectsStats
{
    public float chance;            //chance of something to happen like fire or freeze
    public float duration;          // for how long it stays
    public float damageOverTime;    //damage it does per second
    public float speedMultiplier;   //how much to slow target
    public bool spreads;            //does it affect those who touches the target
}


public class PlayerStats
{
    public float maxHealth;
    public float walkSpeed;
}