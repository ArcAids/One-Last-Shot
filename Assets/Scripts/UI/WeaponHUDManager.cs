using ArcAid.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHUDManager : MonoBehaviour
{
    [SerializeField] TMP_Text weaponName;
    [SerializeField] TMP_Text ammoCountBox;
    [SerializeField] Image weaponSprite;
    int magSize;
    public void SetWeaponData(WeaponData weapon ,int currentAmmo)
    {
        if (weapon != null)
        {
            weaponName.text = weapon.GunName;
            gameObject.SetActive(true,true);
            weaponSprite.sprite = weapon.Sprite;
            magSize = weapon.Magazine;
        }
        else
            gameObject.SetActive(false,true);

        ammoCountBox.text = currentAmmo+"/"+magSize;
    }

    public void SetDefaultState()
    {
        SetWeaponData(null,0);
    }

    public void UpdateAmmoCount(int ammo)
    {
        ammoCountBox.text = ammo+"/"+magSize;
    }
}
