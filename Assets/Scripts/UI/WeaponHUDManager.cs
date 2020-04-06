using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHUDManager : MonoBehaviour
{
    [SerializeField] TMP_Text weaponName;
    [SerializeField] TMP_Text ammoCountBox;
    [SerializeField] Image weaponSprite;
    int magSize;
    public void SetWeaponData(string name,int magSize,int currentAmmo,Sprite sprite)
    {
        weaponName.text = name;
        if (sprite != null)
        {
            gameObject.SetActive(true);
            weaponSprite.sprite = sprite;
            weaponSprite.enabled=true;
        }
        else
            gameObject.SetActive(false);
        this.magSize = magSize;
        ammoCountBox.text = currentAmmo+"/"+magSize;
    }

    public void SetDefaultState()
    {
        SetWeaponData("empty",0,0,null);
    }

    public void UpdateAmmoCount(int ammo)
    {
        ammoCountBox.text = ammo+"/"+magSize;
    }
}
