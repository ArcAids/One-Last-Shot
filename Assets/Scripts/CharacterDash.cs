using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterDash : MonoBehaviour
{
    [SerializeField] float dashSpeed=5;
    [SerializeField] float dashDuration=1;
    [SerializeField] float dashCoolDown=1;
    [SerializeField] bool invulnerableWhileDashing=true;
    IDashInput input;
    CharacterMovement movement;
    Vector3 movementDirection;

    bool canDash=true;
    float dashTime;
    bool dashing=false;

    public bool Dashing { get => dashing; set
        {
            dashing = value;
            if (dashing)
                OnDashStart();
            else
                OnDashEnd();
        } }

    private void Awake()
    {
        input = GetComponent<IDashInput>();
        movement = GetComponent<CharacterMovement>();
        movementDirection.z = 0;
        dashing = false;
    }

    private void Update()
    {
        input.SetInputs();
        if (input.Dash && canDash)
            Dash();

        if(Dashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime > dashDuration)
            {
                Dashing = false;
                dashTime = dashCoolDown;
                canDash = false;
            }
            transform.position += movementDirection * Time.deltaTime * dashSpeed;
        }
        else if(!canDash)
        {
            dashTime -= Time.deltaTime;
            if (dashTime<=0)
            {
                canDash = true;
            }
        }

    }

    public void Dash()
    {
        if(!Dashing)
        {
            movementDirection.x = input.HorizontalInput;
            movementDirection.y = input.VerticalInput;
            Dashing = true;
        }       

    }

    void OnDashEnd()
    {
        movement.EnableMovement();
    }

    void OnDashStart()
    {
        movement.DisableMovement();
    }

}
