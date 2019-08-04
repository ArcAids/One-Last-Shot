using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TMP_Text currentWave;
    [SerializeField] TMP_Text enemiesKilled;
    [SerializeField] TMP_Text bossesKilled;
    [SerializeField] WavesManager manager;

    private void OnEnable()
    {
        currentWave.text = "Waves Survived:"+ manager.currentWave;
        enemiesKilled.text = "Creatures Killed:"+ manager.totalEnemiesKilled;
        bossesKilled.text = "Bosses Killed:"+ manager.totalBossesKilled;
    }
}
