using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(ICharacterInput))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    Animator animator;
    SpriteRenderer bodySprite;
    ICharacterInput input;
    Rigidbody2D rigid;
    Vector3 movementDirection;
    bool canMove=true;

    readonly int walkBoolHash = Animator.StringToHash("Walking");

    private void Start()
    {
        input = GetComponent<ICharacterInput>();
        bodySprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid= GetComponent<Rigidbody2D>();
        movementDirection.z = 0;
    }

    private void LateUpdate()
    {
        if(canMove)
            rigid.velocity = movementDirection * movementSpeed;
    }

    private void Update()
    {
        if (canMove)
            TakeMoveInput();
        else
            movementDirection = Vector3.zero;

        Flip();
        if (movementDirection.x!= 0 || movementDirection.y != 0)
            animator.SetBool(walkBoolHash, true);
        else
            animator.SetBool(walkBoolHash, false);
    }

    void Flip()
    {
        if (input.MouseXDirection >= 0)
            bodySprite.flipX = false;
        else
            bodySprite.flipX = true;
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
        if(rigid!=null)
            rigid.velocity = Vector3.zero;
    }
    public void EnableMovement()
    {
        canMove= true;
    }
}
