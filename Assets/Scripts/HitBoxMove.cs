using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using static System.Net.WebRequestMethods;

public class HitBoxMove : MonoBehaviour
{
    SpriteRenderer hitBox;
    bool isLeft = false;
    float leftP = 1.0f;
    private void Awake()
    {
        hitBox = transform.parent.GetComponent<SpriteRenderer>();
        /*hitBox = parent*/

    }
    // Update is called once per frame
    void Update()
    {
        FlipChanged();
    }

    private void FlipChanged()
    {
        if (hitBox.flipX != isLeft)
        {
            leftP = hitBox.flipX ? -1 : 1;
            transform.position = new Vector2(transform.position.x +(2*leftP), transform.position.y);


            isLeft = hitBox.flipX;
        }
    }
}
