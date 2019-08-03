using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{ 
    [SerializeField] float turnSpeed;
    [SerializeField] Transform gunPivot;
    [SerializeField] Transform gunHolder;
    [SerializeField] IWeapon weapon;
    IWeaponInput input;

    Vector3 aimDirection;
    float aimAngle;
    private void Start()
    {
        input = GetComponent<IWeaponInput>();

    }

    private void Update()
    {
        input.SetInputs();
        Aim();
        if(input.shooting)
            PullTrigger();
    }

    void Equip(IWeapon weapon)
    {
        this.weapon = weapon;
        weapon.Equip();
        weapon.gunTransform.parent = gunHolder;
        weapon.gunTransform.localPosition = Vector2.zero;
        weapon.gunTransform.localRotation= Quaternion.identity;

    }

    void Aim()
    {
        aimDirection.x= input.MouseXPosition - transform.position.x;
        aimDirection.y= input.MouseYPosition - transform.position.y;

        aimAngle = Vector2.SignedAngle(Vector2.right,aimDirection);
        gunPivot.rotation =Quaternion.Euler(0,0,aimAngle);
    }

    void PullTrigger()
    {
        if (weapon == null)
            return;
        weapon.Shoot();
        weapon.Dequip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IWeapon weapon=collision.GetComponent<IWeapon>();
        if (weapon!=null)
        {
            Equip(weapon);
        }
    }
}
