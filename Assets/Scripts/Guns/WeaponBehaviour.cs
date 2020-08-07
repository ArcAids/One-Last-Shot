using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBehaviour : MonoBehaviour, IElementalWeapon
{
    [SerializeField] WeaponData data;
    [SerializeField] Transform muzzle;
    [SerializeField] Elements element;
    [SerializeField] int ammoLeft=1;
    [SerializeField] SpriteRenderer hands;

    [SerializeField] UnityEvent onShot;

    SpriteRenderer model;
    Cinemachine.CinemachineVirtualCamera cam;
    AudioSource audioSource;
    float originalOrthographicSize=10;
    public Transform GunTransform { get => transform; }
    public int AmmoLeft => ammoLeft;
    public bool IsEmpty { get =>ammoLeft<=0; }
    public Elements Element { get => element; set
        {
            element = value;
            model.color = ElementalUtility.GetColor(value);
        }
    }

    public WeaponData WeaponData { get => data; }

    float lastShotTimer;

    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        if (model == null)
            TryGetComponent(out model);
        Element = Element;
        if(cam==null)
            cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
    }

    public void Dequip()
    {
        transform.parent = null;
        if(hands!=null)
            hands.enabled = false;
        Color color = model.color;
        color.a = 0.4f;
        if(cam!=null)
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
        Collider2D collider;
        if (hands!= null)
            hands.enabled = true;
        if (TryGetComponent(out collider))
        { 
            collider.enabled = false;
            if(cam==null)
                cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
            cam.m_Lens.OrthographicSize = data.Zoom;
        }
    }

    public bool Shoot()
    {
        if (ammoLeft > 0 && lastShotTimer<Time.time)
        {
            IElementalShootable bullet=Instantiate(data.BulletPrefab, muzzle.position, muzzle.rotation, null).GetComponent<IElementalShootable>();
            bullet.SwitchElement(Element);
            if(audioSource!=null)
                audioSource?.Play();
            onShot.Invoke();
            bullet.Shoot();
            ammoLeft--;
            lastShotTimer = Time.time+data.DelayBetweenShots;
            return true;
        }
        return false;
    }

    public void Reload()
    {
        ammoLeft = data.Magazine;
    }

    public void FlipSpriteY(bool state)
    {
        model.flipY = state;
        hands.flipY = state;
        if (state)
        { 
            Vector2 offset = data.HoldOffset;
            offset.y = -offset.y;
            transform.localPosition = offset;
        }
        else
            transform.localPosition = data.HoldOffset;
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
