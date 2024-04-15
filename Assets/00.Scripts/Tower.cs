using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public HealthBar healthBar;
    public HealthBar healthBarUI;
    public float towerMaxHP = 1000;
    public float towerHP;
    float timeSliceDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        towerHP = towerMaxHP;
        healthBar.SetMaxHealth(towerHP);
        healthBar.SetHealth(towerMaxHP);
        healthBarUI.SetMaxHealth(towerHP);
        healthBarUI.SetHealth(towerMaxHP);
    }

    // Update is called once per frame
    void Update()
    {
        timeSliceDamage += Time.deltaTime;

        //타워 체력 매 순간 갱신
        healthBar.SetHealth(towerHP);
        healthBarUI.SetHealth(towerHP);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && timeSliceDamage > 1.0f)
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            BrokeTower(enemy.enemyAtk);
            timeSliceDamage = 0.0f;
        }
    }

    private void BrokeTower(float atk)
    {
        towerHP -= atk;
        if(towerHP < 1)
        {
            gameObject.SetActive(false);
        }
    }
}
