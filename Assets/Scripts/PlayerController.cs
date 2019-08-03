using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacterInput, IWeaponInput, IDashInput
{
    [SerializeField] Camera cam;
    Vector3 mousePosition;
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    public float MouseXPosition { get; private set; }

    public float MouseYPosition { get; private set; }

    public bool shooting { get; private set; }

    public bool Dash { get; private set; }

    private void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null)
                cam = FindObjectOfType<Camera>();
        }
    }
     
    public void SetInputs()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        VerticalInput = Input.GetAxis("Vertical");
        if(cam!=null)
            mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        MouseXPosition = mousePosition.x;
        MouseYPosition = mousePosition.y;
        shooting = Input.GetButton("Fire1");

        Dash = Input.GetButton("Fire2");
    }


}
