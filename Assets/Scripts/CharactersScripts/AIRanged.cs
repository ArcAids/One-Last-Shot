using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRanged : AIFollowController, IWeaponInput
{
    [SerializeField] EnemyBullet bullet;

    public float MouseYDirection { get; private set; }

    public bool Shooting { get; private set; }

    public override void SetMovementInputs()
    {
        if (!canMove)
        {
            HorizontalInput = 0;
            VerticalInput = 0;
            return;
        }
        if (target != null)
        {
            float distance = UpdatePath();
            direction = GetDirectionToTarget(target.position);
            if (distance < targetDistance)
            {
                StartCoroutine(Shoot());

                HorizontalInput = 0;
                VerticalInput = 0;
                return;
            }

        }
        else
        if (!eatsCorpse)
            direction = defaultDirection;

        HorizontalInput = direction.x;
        VerticalInput = direction.y;
    }

    public void SetWeaponInputs()
    {
        MouseYDirection = target.position.y - transform.position.y;
        MouseXDirection = target.position.x - transform.position.x;
    }

    IEnumerator Shoot()
    {
        //IShootable shoot=Instantiate(bullet, transform.position + new Vector3(direction.x,direction.y), Quaternion.FromToRotation(Vector2.right, direction), null).GetComponent<IShootable>();
        //shoot.Shoot();
        canMove = false;
        yield return new WaitForSeconds(0.2f);
        Shooting = true;
        yield return null;
        Shooting = false;
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }
}
