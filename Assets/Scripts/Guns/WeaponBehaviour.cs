using UnityEngine;

public class WeaponBehaviour : MonoBehaviour, IElementalWeapon
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] Elements element;
    [SerializeField] int magazine=1;
    [SerializeField] SpriteRenderer model;
    [SerializeField] Sprite heldSprite;
    Sprite originalSprite;
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
        originalSprite = model.sprite;

    }
    public void Dequip()
    {
        transform.parent = null;
        model.sprite = originalSprite;
        Color color = model.color;
        color.a = 0.4f;
        model.color = color;
        Invoke("Disable",1);
    }

    void Disable()
    {
        GetComponent<Collider2D>().enabled = true;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void Equip()
    {
        GetComponent<Collider2D>().enabled = false;
        if (heldSprite != null)
            model.sprite = heldSprite;
    }

    public void Shoot()
    {
        if (magazine > 0)
        {

            IElementalShootable bullet=Instantiate(bulletPrefab, muzzle.position, muzzle.rotation, null).GetComponent<IElementalShootable>();
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
