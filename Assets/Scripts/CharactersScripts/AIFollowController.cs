using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SAP2D;

public class AIFollowController : MonoBehaviour, ICharacterInput
{
    [SerializeField] Transform target;
    [SerializeField] bool eatsCorpse=false;
    [SerializeField] SAP2DPathfindingConfig config;
    Transform body;
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    public float MouseXDirection { get; private set; }

    Vector2[] path;
    Vector2 wayPoint;
    Vector2 defaultDirection;
    Vector2 direction;
    GPSinator gps;

    float timer=0;
    private void Awake()
    {
        defaultDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        body = GetComponentInChildren<SpriteRenderer>().transform;
        if (config != null)
            gps = new GPSinator(body.transform,target,config);
    }
    public void SetMovementInputs()
    {
        if (target != null)
        {
            direction=GetDirectionToTarget(target.position);
        }
        else
        {
            if (eatsCorpse)
                direction = Vector2.zero;
            else
                direction = defaultDirection;
        }

        HorizontalInput = direction.x;
        VerticalInput = direction.y;
    }

    Vector2 GetDirectionToTarget(Vector3 targetPosition)
    {
        Vector2 direction;
        if (gps!=null && gps.isReady)
        {

            if (timer < Time.time)
            {
                gps.UpdatePath();
                timer = Time.time + 1;
            }

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

    void UpdatePath()
    {
        Vector2[] newPath = SAP2DPathfinder.singleton.FindPath(body.transform.position, target.position, config);
        if (newPath != null && newPath.Length > 0)
            path = newPath;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        if(gps!=null)
            gps.SetTarget(target);
    }
}

public class GPSinator
{
    Transform self;
    Transform target;
    SAP2DPathfindingConfig config;
    Vector2[] path;
    int currentWayPointIndex;
    float distanceSquared;
    public bool isReady=false;

    public GPSinator(Transform self, Transform target,SAP2DPathfindingConfig config)
    {
        this.config = config;
        this.self = self;
        SetTarget(target);
    }

    public void SetTarget(Transform target)
    {
        if (config==null || target == null || self== null)
        {
            isReady = false;
            return;
        }
        this.target = target;
        isReady = true;
        UpdatePath();
    }

    public void UpdatePath()
    {
        if (!isReady) return;
        Vector2[] newPath = SAP2DPathfinder.singleton.FindPath(self.position, target.position, config);
        if (newPath != null && newPath.Length > 0)
        {
            path = newPath;
            currentWayPointIndex = 0;
        }
    }

    public Vector2 GetDirection()
    {
        if (!isReady) return Vector2.zero;
        return GetWayPoint() - new Vector2(self.position.x, self.position.y);
    }

    public Vector2 GetWayPoint()
    {
        if (GetDistanceToCurrentWayPointSquared() < 0.1f)
        {
            if (currentWayPointIndex + 1 < path.Length)
                currentWayPointIndex++;
            else
                return new Vector2(self.position.x, self.position.y);
        }

        return path[currentWayPointIndex];
    }

    float GetDistanceToCurrentWayPointSquared()
    {
        return (new Vector2(self.position.x,self.position.y)-path[currentWayPointIndex]).sqrMagnitude;
    }

}