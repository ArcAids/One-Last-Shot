using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour, IElemental
{ 
    [SerializeField] float turnSpeed;
    [SerializeField] Transform gunPivot;
    [SerializeField] Transform gunHolder;
    [SerializeField] IElementalWeapon weapon;
    [SerializeField] ElementalEventController elementController;
    IWeaponInput input;

    Vector3 aimDirection;
    float aimAngle;


    private void Start()
    {
        input = GetComponent<IWeaponInput>();

    }

    private void OnEnable()
    {
        elementController.RegisterElmentSwitch(this);
    }

    private void OnDisable()
    {
        elementController.DeregisterElmentSwitch(this);
    }

    private void Update()
    {
        input.SetInputs();
        Aim();
        if(input.shooting)
            PullTrigger();
    }

    void Equip(IElementalWeapon weapon)
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
        IElementalWeapon weapon=collision.GetComponent<IElementalWeapon>();
        if (weapon!=null)
        {
            Equip(weapon);
        }
    }

    public void SwitchElement(Elements element)
    {
        weapon.SwitchElement(element);
    }
}
