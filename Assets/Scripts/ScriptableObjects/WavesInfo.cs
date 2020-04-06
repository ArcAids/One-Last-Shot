using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public Elements element;
    public bool isBoss;

}
[System.Serializable]
public class Wave
{
    public List<Enemy> enemies;
    public float delayBetweenSpawns;
}

[CreateAssetMenu]
public class WavesInfo : ScriptableObject
{
    public List<Wave> waves;
    public bool endless;
}
