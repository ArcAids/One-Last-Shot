using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour, IWeaponShotListener
{
    [SerializeField] WeaponsEventController weaponsEventController;
    [SerializeField] List<WeaponBehaviour> weapons;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GunPointer pointer;

    private void OnEnable()
    {
        weaponsEventController.RegisterElementSwitch(this);
    }

    private void OnDisable()
    {
        weaponsEventController.DeregisterElmentSwitch(this);
    }

    public void OnShot()
    {
        SpawnWeapon();
    }

    void SpawnWeapon()
    {
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
        int randomWeaponIndex= Random.Range(0, weapons.Count);

        WeaponBehaviour gun =Instantiate(weapons[randomWeaponIndex],spawnPoints[randomSpawnPointIndex].transform.position,Quaternion.identity,null);
        pointer.SetGun(gun.transform);

    }
}
