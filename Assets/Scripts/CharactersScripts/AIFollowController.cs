using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SAP2D;

public class AIFollowController : MonoBehaviour, ICharacterInput
{
    [SerializeField] protected Transform target;
    [SerializeField] protected bool eatsCorpse=false;
    [SerializeField] SAP2DPathfindingConfig config;
    [SerializeField] protected float targetDistance=0.1f;

    Transform body;
    public float HorizontalInput { get; protected set; }
    public float VerticalInput { get; protected set; }

    public float MouseXDirection { get; protected set; }

    protected Vector2 defaultDirection;
    protected Vector2 direction;
    protected GPSinator gps;
    protected bool canMove=false;
    protected float timer=0;
    private void Awake()
    {
        defaultDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        body = GetComponentInChildren<SpriteRenderer>().transform;
        Invoke("StartChasing", 1);
    }

    void StartChasing()
    {
        canMove = true;
        if (config != null)
            gps = new GPSinator(body.transform, target, config);
        GetComponent<AIAttack>()?.EnableAttack();
    }

    public virtual void SetMovementInputs()
    {
        if(!canMove)
        {
            HorizontalInput = 0;
            VerticalInput = 0;
            return;
        }
        if (target != null)
        {
            float distance=UpdatePath();
            if (distance > targetDistance)
                direction = GetDirectionToTarget(target.position);
            else direction = Vector2.zero;
        }
        else
        if (!eatsCorpse)
            direction = defaultDirection;        

        HorizontalInput = direction.x;
        VerticalInput = direction.y;
    }

    protected float UpdatePath()
    {
        if (gps == null || !gps.isReady)
            return 0;

        float distance = gps.GetDistance();
        if (timer < Time.time)
        {
            gps.UpdatePath();
            float waitTime = distance * 0.1f;
            waitTime = Mathf.Clamp(waitTime, 0.1f, 5f);
            timer = Time.time + waitTime;
        }
        return distance;

    }
    protected Vector2 GetDirectionToTarget(Vector3 targetPosition)
    {
        Vector2 direction;
        if (gps!=null && gps.isReady)
        {
            direction = gps.GetDirection().normalized;
            MouseXDirection = direction.normalized.x;
        }
        else
        {
            direction = targetPosition - body.transform.position;
            direction = direction.normalized;
            MouseXDirection = targetPosition.x - transform.position.x;
        }
        return direction;
    }

    public void SetTarget(Transform target)
    {
        if (target == null && eatsCorpse)
            return;
        
        this.target = target;
        if(gps!=null)
            gps.SetTarget(target);
    }
}
