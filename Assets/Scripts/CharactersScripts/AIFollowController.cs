using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollowController : MonoBehaviour, ICharacterInput
{
    [SerializeField] Transform target;
    [SerializeField] bool eatsCorpse=false;
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    public float MouseXDirection { get; private set; }


    Vector2 defaultDirection;
    Vector2 direction;
    private void Awake()
    {
        defaultDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    }
    public void SetMovementInputs()
    {
        if (target != null)
        {
            direction = target.position - transform.position;
            MouseXDirection = target.position.x - transform.position.x;
        }
        else
        {
            if (eatsCorpse)
                direction = Vector2.zero;
            else
                direction = defaultDirection;
        }
        direction=direction.normalized;
        
        HorizontalInput = direction.x;
        VerticalInput = direction.y;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
