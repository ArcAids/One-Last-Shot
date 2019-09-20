using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyDeathEvent : ScriptableObject
{
    List<IEnemyDeathListener> listeners;
    public void RegisterElmentSwitch(IEnemyDeathListener elemental)
    {
        if (listeners == null)
            listeners = new List<IEnemyDeathListener>();
        if (!listeners.Contains(elemental))
            listeners.Add(elemental);
    }

    public void DeregisterElmentSwitch(IEnemyDeathListener elemental)
    {
        if (listeners == null)
            return;
        listeners.Remove(elemental);
    }

    public void OnDeath()
    {
        if (listeners == null)
            return;
        foreach (var listener in listeners)
        {
            listener.OnDeath();
        }
    }

    public void OnBossDeath()
    {
        if (listeners == null)
            return;
        foreach (var listener in listeners)
        {
            listener.OnBossDeath();
        }
    }
}


