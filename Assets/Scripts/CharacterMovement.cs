using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed;

    SpriteRenderer bodySprite;
    ICharacterInput input;
    Vector3 movementDirection;

    private void Start()
    {
        input = GetComponent<ICharacterInput>();
        bodySprite = GetComponentInChildren<SpriteRenderer>();
        movementDirection.z = 0;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        input.SetInputs();
        movementDirection.x = input.HorizontalInput;
        movementDirection.y = input.VerticalInput;

        transform.position += movementDirection * Time.deltaTime * movementSpeed;
        if (input.MouseXPosition > transform.position.x)
            bodySprite.flipY=true;
        else
            bodySprite.flipY=false;
    }
}
