using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour
{
    public List<GameObject> AllObjects;
    public GameObject nearestObj;
    CircleCollider2D circleCollider;
    float distance;
    float nearestDistance = 10000;

    public float speed = 300f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();


        //건물, 플레이어 정보를 받아 가장 가까운 것부터 타겟해주는 것
        LoadTarget();
        SetTarget();

        //0.5초마다 계속 타겟을 Chase해주는 Invoke
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        InvokeRepeating("ResetAddForce", 0.5f, 1f);
    }

    void ResetAddForce()
    {
        rb.velocity = Vector2.zero;
    }
    void UpdatePath()
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

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }else
        {
            reachedEndOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //transform.Translate(force * 0.1f);
        

        rb.AddForce(force);
        

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void LoadTarget()
    {
        AllObjects.AddRange(GameObject.FindGameObjectsWithTag("Structure"));
        AllObjects.Add(GameObject.FindWithTag("Player"));
    }

    void SetTarget()
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
