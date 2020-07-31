using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TMP_Text currentWave;
    [SerializeField] TMP_Text enemiesKilled;
    [SerializeField] TMP_Text bossesKilled;
    [SerializeField] WavesManager manager;
    [SerializeField] EnemyDeathEvent enemyManager;
    
    private void OnEnable()
    {
        currentWave.text = "Waves Survived:"+ manager.currentWave;
        enemiesKilled.text = "Creatures Killed:"+ enemyManager.totalEnemiesKilled;
        bossesKilled.text = "Bosses Killed:"+ enemyManager.totalBossesKilled;
        Invoke("SetInteractable", 3);
    }

    void SetInteractable()
    {
        Button button;
        if(TryGetComponent(out button))
            button.interactable = true;
    }
}
