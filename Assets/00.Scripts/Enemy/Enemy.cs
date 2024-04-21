using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    List<GameObject> m_List = new List<GameObject>();
    public HealthBar healthBar;
    public GameObject[] prefabs;

    Animator ani;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public float currentHP;
    public float MaxHP = 10;
    public float enemyAtk = 50;
    public float knockBackPower = 150.0f;
    public bool isLive = true;
    public bool isRight;
    private void Start()
    {
        currentHP = MaxHP;
        healthBar.SetMaxHealth(MaxHP);
        healthBar.SetHealth(currentHP);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentHP);
    }
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
        Debug.Log(dropCount+"°³ µå·Ó");
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if(currentHP <= 0)
        {
            isLive = false;
            rb.velocity = Vector2.zero;
            ani.SetTrigger("Death");
            Invoke(nameof(EnemyDeath), 0.2f);
        }
        else
        {
            //³Ë¹éÀÎµ¥ ÇöÀç ¾È¸ÔÈû
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
