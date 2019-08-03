using UnityEngine;

public class WeaponBehaviour : MonoBehaviour, IElementalWeapon
{
    [SerializeField] BulletBehaviour bulletPrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] Elements element;
    [SerializeField] int magazine=1;
    [SerializeField] SpriteRenderer model;
    public Transform gunTransform { get => transform; }
    public Elements Element { get => element; set
        {
            element = value;
            model.color = ElementalUtility.GetColor(value);
        } }

    private void Awake()
    {
        model = GetComponent<SpriteRenderer>();
        Element = Element;   

    }
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

            IElementalShootable bullet=Instantiate(bulletPrefab, muzzle.position, muzzle.rotation, null);
            bullet.SwitchElement(Element);
            bullet.Shoot();
            magazine--;
        }
    }

    public void SwitchElement(Elements element)
    {
        Element = element;
        
    }

    public void Shoot(Elements element)
    {
        SwitchElement(element);
        Shoot();
    }
}
