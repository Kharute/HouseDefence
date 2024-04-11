using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int currentHP;
    public int MaxHP = 200;
    public HealthBar healthBar;

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
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }*/
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
            TakeDamage(20);
    }

    private void TakeDamage(int damage)
    {
        currentHP -= damage;
    }

    
}

