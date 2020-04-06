using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class WavesManager : MonoBehaviour, IEnemyDeathListener
{
    [System.Serializable]
    class WaveData
    {
        public List<AIFollowController> enemies;
        public AIFollowController boss;
        public float delayBetweenSpawns;
        public UnityEvent onWaveStart;
    }

    //[System.Serializable]
    //class WaveData
    //{
    //    public List<SpawnRates> enemiesToSpawn;
    //    public int numberOfEnemies;
    //    public int numberOfBosses;

    //}

     
    //[System.Serializable]
    //class SpawnRates
    //{
    //    public AIFollowController enemyPrefab;
    //    public float chance;
    //}
    //[SerializeField] WaveData enemiesKind;

    [SerializeField] List<WaveData> wavesData;
    [SerializeField] WavesInfo wavesInfo;
    [SerializeField] float delayBeforeWaves;
    [SerializeField] EnemyDeathEvent deathEvent;
    [SerializeField] TMPro.TMP_Text waver;
    EnemySpawner enemyManager;

    bool isSpawning=false;
    public int currentWave=-1;
    private void Awake()
    {
        
        enemyManager = GetComponent<EnemySpawner>();
    }

    private void OnEnable()
    {
        deathEvent.RegisterEnemyDeathEvent(this);   
    }

    private void OnDisable()
    {
        deathEvent.DeRegisterEnemyDeathEvent(this);   
    }
    private void Start()
    {
        deathEvent.ResetData();
        StartWave();
    }

    void StartWave()
    {
        currentWave++;
        if(wavesInfo!=null && !wavesInfo.endless)
        {
            if (currentWave >= wavesInfo.waves.Count)
            {
                GetComponent<GameManager>()?.YouWin();
                return;
            }
            //if (wavesData != null)
               // StartCoroutine(WaveSpawner(wavesData[currentWave]));
            StartCoroutine(WaveSpawner(wavesInfo.waves[currentWave]));
        }
        else
            StartCoroutine(WaveSpawner(currentWave,(currentWave+1)*3,currentWave,Mathf.Max((20-currentWave)*0.1f,0.5f)));
    }

    IEnumerator WaveSpawner(WaveData wave)
    {
        wave.onWaveStart.Invoke();
        waver.gameObject.SetActive(true);
        waver.text = "Wave "+(currentWave+1);
        yield return new WaitForSeconds(delayBeforeWaves);
        waver.gameObject.SetActive(false);
        isSpawning = true;
        foreach (var enemy in wave.enemies)
        {
            yield return new WaitForSeconds(wave.delayBetweenSpawns);
            enemyManager.Spawn(enemy);
        }
        yield return new WaitForSeconds(wave.delayBetweenSpawns);
        if(wave.boss!=null)
            enemyManager.Spawn(wave.boss);
        isSpawning = false;
    }
    IEnumerator WaveSpawner(Wave wave)
    {
        waver.gameObject.SetActive(true);
        waver.text = "Wave "+(currentWave+1);
        yield return new WaitForSeconds(delayBeforeWaves);
        waver.gameObject.SetActive(false);
        isSpawning = true;
        foreach (var enemy in wave.enemies)
        {
            yield return new WaitForSeconds(wave.delayBetweenSpawns);
            enemyManager.Spawn(enemy);
        }
        isSpawning = false;
    }

    IEnumerator WaveSpawner(int waveNumber, int numberOfEnemies, int numberOfBosses, float delayBetweenEnemies)
    {
        waver.gameObject.SetActive(true);
        waver.text = "Wave " + (currentWave + 1);
        isSpawning = true;

        int enemiesAlive = numberOfEnemies + numberOfBosses;
        float bossChance = (float)numberOfBosses/enemiesAlive;
        int bossesToSpawn=numberOfBosses;
        bool isBoss;

        yield return new WaitForSeconds(delayBeforeWaves);
        waver.gameObject.SetActive(false);
        for (int i = 0; i < enemiesAlive; i++)
        {
            isBoss = bossesToSpawn<=0?false: Random.Range(0f, 1f) < bossChance;
            if (isBoss)
                bossesToSpawn--;
            enemyManager.Spawn((Elements)Random.Range(0, 3),isBoss);
            yield return new WaitForSeconds(delayBetweenEnemies);
        }
        isSpawning = false;
    }

    public void OnDeath()
    {
        Debug.Log("enemies left: "+ deathEvent.enemiesAlive);
        if (!isSpawning && deathEvent.enemiesAlive <= 0)
            StartWave();
    }
    public void OnBossDeath()
    {
        //Debug.Log("enemies left: "+ enemiesAlive);
        if (!isSpawning && deathEvent.enemiesAlive <= 0)
            StartWave();
    }
}
