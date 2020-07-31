using UnityEngine;

public class WeaponController : MonoBehaviour, IElemental
{ 
    [SerializeField] Transform gunPivot;
    [SerializeField] Transform gunHolder;
    [SerializeField] IElementalWeapon weapon;
    [SerializeField] WeaponBehaviour currentWeapon;
    [SerializeField] ElementalEventController elementController;
    [SerializeField] WeaponsEventController weaponEvent;
    [SerializeField] WeaponHUDManager hudManager;
    [SerializeField] GunPointer pointer;
    

    Elements currentElement;
    IWeaponInput input;

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
        TryGetComponent(out input);
        IElementalWeapon startWeapon;
        if (currentWeapon != null && currentWeapon.TryGetComponent(out startWeapon))
            Equip(startWeapon);
    }

    private void OnEnable()
    {
        elementController?.RegisterElmentSwitch(this);
    }

    private void OnDisable()
    {
        elementController?.DeregisterElmentSwitch(this);
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
        weapon.GunTransform.TryGetComponent(out currentWeapon);
        weapon.Equip();
        weapon.SwitchElement(Element);
        pointer?.Deactivate();
        gunHolder.localScale = new Vector2(1, 1);
        weapon.GunTransform.parent = gunHolder;
        weapon.GunTransform.localRotation= Quaternion.identity;
        weapon.GunTransform.localPosition = weapon.WeaponData.HoldOffset;
      
        hudManager?.SetWeaponData(weapon.WeaponData,weapon.AmmoLeft);
    }


    void Aim()
    {
        aimDirection.x= input.MouseXDirection;
        aimDirection.y= input.MouseYDirection;
        aimAngle = Vector2.SignedAngle(Vector2.right,aimDirection);
        //if (sprite != null)
        {
            weapon?.FlipSpriteY(aimDirection.x < 0);
        }
        //else
        //if (aimDirection.x <0)
        //    gunHolder.localScale = new Vector2(1,-1);
        //else
        //    gunHolder.localScale = new Vector2(1,1);

        gunPivot.rotation =Quaternion.Euler(0,0,aimAngle);
    }

    void PullTrigger()
    {
        if (weapon == null)
            return;

        if (weapon.Shoot())
        {
            weaponEvent?.OnWeaponShot();
            hudManager?.UpdateAmmoCount(weapon.AmmoLeft);
        }
        if (weapon.AmmoLeft<=0)
        {
            weapon.Dequip();
            weaponEvent?.OnWeaponDequip();
            weapon = null;
            hudManager?.SetDefaultState();
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
