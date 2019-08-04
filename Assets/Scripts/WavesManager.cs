using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour, IEnemyDeathListener
{
    [System.Serializable]
    class Wave
    {
        public List<AIFollowController> enemies;
        public AIFollowController boss;
        public float delayBetweenSpawns;
        public int totalEnemies;
    }

    [SerializeField] List<Wave> wavesData;
    [SerializeField] float delayBeforeWaves;
    [SerializeField] EnemyDeathEvent deathEvent;
    [SerializeField] TMPro.TMP_Text waver;
    EnemyManager enemyManager;

    int enemiesAlive;
    public int totalEnemiesKilled=0;
    public int totalBossesKilled=0;
    public int currentWave=-1;
    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    private void OnEnable()
    {
        deathEvent.RegisterElmentSwitch(this);   
    }

    private void OnDisable()
    {
        deathEvent.DeregisterElmentSwitch(this);   
    }
    private void Start()
    {
        StartWave();
    }

    void StartWave()
    {
        currentWave++;
        if(currentWave>=wavesData.Count)
        {
            GetComponent<GameManager>().YouWin();
        }
        else
            StartCoroutine(WaveSpawner(wavesData[currentWave]));
    }

    IEnumerator WaveSpawner(Wave wave)
    {
        wave.totalEnemies = wave.enemies.Count + (wave.boss==null?0:1);
        enemiesAlive = wave.totalEnemies;
        waver.gameObject.SetActive(true);
        waver.text = "Wave "+(currentWave+1);
        yield return new WaitForSeconds(delayBeforeWaves);
        waver.gameObject.SetActive(false);
        foreach (var enemy in wave.enemies)
        {
            yield return new WaitForSeconds(wave.delayBetweenSpawns);
            enemyManager.Spawn(enemy);
        }
        yield return new WaitForSeconds(wave.delayBetweenSpawns);
        if(wave.boss!=null)
            enemyManager.Spawn(wave.boss);

    }

    public void OnDeath()
    {
        enemiesAlive--;
        Debug.Log("enemies left: "+ enemiesAlive);
        if (enemiesAlive <= 0)
            StartWave();
        totalEnemiesKilled++;
    }
    public void OnBossDeath()
    {
        enemiesAlive--;
        Debug.Log("enemies left: "+ enemiesAlive);
        if (enemiesAlive <= 0)
            StartWave();
        totalBossesKilled++;
    }
}
