using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float timer = 120f;

    TextMeshProUGUI text;
    public GameObject[] panel;

    GameObject player;
    public GameObject House;
    public bool isEnd = false;
    // Start is called before the first frame update
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEnd)
        {
            timer -= Time.deltaTime;
            Time.timeScale = 1;

            if (timer > 0.1f)
            {
                text.text = null;
                text.text += ((int)timer / 60 % 60).ToString();
                text.text += " :" + ((int)timer % 60).ToString();
            }
            if (timer <= 0.1f)
            {
                isEnd = true;
                panel[0].gameObject.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 0;
            if (!player.activeInHierarchy || !House.activeInHierarchy)
            {
                panel[1].SetActive(true);
            }
            
        }
    }
}
