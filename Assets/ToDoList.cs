using UnityEngine;


public class ToDoList : MonoBehaviour
{
    public bool[] clear;
    public GameObject[] enemySpawn;
    public GameObject[] star;
    public GameTimer timer;


    private void Awake()
    {
        clear = new bool[3];
        for (int i = 0; i < clear.Length; i++)
            clear[i] = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(timer.timer < 0.1f)
            clear[0] = true;

        if (!(enemySpawn[0].gameObject.activeInHierarchy || enemySpawn[1].gameObject.activeInHierarchy))
        {
            clear[1] = true;
        }
        if (GameObject.Find("Jewel(Clone)") == null)
        {
            clear[2] = true;
        }
        ClearCheck();
    }
    void ClearCheck()
    {
        for (int i = 0; i < star.Length; i++)
        {
            star[i].SetActive(clear[i]);
        }
    }
}
