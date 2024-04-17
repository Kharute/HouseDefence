using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    Queue<GameObject> m_Queue = new Queue<GameObject>();

    public GameObject[] prefabs;
    public float spawnTime = 10.0f;
    public int m_Count = 10;

    private void Start()
    {
        for (int i = 0; i < m_Count; i++)
        {
            int e_rand = Random.Range(0, prefabs.Length);
            GameObject m_object = Instantiate(prefabs[e_rand]);
            m_object.transform.position = transform.position;

            m_object.SetActive(false);
            m_Queue.Enqueue(m_object);
        }
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        while (m_Queue.Count > 0)
        {
            GameObject Enemy = m_Queue.Dequeue();
            Enemy.SetActive(true);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
