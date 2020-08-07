using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHUD : MonoBehaviour
{
    [SerializeField] HealthChunk heart;
    [SerializeField] int numberOfChunks;
    [SerializeField] float updateSpeed;

    [SerializeField] Color inActiveColor;
    [SerializeField] bool showWhenEmpty;
    [SerializeField] bool fadeIfNotFull;
    [SerializeField] AudioSource heartBeat;
    List<HealthChunk> hearts=new List<HealthChunk>();

    float currentHealth;
    float step;
    float currentUIHealth;
    float maxValue=0;
    private Color originalColor;
    int currentChunkIndex=0;
    public bool FadeIfNotFull { get => fadeIfNotFull; private set => fadeIfNotFull = value; }
    public bool ShowWhenEmpty { get => showWhenEmpty; private set => showWhenEmpty = value; }
    public Color InActiveColor { get => inActiveColor; private set => inActiveColor = value; }
    public Color OriginalColor { get => originalColor; private set => originalColor = value; }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateUI());
    }

    public void UpdateHealth(float health)
    {
        currentHealth = Mathf.Clamp(health,0,maxValue);
        if (gameObject.activeInHierarchy)
        {
            StopAllCoroutines();
            StartCoroutine(UpdateUI());
            if (health == 0)
                heartBeat.Stop();
            else if (health <= 1)
                heartBeat?.Play();
        }
    }

    public void TakeDamage(float damage)
    {
        UpdateHealth(Mathf.Max(currentHealth - damage, 0));
    }

    IEnumerator UpdateUI()
    {
        int index;
        while(currentUIHealth!=currentHealth)
        {
            currentUIHealth= Mathf.MoveTowards(currentUIHealth, currentHealth, Time.deltaTime * updateSpeed);
            index = Mathf.FloorToInt(currentUIHealth* step);

            if (index <= hearts.Count && currentChunkIndex != index)
                //hearts[currentChunkIndex].UpdateHealth((index-currentChunkIndex));
                MoveToNewUIChunkIndex(index);


                hearts[currentChunkIndex].UpdateHealth(step* currentUIHealth - currentChunkIndex);
            
            yield return null;
        }
    }

    void MoveToNewUIChunkIndex(int newIndex)
    {
        int timer=0;
        while(timer < 100 && currentChunkIndex !=newIndex)
        {
            hearts[currentChunkIndex].UpdateHealth(newIndex>currentChunkIndex?1:0);
            currentChunkIndex=Mathf.Clamp(Mathf.FloorToInt(Mathf.MoveTowards(currentChunkIndex, newIndex, 1)),0,hearts.Count-1);
            timer++;
        }    
    }

    public void SetUpChunks(float maxValue, int numberOfChunks = -1)
    {
        if (numberOfChunks > 0)
            this.numberOfChunks = numberOfChunks;

        for (int i = 0; i < this.numberOfChunks; i++)
        {
            HealthChunk health=Instantiate(heart, transform);
            //health.transform.SetAsFirstSibling();
            health.Init(this);
            hearts.Add(health);
        }
        this.maxValue = maxValue;
        step = this.numberOfChunks / this.maxValue;
        
    }

    public void SetOriginalColor(Color color)
    {
        OriginalColor = color;
    }
}
