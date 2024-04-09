using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int towerHP = 1000;
    float timeSliceDamage;
    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSliceDamage += Time.deltaTime;
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
