using UnityEngine;

public interface IWeapon {

    Transform GunTransform { get; }
    WeaponData WeaponData { get; }
    int AmmoLeft { get; }
    bool Shoot();
    void Equip();
    void Dequip();
    void FlipSpriteY(bool flipState);
}

public interface IElementalWeapon : IWeapon, IElemental
{
    void Shoot(Elements element);
}