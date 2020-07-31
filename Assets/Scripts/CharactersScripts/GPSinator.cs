using UnityEngine;
using SAP2D;

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

    public float GetDistance(){
        return Vector3.Distance(target.position,self.position);
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

        if(path!=null)
            return path[currentWayPointIndex];
        else
            return new Vector2(self.position.x, self.position.y);
    }

    float GetDistanceToCurrentWayPointSquared()
    {
        if(path==null || path.Length==0)
            return Mathf.Infinity;
            
        return (new Vector2(self.position.x,self.position.y)-path[currentWayPointIndex]).sqrMagnitude;
    }

}