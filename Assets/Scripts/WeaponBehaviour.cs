using UnityEngine;

public class WeaponBehaviour : MonoBehaviour, IWeapon
{
    [SerializeField] BulletBehaviour bulletPrefab;
    [SerializeField] Transform muzzle;

    [SerializeField] int magazine=1;
    public Transform gunTransform { get => transform; }

    
    public void Dequip()
    {
        transform.parent = null;
        Invoke("Disable",1);
    }

    void Disable()
    {
        GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    public void Equip()
    {
        magazine = 1;
        GetComponent<Collider2D>().enabled = false;
    }

    public void Shoot()
    {
        if (magazine > 0)
        {
            Instantiate(bulletPrefab, muzzle.position, muzzle.rotation, null).Shoot();
            magazine--;
        }
    }
}
