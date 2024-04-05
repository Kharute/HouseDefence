using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public float speed;

    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        Vector3 dirPos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, dirPos, speed);
    }
}
