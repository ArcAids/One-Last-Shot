using UnityEngine;

public interface IWeapon {

    Transform gunTransform { get; }

    bool IsEmpty { get; }
    bool Shoot();
    void Equip();
    void Dequip();
}

public interface IElementalWeapon : IWeapon, IElemental
{
    void Shoot(Elements element);
}