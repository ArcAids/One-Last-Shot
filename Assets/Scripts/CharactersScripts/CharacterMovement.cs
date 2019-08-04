using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(ICharacterInput))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] bool AIControlled = false;
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
        if (canMove)
            rigid.velocity = movementDirection * movementSpeed;
        else
            rigid.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (canMove)
            Move();
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
        if (AIControlled)
        {
             if (input.HorizontalInput > 0)
                bodySprite.flipX = false;
            else
                bodySprite.flipX = true;
        }
        else
        {
            if (input.MouseXPosition > transform.position.x)
                bodySprite.flipX = false;
            else
                bodySprite.flipX = true;
        }
    }
    void Move()
    {
        input.SetInputs();
        movementDirection.x = input.HorizontalInput;
        movementDirection.y = input.VerticalInput;
    }

    public void DisableMovement()
    {
        canMove = false;
    }
    public void EnableMovement()
    {
        canMove = true;
    }
}
