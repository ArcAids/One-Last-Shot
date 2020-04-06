using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] Transform player;
    [SerializeField] EnemyDeathEvent enemyManager;

    public void Spawn(Elements element,bool boss=false)
    {
        Spawn(enemyManager.GetEnemyPrefab(element,boss));
    }
    public void Spawn(Enemy enemy)
    {
        Spawn(enemyManager.GetEnemyPrefab(enemy));
    }

    [ContextMenu("SpawnRandom")]
    public void SpawnEnemy()
    {
        Spawn((Elements)Random.Range(0,2));
    }

    public void Spawn(AIFollowController enemy)
    {
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
        AIFollowController baby = Instantiate(enemy, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity, null);
        baby.SetTarget(player);
        enemyManager.spawnedEnemies.Add(baby);
    }

}
