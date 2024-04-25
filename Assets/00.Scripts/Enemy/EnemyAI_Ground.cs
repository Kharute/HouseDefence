using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;

public class EnemyAI_Ground : EnemyAI
{
    Enemy enemy;
    SpriteRenderer sr;

    public float jumpForce = 300f;
    public float checkDistance = 3f;

    bool isGround;
    public bool isChasing;
    public Vector2 force;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        enemy = GetComponentInParent<Enemy>();
        sr = GetComponent<SpriteRenderer>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        //건물, 플레이어 정보를 받아 가장 가까운 것부터 타겟해주는 것
        LoadTarget();
        SetTarget();

        //0.5초마다 계속 타겟을 Chase해주는 Invoke
        InvokeRepeating("UpdatePath", 0f, 0.5f);
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
        Vector2 vectorLeft = direction.x < 0 ? Vector2.left : Vector2.right;

        force = vectorLeft * speed * Time.deltaTime;

        //transform.Translate(force * 0.1f);

        //벽에 막혔다면 점프해줘야함.
        Vector2 chkFront = Vector2.left * (enemy.isRight ? -1 : 1);
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, chkFront, checkDistance);
        Debug.DrawRay(transform.position, chkFront*checkDistance, Color.blue);
        if(hit != null && isGround)
        {
            for(int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.CompareTag("Player") || hit[i].collider.CompareTag("Platform"))
                {
                    rb.AddForce(Vector2.up * jumpForce);
                }
            }
        }
        
        rb.AddForce(force);
        
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (rb.velocity.x >= 0.01f)
        {
            enemy.isRight = true;
            sr.flipX = true;
        }
        else if (rb.velocity.x <= -0.01f)
        {
            enemy.isRight = false;
            sr.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Platform"))
            isGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
            isGround = false;
    }

}
