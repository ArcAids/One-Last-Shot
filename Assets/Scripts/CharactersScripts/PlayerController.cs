using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacterInput, IWeaponInput, IDashInput, IElementControlInput
{
    [SerializeField] Camera cam;
    Vector3 mousePosition;
    public bool ActivateFireElement { get; private set; }
    public bool ActivateIceElement { get; private set; }
    public bool ActivateSlashElement { get; private set; }
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    public float MouseXPosition { get; private set; }

    public float MouseYPosition { get; private set; }

    public bool shooting { get; private set; }

    public bool Dash { get; private set; }

    bool canControl=true;
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
        {
            HorizontalInput = 0;
            VerticalInput = 0;
            return;
        }

        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        if(cam!=null)
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        MouseXPosition = mousePosition.x;
        MouseYPosition = mousePosition.y;
        shooting = Input.GetButton("Fire1");

        Dash = Input.GetButton("Fire2");

        ActivateFireElement = Input.GetKey(KeyCode.Alpha1);
        ActivateIceElement = Input.GetKey(KeyCode.Alpha2);
        ActivateSlashElement = Input.GetKey(KeyCode.Alpha3);
    }

    public void DisableControls()
    {
        canControl = false;
    }
    public void EnableControls()
    {

        canControl = true;
    }

}
