using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponData : ScriptableObject
{

    [SerializeField] string gunName;

    [SerializeField] int magazine = 1;
    [SerializeField] float fireRate = 1;
    [SerializeField] float zoom;
    [SerializeField] Vector2 holdOffset;

    [SerializeField] Sprite sprite;
    [SerializeField] AudioClip shotSound;
    [SerializeField] GameObject bulletPrefab;

    public WeaponData()
    {
        DelayBetweenShots = 1 / FireRate;
    }


    public string GunName { get => gunName;}
    public int Magazine { get => magazine;}
    public float FireRate { get => fireRate;}
    public float Zoom { get => zoom;}
    public Vector2 HoldOffset { get => holdOffset;}
    public Sprite Sprite { get => sprite;}
    public AudioClip ShotSound { get => shotSound;}
    public GameObject BulletPrefab { get => bulletPrefab;}
    public float DelayBetweenShots { get; private set; }
}
