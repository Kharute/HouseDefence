using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public float time;
    public bool isleft;
    public float damage = 10;
    public float speed = 3f;
    
    void Update()
    {
        time += Time.deltaTime;
        transform.Translate(Vector2.right * (isleft? -0.01f : 0.01f) * speed);
        if (time > 3)
        {
            time = 0;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            gameObject.SetActive(false);
        }
        
        else if (collision.gameObject.layer == 12)
        {
            SpawnTower tower= collision.GetComponent<SpawnTower>();
            tower.BrokeTower(damage);
            gameObject.SetActive(false);
        }
    }
}
