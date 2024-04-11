using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public HealthBar healthBar;
    
    public int towerMaxHP = 1000;
    public int towerHP;
    float timeSliceDamage;
    

    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        towerHP = towerMaxHP;
        healthBar.SetMaxHealth(towerHP);
        healthBar.SetHealth(towerMaxHP);
    }

    // Update is called once per frame
    void Update()
    {
        timeSliceDamage += Time.deltaTime;

        //타워 체력 매 순간 갱신
        healthBar.SetHealth(towerHP);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") && timeSliceDamage > 1.0f)
        {
            BrokeTower();
            timeSliceDamage = 0.0f;
        }
    }

    private void BrokeTower()
    {
        towerHP -= 50;
        if(towerHP < 1)
        {
            gameObject.SetActive(false);
        }
    }
}
