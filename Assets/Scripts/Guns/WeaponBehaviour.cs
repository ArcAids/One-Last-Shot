﻿using UnityEngine;
using UnityEngine.Events;

public class WeaponBehaviour : MonoBehaviour, IElementalWeapon
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] Elements element;
    [SerializeField] int magazine=1;
    [SerializeField] SpriteRenderer model;
    [SerializeField] SpriteRenderer hands;
    [SerializeField] Cinemachine.CinemachineVirtualCamera cam;
    [SerializeField] float zoom;
    [SerializeField] new AudioSource audio;
    [SerializeField] UnityEvent onShot;

    float originalOrthographicSize;
    public Transform gunTransform { get => transform; }
    public Elements Element { get => element; set
        {
            element = value;
            model.color = ElementalUtility.GetColor(value);
        }
    }

    private void Awake()
    {
        model = GetComponent<SpriteRenderer>();
        Element = Element;
        if(cam==null)
            cam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        originalOrthographicSize = 10;

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

    public void Shoot()
    {
        if (magazine > 0)
        {

            IElementalShootable bullet=Instantiate(bulletPrefab, muzzle.position, muzzle.rotation, null).GetComponent<IElementalShootable>();
            bullet.SwitchElement(Element);
            if(audio!=null)
                audio?.Play();
            onShot.Invoke();
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
