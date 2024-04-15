using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    public bool isHere = false;
    public float HealPoint = 0.1f;
    public float regenHPTime = 0.1f;
    Player player1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player1 = collision.GetComponent<Player>();
            isHere = true;
            StartCoroutine(Heal());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isHere = false;
        StopCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        while (isHere)
        {
            if (player1 != null)
            {
                player1.Heal(HealPoint);
                yield return new WaitForSeconds(regenHPTime);
            }
        }
        
        
    }


}
