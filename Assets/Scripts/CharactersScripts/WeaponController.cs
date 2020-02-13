using UnityEngine;


public class WeaponController : MonoBehaviour, IElemental
{ 
    [SerializeField] Transform gunPivot;
    [SerializeField] Transform gunHolder;
    [SerializeField] IElementalWeapon weapon;
    [SerializeField] ElementalEventController elementController;
    [SerializeField] WeaponsEventController weaponEvent;
    [SerializeField] GunPointer pointer;
    

    Elements currentElement;
    IWeaponInput input;
    SpriteRenderer sprite;

    Vector3 aimDirection;
    float aimAngle;
    public Elements Element
    {
        get => currentElement; private set
        {
            currentElement = value;
        }
    }

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
        input.SetWeaponInputs();
        Aim();
        if(input.Shooting)
            PullTrigger();
    }

    void Equip(IElementalWeapon weapon)
    {
        this.weapon = weapon;
        weapon.Equip();
        weapon.SwitchElement(Element);
        pointer.Deactivate();
        gunHolder.localScale = new Vector2(1, 1);
        weapon.gunTransform.parent = gunHolder;
        weapon.gunTransform.localPosition = Vector2.zero;
        weapon.gunTransform.localRotation= Quaternion.identity;
        sprite = weapon.gunTransform.GetComponent<SpriteRenderer>();
    }


    void Aim()
    {
        aimDirection.x= input.MouseXDirection;
        aimDirection.y= input.MouseYDirection;
        aimAngle = Vector2.SignedAngle(Vector2.right,aimDirection);
        if (sprite != null)
        {
            if (aimDirection.x < 0)
                sprite.flipY = true;
            else
                sprite.flipY = false;
        }
        else
        if (aimDirection.x <0)
            gunHolder.localScale = new Vector2(1,-1);
        else
            gunHolder.localScale = new Vector2(1,1);
        gunPivot.rotation =Quaternion.Euler(0,0,aimAngle);
    }

    void PullTrigger()
    {
        if (weapon == null)
            return;

        if(weapon.Shoot())
            weaponEvent.OnWeaponShot();
        if (weapon.IsEmpty)
        {
            weapon.Dequip();
            weapon = null;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.weapon != null)
            return;
        IElementalWeapon weapon=collision.GetComponent<IElementalWeapon>();
        if (weapon!=null)
        {
            Equip(weapon);
        }
    }

    public void SwitchElement(Elements element)
    {
        Element = element;
        weapon?.SwitchElement(element);
    }




}
