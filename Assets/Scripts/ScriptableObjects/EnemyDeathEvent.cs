using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyDeathEvent : ScriptableObject
{
    [Header("Prefabs")]
    [SerializeField] AIFollowController zombie;
    [SerializeField] AIFollowController worm;
    [SerializeField] AIFollowController ghost;
    [SerializeField] AIFollowController zombieBoss;
    [SerializeField] AIFollowController wormBoss;
    [SerializeField] AIFollowController ghostBoss;

    [System.NonSerialized]
    public List<AIFollowController> spawnedEnemies = new List<AIFollowController>();
    public int enemiesAlive { get => spawnedEnemies.Count;  }
    public int totalEnemiesKilled = 0;
    public int totalBossesKilled = 0;

    List<IEnemyDeathListener> listeners;

    public AIFollowController GetEnemyPrefab(Elements element,bool boss)
    {
        switch (element)
        {
            case Elements.Fire:
                return boss ? zombieBoss : zombie;
            case Elements.Ice:
                return boss? wormBoss:worm;
            case Elements.Slash:
                return boss? ghostBoss:ghost;
        }
        return zombie;
    }

    public AIFollowController GetEnemyPrefab(Enemy enemy)
    {
        return GetEnemyPrefab(enemy.element,enemy.isBoss);
    }

    [ContextMenu("KillThemAll")]
    public void KillAllEnemies()
    {
        List<AIFollowController> allEnemies = new List<AIFollowController>();
        allEnemies.AddRange(spawnedEnemies);
        foreach (var enemy in allEnemies)
        {
            enemy.GetComponent<ElementalHealthBehaviour>().TakeDamage(999);
        }
        spawnedEnemies.Clear();
    }

    public void ResetData()
    {
        totalBossesKilled = 0;
        totalEnemiesKilled = 0;
        spawnedEnemies.Clear();
    }

    public void RegisterEnemyDeathEvent(IEnemyDeathListener elemental)
    {
        if (listeners == null)
            listeners = new List<IEnemyDeathListener>();
        if (!listeners.Contains(elemental))
            listeners.Add(elemental);
    }

    public void DeRegisterEnemyDeathEvent(IEnemyDeathListener elemental)
    {
        if (listeners == null)
            return;
        listeners.Remove(elemental);
    }

    public void OnDeath(AIFollowController corpse)
    {
        if (spawnedEnemies.Contains(corpse))
        {
            spawnedEnemies.Remove(corpse);
            totalEnemiesKilled++;
        }

        if (listeners == null)
            return;
        foreach (var listener in listeners)
        {
            listener.OnDeath();
        }
    }

    public void OnBossDeath(AIFollowController corpse)
    {
        if (spawnedEnemies.Contains(corpse))
        {
            spawnedEnemies.Remove(corpse);
            totalBossesKilled++;
        }
        if (listeners == null)
            return;
        foreach (var listener in listeners)
        {
            listener.OnBossDeath();
        }
    }
}


