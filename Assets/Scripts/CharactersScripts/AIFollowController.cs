using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollowController : MonoBehaviour, ICharacterInput
{
    [SerializeField] Transform target;
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    public float MouseXPosition { get; private set; }

    Vector2 direction;
    public void SetInputs()
    {
        direction = target.position - transform.position;
        direction=direction.normalized;
        
        HorizontalInput = direction.x;
        VerticalInput = direction.y;
    }
}
