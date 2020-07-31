using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour, IWeaponListener
{
    [SerializeField] WeaponsEventController weaponsEventController;
    [SerializeField] List<WeaponBehaviour> weapons;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GunPointer pointer;
    [SerializeField] WeaponBehaviour startingGun;


    private void Start()
    {
        if (startingGun != null && !weaponsEventController.OneShot)
        {
            Destroy(startingGun.gameObject);
            SpawnWeapon();
        }
    }

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
        
    }

    void SpawnWeapon()
    {
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
        int randomWeaponIndex= Random.Range(0, weapons.Count);

        WeaponBehaviour gun =Instantiate(weapons[randomWeaponIndex],spawnPoints[randomSpawnPointIndex].transform.position,Quaternion.identity,null);
        if(!weaponsEventController.OneShot)
            gun.Reload();
        pointer.SetGun(gun.transform);

    }

    public void OnDequip()
    {
        SpawnWeapon();
    }
}
