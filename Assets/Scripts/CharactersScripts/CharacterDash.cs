using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(CharacterController))]
public class CharacterDash : MonoBehaviour
{

    [SerializeField] float dashSpeed=5;
    [SerializeField] float dashDuration=1;
    [SerializeField] float dashCoolDown=1;
    [SerializeField] bool invulnerableWhileDashing=true;
    [Space]
    [SerializeField] Image rechargeCircle;
    [Space]
    [SerializeField] TrailRenderer trail;
    Rigidbody2D rigidBody;
    new Collider2D collider;
    IDashInput input;
    CharacterMovement movement;
    Vector3 movementDirection;

    bool canDash=true;
    float dashTime;
    float dashRecharge;
    bool dashing=false;

    public bool Dashing { get => dashing; set
        {
            dashing = value;
            if (dashing)
                OnDashStart();
            else
                OnDashEnd();
        } }

    public float DashRecharge { get => dashRecharge; private set
        {
            dashRecharge = value;
            rechargeCircle.fillAmount = 1-(value / dashCoolDown);
        }
    }

    private void Awake()
    {
        input = GetComponent<IDashInput>();
        movement = GetComponent<CharacterMovement>();
        rigidBody = GetComponent<Rigidbody2D>();
        
        collider = GetComponentInChildren<Collider2D>();
        if (trail==null)
            trail=GetComponentInChildren<TrailRenderer>();
        movementDirection.z = 0;
        DashRecharge = 0;
        dashing = false;
    }

    private void Update()
    {
        input.SetDashInputs();
        if (input.Dash && canDash )
            Dash();

        if(Dashing)
        {
            dashTime += Time.deltaTime;
            if (dashTime > dashDuration)
            {
                Dashing = false;
                DashRecharge= dashCoolDown;
                dashTime = 0;
                canDash = false;
            }
        }
        else if(!canDash)
        {
            DashRecharge -= Time.deltaTime;
            if (DashRecharge<=0)
            {
                canDash = true;
                rechargeCircle.color = Color.white;
            }
        }

    }

    public void Dash()
    {
        if(!Dashing)
        {
            movementDirection.x = input.HorizontalInput;
            movementDirection.y = input.VerticalInput;
            if(movementDirection.x !=0 || movementDirection.y!= 0)
                Dashing = true;
        }       

    }

    void OnDashEnd()
    {
        movement?.EnableMovement();
        trail.enabled = false;
        collider.enabled = true;
    }

    void OnDashStart()
    {
        trail.Clear();
        trail.enabled = true;
        movement?.DisableMovement(movementDirection.normalized * dashSpeed);
        rechargeCircle.color = Color.grey;
        rigidBody.velocity = movementDirection.normalized * dashSpeed;
        if(invulnerableWhileDashing)
            collider.enabled = false;
    }

}
