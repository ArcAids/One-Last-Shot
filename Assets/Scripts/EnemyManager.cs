using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] Transform player;
    [SerializeField] AIFollowController zombie;
    [SerializeField] AIFollowController worm;
    [SerializeField] AIFollowController ghost;

    public void Spawn(Elements element)
    {
       
        switch (element)
        {
            case Elements.Fire:
                Spawn(zombie);
                break;
            case Elements.Ice:
                Spawn(worm);
                break;
            case Elements.Slash:
                Spawn(ghost);
                break;
            default:
                break;
        }
    }

    public void Spawn(AIFollowController enemy)
    {
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
        Instantiate(enemy, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity, null).SetTarget(player);
    }

}
