using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP = 3;

    private void Update()
    {
        if(HP < 1)
        {
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(int damage)
    {
        HP = HP - damage;
    }

    
}
