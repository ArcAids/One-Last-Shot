using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBehaviour : MonoBehaviour, IElementalWeapon
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] Elements element;
    [SerializeField] int magazine=1;
    [SerializeField] float fireRate=1;
    [SerializeField] SpriteRenderer model;
    [SerializeField] SpriteRenderer hands;
    [SerializeField] Cinemachine.CinemachineVirtualCamera cam;
    [SerializeField] float zoom;
    [SerializeField] new AudioSource audio;
    [SerializeField] UnityEvent onShot;

    float originalOrthographicSize;
    public Transform gunTransform { get => transform; }
    public int AmmoLeft => magazine;
    public bool IsEmpty { get =>magazine<=0; }
    public Elements Element { get => element; set
        {
            element = value;
            model.color = ElementalUtility.GetColor(value);
        }
    }

    float lastShotTimer;
    float LastShotTimer { get { return lastShotTimer; } set { lastShotTimer = value; if (value <= 0) canShoot = true; } }


    float shotTimeDelay;
    bool canShoot;

    private void Awake()
    {
        model = GetComponent<SpriteRenderer>();
        Element = Element;
        if(cam==null)
            cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        originalOrthographicSize = 10;
        shotTimeDelay = 1/fireRate;
    }

    public void Dequip()
    {
        transform.parent = null;
        if(hands!=null)
            hands.enabled = false;
        Color color = model.color;
        color.a = 0.4f;
        cam.m_Lens.OrthographicSize = originalOrthographicSize;
        model.color = color;
#if UNITY_ANDROID
        DisableInASecond();
#endif
    }

    public void DisableInASecond()
    {
        Invoke("Disable",2);

    }

    void Disable()
    {
        
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void Equip()
    {
        GetComponent<Collider2D>().enabled = false;
        if (hands!= null)
            hands.enabled = true;
        if(cam==null)
            cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        cam.m_Lens.OrthographicSize = zoom;
    }

    public bool Shoot()
    {
        if (magazine > 0 && canShoot)
        {
            IElementalShootable bullet=Instantiate(bulletPrefab, muzzle.position, muzzle.rotation, null).GetComponent<IElementalShootable>();
            bullet.SwitchElement(Element);
            if(audio!=null)
                audio?.Play();
            onShot.Invoke();
            bullet.Shoot();
            magazine--;
            canShoot = false;
            LastShotTimer = shotTimeDelay;
            return true;
        }
        return false;
    }

    public void FlipSpriteY(bool state)
    {
        model.flipY = state;
        hands.flipY = state;
    }

    private void Update()
    {
        if(!canShoot)
            LastShotTimer -= Time.deltaTime;
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
