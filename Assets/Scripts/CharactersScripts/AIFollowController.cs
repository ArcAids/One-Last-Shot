using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollowController : MonoBehaviour, ICharacterInput
{
    [SerializeField] Transform target;
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    public float MouseXPosition { get; private set; }


    Vector2 defaultDirection;
    Vector2 direction;
    private void Awake()
    {
        defaultDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    }
    public void SetInputs()
    {
        if (target != null)
            direction = target.position - transform.position;
        else
            direction = defaultDirection;

        direction=direction.normalized;
        
        HorizontalInput = direction.x;
        VerticalInput = direction.y;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
