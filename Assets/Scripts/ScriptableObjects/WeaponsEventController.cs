using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponsEventController : ScriptableObject
{
    List<IWeaponShotListener> listeners;
    public void RegisterElementSwitch(IWeaponShotListener elemental)
    {
        if (listeners == null)
            listeners = new List<IWeaponShotListener>();
        if (!listeners.Contains(elemental))
            listeners.Add(elemental);
    }

    public void DeregisterElmentSwitch(IWeaponShotListener elemental)
    {
        listeners.Remove(elemental);
    }

    public void OnWeaponShot()
    {
        if (listeners == null)
            return;
        foreach (var listener in listeners)
        {
            listener.OnShot();
        }
    }
}
