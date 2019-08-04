using Cinemachine;
using UnityEngine;


public class WeaponController : MonoBehaviour, IElemental
{ 
    [SerializeField] float turnSpeed;
    [SerializeField] Transform gunPivot;
    [SerializeField] Transform gunHolder;
    [SerializeField] IElementalWeapon weapon;
    [SerializeField] ElementalEventController elementController;
    [SerializeField] WeaponsEventController weaponEvent;
    [SerializeField] GunPointer pointer;
    

    Elements currentElement;
    IWeaponInput input;
    CinemachineImpulseSource shaker;

    Vector3 aimDirection;
    float aimAngle;


    private void Start()
    {
        input = GetComponent<IWeaponInput>();
        shaker = GetComponent<CinemachineImpulseSource>();

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
        weapon.SwitchElement(currentElement);
        pointer.Deactivate();
        gunHolder.localScale = new Vector2(1, 1);
        weapon.gunTransform.parent = gunHolder;
        weapon.gunTransform.localPosition = Vector2.zero;
        weapon.gunTransform.localRotation= Quaternion.identity;

    }


    void Aim()
    {
        aimDirection.x= input.MouseXPosition - transform.position.x;
        aimDirection.y= input.MouseYPosition - transform.position.y;
        aimAngle = Vector2.SignedAngle(Vector2.right,aimDirection);
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

        weapon.Shoot();
       
        shaker.GenerateImpulse();
        weaponEvent.OnWeaponShot();
        weapon.Dequip();
        weapon = null;
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
        currentElement = element;
        weapon?.SwitchElement(element);
    }




}
