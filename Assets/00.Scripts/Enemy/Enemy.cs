using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    List<GameObject> m_List = new List<GameObject>();
    public GameObject[] prefabs;

    Animator ani;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float HP = 3;
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
        prefabs = Resources.LoadAll<GameObject>("01.Prefabs/Items/Field");
        int dropCount = Random.Range(1, 3);

        for(int i = 0; i < dropCount; i++)
        {
            int randomDrop = Random.Range(0, prefabs.Length);
            GameObject m_object = Instantiate(prefabs[randomDrop]);
            
            m_object.SetActive(false);
            m_List.Add(m_object);
        }
        Debug.Log(dropCount+"�� ���");

    }

    public void TakeDamage(float damage)
    {
        HP -= damage;

        if(HP <= 0)
        {
            isLive = false;
            rb.velocity = Vector2.zero;
            ani.SetTrigger("Death");
            Invoke(nameof(EnemyDeath), 0.3f);
        }
        else
        {
            //�˹��ε� ���� �ȸ���
            rb.AddForce(new Vector2(knockBackPower * (isRight? -1 : 1), knockBackPower));
        }
    }
    private void EnemyDeath()
    {
        foreach(var i in m_List)
        {
            i.transform.position = transform.position;
            i.SetActive(true);
        }
        
        gameObject.SetActive(false);
    }
}
