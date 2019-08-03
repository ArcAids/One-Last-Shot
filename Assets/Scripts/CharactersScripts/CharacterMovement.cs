using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    SpriteRenderer bodySprite;
    ICharacterInput input;
    Vector3 movementDirection;

    bool canMove=true;
    private void Start()
    {
        input = GetComponent<ICharacterInput>();
        bodySprite = GetComponentInChildren<SpriteRenderer>();
        movementDirection.z = 0;
    }

    private void Update()
    {
        if(canMove)
            Move();
        Flip();
    }

    void Flip()
    {
        if (input.MouseXPosition > transform.position.x)
            bodySprite.flipY = true;
        else
            bodySprite.flipY = false;
    }
    void Move()
    {
        input.SetInputs();
        movementDirection.x = input.HorizontalInput;
        movementDirection.y = input.VerticalInput;

        transform.position += movementDirection * Time.deltaTime * movementSpeed;
       
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
