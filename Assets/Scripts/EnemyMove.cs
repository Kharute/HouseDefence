using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    private int posX = -1;
    private Rigidbody2D rb;

    private void Awake()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    /*void Update()
    {
        rb.velocity = new Vector2(posX, rb.position.y);

    }*/



    /*private void FixedUpdate()
    {
        //Vector2 frontVec = new Vector2(rb.position.x + posX, rb.position.y);

        //Debug.DrawRay(frontVec, Vector2.down, new Color(1, 0, 0));
        RaycastHit2D hits = Physics2D.Raycast(rb.position, Vector2.down, LayerMask.GetMask("Flatform"));
    }*/
}
