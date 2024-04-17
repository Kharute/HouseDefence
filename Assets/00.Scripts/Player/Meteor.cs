using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D bc;
    Animator ani;
    Enemy[] enemy;
    SpawnTower sTower;

    public float time;
    public bool isleft;
    public float damage = 10;
    public float speed = 10;
    public bool isExplode = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        spriteRenderer.flipX = !isleft;
        time += Time.deltaTime;
        if(!isExplode)
        {
            transform.Translate(new Vector2(1 * (isleft ? -1 : 1), -1) * speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y+0.3f);
            isExplode = true;
            ani.SetTrigger("Explode");
            Invoke(nameof(Explode), 0.5f);
        }
    }

    //boxcollider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemy = collision.collider.GetComponents<Enemy>();
        sTower = collision.collider.GetComponent<SpawnTower>();

        if (enemy != null)
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                enemy[i].TakeDamage(damage);
            }
        }
        if(sTower != null)
        {
            sTower.BrokeTower(damage);
        }
    }
    void Explode()
    {
        gameObject.SetActive(false);
    }
    public void SetSkill(bool left)
    {
        isleft = left;
        isExplode = false;
        bc.enabled = false;
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
