using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float timer = 120f;

    TextMeshProUGUI text;
    public GameObject[] panel;

    public bool isEnd = false;
    // Start is called before the first frame update
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEnd)
        {
            timer -= Time.deltaTime;

            if (timer > 0.1f)
            {
                text.text = null;
                text.text += ((int)timer / 60 % 60).ToString();
                text.text += " :" + ((int)timer % 60).ToString();
            }
            if (timer <= 0.1f)
            {
                panel[0].gameObject.SetActive(true);
            }
        }
        else
        {
            panel[1].gameObject.SetActive(true);
        }
    }
}
