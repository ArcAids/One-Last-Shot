using UnityEngine;

public interface IWeapon {

    Transform gunTransform { get; }
    void Shoot();
    void Equip();
    void Dequip();
}
