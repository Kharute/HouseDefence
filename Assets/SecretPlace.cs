using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretPlace : MonoBehaviour
{
    TilemapRenderer tilemapRenderer;
    private void Awake()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(tilemapRenderer.enabled)
        {
            tilemapRenderer.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!tilemapRenderer.enabled)
        {
            tilemapRenderer.enabled = true;
        }
    }
}
