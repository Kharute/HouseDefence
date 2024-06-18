using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenser : MonoBehaviour
{
    EnemyAI_Ground enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyAI_Ground>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            enemy.isChasing = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            enemy.isChasing = false;
    }
}
