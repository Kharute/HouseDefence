using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    public HealthBar healthBar;

    public float towerMaxHP = 1000;
    public float towerHP;
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
        healthBar.SetHealth(towerHP);
    }

    public void BrokeTower(float damage)
    {
        towerHP -= damage;
        if (towerHP < 1)
        {
            gameObject.SetActive(false);
        }
    }
}
