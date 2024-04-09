using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator ani;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public int HP = 3;
    public float enemyAtk = 50;
    public float knockBackPower = 200.0f;
    public bool isLive = true;
    public bool isRight;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        isRight = sr.flipX;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;

        if(HP <= 0)
        {
            isLive = false;
            rb.velocity = Vector2.zero;
            ani.SetTrigger("Death");
            Invoke(nameof(EnemyDeath), 0.5f);
        }
        else
        {
            rb.AddForce(new Vector2(knockBackPower * (isRight? -1 : 1), knockBackPower));
        }
    }
    private void EnemyDeath()
    {
        gameObject.SetActive(false);
    }
}
