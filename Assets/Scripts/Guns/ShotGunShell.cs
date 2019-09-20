using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunShell :MonoBehaviour, IElementalShootable
{
    [SerializeField]
    List<BulletBehaviour> sharpnels;
    Elements element;
    public Elements Element
    {
        get => element; private set
        {
            element = value;
        }
    }

    public void Shoot()
    {
        foreach (var sharpnel in sharpnels)
        {
            sharpnel.SwitchElement(Element);
            sharpnel.Shoot();
        }
        Invoke("Disable", 3);
    }

    void Disable()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public void SwitchElement(Elements element)
    {
        Element = element;
    }
}
