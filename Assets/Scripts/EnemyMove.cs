using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator ani;
    private Enemy enemy;

    public float goMoveSecond = 2.0f;
    public float stopMoveSecond = 1.0f;
    public float moveSpeed = 10.0f;
    private int posX = -1;
    private bool isMove = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        StartCoroutine(MoveRoitains());
    }

    // Update is called once per frame
    void Update()
    {
        float moveInt = isMove ? -0.5f : 0;
        if (isMove)
        {
            rb.velocity = new Vector2(rb.velocity.x + moveInt * moveSpeed * Time.deltaTime, rb.velocity.y);
        }
    }
    IEnumerator MoveRoitains()
    {
        float originalGravity = rb.gravityScale;

        while (enemy.isLive)
        {
            isMove = false;
            ani.SetBool("Move", isMove);
            yield return new WaitForSeconds(stopMoveSecond);
            rb.velocity = Vector2.zero;

            isMove = true;
            ani.SetBool("Move", isMove);
            yield return new WaitForSeconds(goMoveSecond);
        }
    }
    private void FixedUpdate()
    {
        Vector2 frontVec = new Vector2(rb.position.x + posX, rb.position.y);

        Debug.DrawRay(frontVec, Vector2.down, new Color(1, 0, 0));
        RaycastHit2D hits = Physics2D.Raycast(rb.position, Vector2.down, LayerMask.GetMask("Platform"));

        posX = hits.rigidbody == null ? 1 : -1;
    }

    // 부딪히고 나서 안밀리도록 조정
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
            rb.velocity = Vector2.zero;
    }
}
