using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacterInput, IWeaponInput, IDashInput, IElementControlInput
{
    [SerializeField] Camera cam;
    public bool ActivateFireElement { get; private set; }
    public bool ActivateIceElement { get; private set; }
    public bool ActivateSlashElement { get; private set; }
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public float MouseXPosition { get; private set; }
    public float MouseYPosition { get; private set; }
    public bool Shooting { get; private set; }
    public bool Dash { get; private set; }

    bool canControl=true;
    Vector3 mousePosition;
    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
                cam = FindObjectOfType<Camera>();
        }
        canControl = true;
    }
     
    public void SetInputs()
    {
        if (!canControl)
            return;


        ActivateFireElement = Input.GetKey(KeyCode.Alpha1);
        ActivateIceElement = Input.GetKey(KeyCode.Alpha2);
        ActivateSlashElement = Input.GetKey(KeyCode.Alpha3);
    }
     public void SetWeaponInputs()
    {
        if (!canControl)
            return;

        UpdateMousePosition();
        MouseXPosition = mousePosition.x;
        MouseYPosition = mousePosition.y;
        Shooting = Input.GetButton("Fire1");
    }

    public void SetMovementInputs()
    {
        if (!canControl)
            return;
        UpdateMousePosition();
        Dash = Input.GetButton("Fire2");
        MouseXPosition = mousePosition.x;
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
    }

    void UpdateMousePosition()
    {
        if (cam != null)
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void DisableControls()
    {
        canControl = false;
        HorizontalInput = 0;
        VerticalInput = 0;
        MouseXPosition = 0;
        MouseYPosition= 0;
        Dash = false;
        ActivateFireElement = false;
        ActivateIceElement =   false;
        ActivateSlashElement = false;

    }
    public void EnableControls()
    {
        canControl = true;
    }

}
