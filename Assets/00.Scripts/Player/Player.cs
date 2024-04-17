using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;

    public float currentHP;
    public float MaxHP = 200;
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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                TakeDamage(enemy.enemyAtk);
            }
        }
        if (collision.collider.tag == "Item")
        {
            var item = collision.collider.GetComponent<Item>();
            if (item)
            {
                inventory.AddItem(item.item, 1);
                collision.gameObject.SetActive(false);
            }
        }
    }

    private void TakeDamage(float damage)
    {
        currentHP -= damage;
    }
    public void Heal(float heal)
    {
        if(currentHP < MaxHP)
            currentHP += heal;
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

}

