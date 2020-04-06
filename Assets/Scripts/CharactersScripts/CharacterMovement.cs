using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(ICharacterInput))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float baseMovementDampening=0.2f;
    Animator animator;
    SpriteRenderer bodySprite;
    ICharacterInput input;
    Rigidbody2D rigid;
    Vector3 movementDirection;
    Vector3 targetVelocity;
    bool canMove=true;

    float movementDampening;
    readonly int walkBoolHash = Animator.StringToHash("Walking");

    private void Start()
    {
        input = GetComponent<ICharacterInput>();
        bodySprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid= GetComponent<Rigidbody2D>();
        movementDirection.z = 0;
        movementDampening = baseMovementDampening;
    }

    

    private void FixedUpdate()
    {
        if(canMove)
            targetVelocity = movementDirection * movementSpeed;
        if(movementDirection==Vector3.zero)
        {
            targetVelocity.x =  rigid.velocity.x * Mathf.Pow(movementDampening, Time.deltaTime *10f);
            targetVelocity.y =  rigid.velocity.y * Mathf.Pow(movementDampening, Time.deltaTime *10f);
        }

        rigid.velocity = targetVelocity;

    }

    private void Update()
    {
        if (canMove)
            TakeMoveInput();
        else
            movementDirection = Vector3.zero;

        Flip();
        if(animator)
            SetAnimations();
    }

    void Flip()
    {
        if (input.MouseXDirection >= 0)
            bodySprite.flipX = false;
        else
            bodySprite.flipX = true;
    }
    void SetAnimations()
    {
        if (movementDirection.x != 0 || movementDirection.y != 0)
            animator?.SetBool(walkBoolHash, true);
        else
            animator?.SetBool(walkBoolHash, false);
    }
    void TakeMoveInput()
    {
        input.SetMovementInputs();
        movementDirection.x = input.HorizontalInput;
        movementDirection.y = input.VerticalInput;
        movementDirection=movementDirection.normalized;
    }

    public void AddSpeed(float value)
    {
        movementSpeed +=value;
    }

    public void DisableMovement()
    {
        canMove= false;
        targetVelocity = Vector3.zero;
    }
    public void DisableMovement(Vector2 targetVelocity)
    {
        DisableMovement();
        movementDampening = 1;
        this.targetVelocity = targetVelocity;
        rigid.velocity = targetVelocity;
    }
    public void EnableMovement()
    {
        canMove= true;
        movementDampening = baseMovementDampening;
    }
}
