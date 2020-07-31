using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Events/WeaponEventController")]
public class WeaponsEventController : ScriptableObject
{
    [SerializeField] bool oneShot=true;
    List<IWeaponListener> listeners;

    public bool OneShot { get => oneShot; private set => oneShot = value; }

    public void SetOneShot(bool value)
    {
        OneShot = value;
    }
    public void RegisterElementSwitch(IWeaponListener elemental)
    {
        if (listeners == null)
            listeners = new List<IWeaponListener>();
        if (!listeners.Contains(elemental))
            listeners.Add(elemental);
    }

    public void DeregisterElmentSwitch(IWeaponListener elemental)
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

    public void OnWeaponDequip()
    {
        if (listeners == null)
            return;
        foreach (var listener in listeners)
        {
            listener.OnDequip();
        }
    }
}
