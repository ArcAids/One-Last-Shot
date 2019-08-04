using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPointer : MonoBehaviour
{
    [SerializeField]
    Transform gun;
    Vector3 aimDirection;
    float aimAngle;
    private void Update()
    {
        if (gun == null)
            return;

        aimDirection = gun.position - transform.position;

        aimAngle = Vector2.SignedAngle(Vector2.right, aimDirection);
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    internal void SetGun(Transform transform)
    {
        gun = transform;
        Activate();
    }
}
