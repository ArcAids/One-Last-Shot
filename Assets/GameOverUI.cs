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
        SetInteractable(false);
        currentWave.text = "Waves Survived:"+ manager.currentWave;
        enemiesKilled.text = "Creatures Killed:"+ enemyManager.totalEnemiesKilled;
        bossesKilled.text = "Bosses Killed:"+ enemyManager.totalBossesKilled;
        Invoke("SetInteractable", 2);
    }

    void SetInteractable()
    {
        SetInteractable(true);
    }
    void SetInteractable(bool value)
    {
        Button button;
        if(TryGetComponent(out button))
            button.interactable = value;
    }
}
