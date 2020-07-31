using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletBehaviour
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger)
            return;

        if (penetration > 0)
        {
            ITakeDamage target;
            collision.TryGetComponent(out target);
            if (target != null && !(target is ITakeElementalDamage) && target.IsAlive && DoRawDamage(target))
            {
                penetration--;
            }
            ApplyForce(collision.GetComponent<Rigidbody2D>());
            if (penetration <= 0)
                Disable();
        }
        else
            Disable();
    }
}
