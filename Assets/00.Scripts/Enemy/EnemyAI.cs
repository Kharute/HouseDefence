using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    public List<GameObject> AllObjects;
    public GameObject nearestObj;
    public CircleCollider2D circleCollider;
    public float distance;
    public float nearestDistance = 10000;
    public float nextWaypointDistance = 3f;

    public Path path;
    public int currentWaypoint = 0;
    public bool reachedEndOfPath = false;

    public Seeker seeker;
    public Rigidbody2D rb;
    public float speed = 300f;

    protected void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (nearestObj.activeInHierarchy == false)
            {
                AllObjects.Remove(nearestObj);
            }
            SetTarget();
            seeker.StartPath(rb.position, nearestObj.transform.position, OnPathComplete);
        }
    }

    protected void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    protected void LoadTarget()
    {
        AllObjects.AddRange(GameObject.FindGameObjectsWithTag("Structure"));
        AllObjects.Add(GameObject.FindWithTag("Player"));
    }

    protected void SetTarget()
    {
        nearestDistance = 10000;
        for (int i = 0; i < AllObjects.Count; i++)
        {
            distance = Vector2.Distance(this.transform.position, AllObjects[i].transform.position);
            if (distance < nearestDistance)
            {
                nearestObj = AllObjects[i];
                nearestDistance = distance;
            }
        }
    }
}
