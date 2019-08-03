using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{ 
    [SerializeField] float turnSpeed;
    [SerializeField] Transform gunHolder;
    [SerializeField] IWeapon Gun;
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
    }

    void Aim()
    {
        aimDirection.x= input.MouseXPosition - transform.position.x;
        aimDirection.y= input.MouseYPosition - transform.position.y;

        aimAngle = Vector2.SignedAngle(Vector2.right,aimDirection);
        gunHolder.rotation =Quaternion.Euler(0,0,aimAngle);
    }
}
