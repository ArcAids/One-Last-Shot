using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour, ICharacterInput, IWeaponInput, IDashInput, IElementControlInput
{
    [SerializeField] Camera cam;
    public bool ActivateFireElement { get; private set; }
    public bool ActivateIceElement { get; private set; }
    public bool ActivateSlashElement { get; private set; }
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public float MouseXDirection { get; private set; }
    public float MouseYDirection { get; private set; }
    public bool Shooting { get; private set; }
    public bool Dash { get; private set; }

    bool canControl=true;

    public bool DashUIButton { get; set; }
    public bool FireUIButton { get; set; }
    public bool IceUIButton  { get; set; }
    public bool MagicUIButton { get; set; }
    Vector3 mouseDirection;
    Elements currentElement=Elements.Fire;
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

        ActivateFireElement = false;
        ActivateIceElement = false;
        ActivateSlashElement = false;

        if (Input.GetKeyDown(KeyCode.Alpha1) || FireUIButton)
            currentElement = Elements.Fire;
        else if (Input.GetKeyDown(KeyCode.Alpha2) || IceUIButton)
            currentElement = Elements.Ice;
        else if (Input.GetKeyDown(KeyCode.Alpha3) || MagicUIButton)
            currentElement = Elements.Slash;
        else if (Input.mouseScrollDelta.y<0)
            SelectNextElement();
        else if (Input.mouseScrollDelta.y > 0)
            SelectPreviousElement();

        switch (currentElement)
        {
            case Elements.Fire:
                ActivateFireElement = true;
                break;
            case Elements.Ice:
                ActivateIceElement = true;
                break;
            case Elements.Slash:
                ActivateSlashElement = true;
                break;
            default:
                break;
        }

        FireUIButton = false;
        IceUIButton = false;
        MagicUIButton = false;
    }

    public void SelectNextElement()
    {
        //Debug.Log("Up"+Input.mouseScrollDelta.y);
        if ((int)currentElement == 2)
            currentElement = 0;
        else
            currentElement++;
    }
    public void SelectPreviousElement()
    {
        //Debug.Log("down"+Input.mouseScrollDelta.y);
        if ((int)currentElement == 0)
            currentElement = (Elements)2;
        else
            currentElement--;
    }


     public void SetWeaponInputs()
    {
        if (!canControl)
            return;

        UpdateMousePosition();
        MouseXDirection = mouseDirection.x;
        MouseYDirection = mouseDirection.y;
#if UNITY_ANDROID
        Shooting = CrossPlatformInputManager.GetButton("Fire");
#else
//#endif
//#if UNITY_STANDALONE || UNITY_EDITOR 
        Shooting = Input.GetButton("Fire1");
#endif

    }

    public void SetMovementInputs()
    {
        if (!canControl)
            return;
        UpdateMousePosition();
        MouseXDirection = mouseDirection.x;
#if UNITY_ANDROID
        HorizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        VerticalInput = CrossPlatformInputManager.GetAxis("Vertical");
#else
        //#endif
        //#if UNITY_STANDALONE || UNITY_EDITOR 
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
#endif
    }
    public void SetDashInputs()
    {
        if (!canControl)
            return;
        SetMovementInputs();
#if UNITY_ANDROID
        Dash = DashUIButton;
#else
        //#endif
        //#if UNITY_STANDALONE || UNITY_EDITOR 
        Dash = Input.GetButtonDown("Fire2") || DashUIButton;
#endif

        DashUIButton = false;
    }

    void UpdateMousePosition()
    {

        float mouseX = CrossPlatformInputManager.GetAxis("Horizontal");
        float mouseY = CrossPlatformInputManager.GetAxis("Vertical");
#if UNITY_ANDROID
        mouseDirection = new Vector2(mouseX==0?mouseDirection.x: mouseX,mouseY==0?mouseDirection.y: mouseY);
#else
        //#endif
        //#if UNITY_STANDALONE || UNITY_EDITOR 
        if (cam != null)
            mouseDirection = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
#endif
        mouseDirection = mouseDirection.normalized;
    }

    public void DisableControls()
    {
        canControl = false;
        HorizontalInput = 0;
        VerticalInput = 0;
        MouseXDirection = 0;
        MouseYDirection= 0;
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
