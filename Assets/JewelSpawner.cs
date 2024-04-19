using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JewelSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabs;

    [SerializeField]
    TextMeshProUGUI textMeshProUGUI;

    [SerializeField]
    Transform[] transforms;

    List<GameObject> jewel = new List<GameObject>();
    
    int jewelCount = 3;
    //3개 만들어주고
    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < jewelCount; i++)
        {
            GameObject m_object = Instantiate(prefabs);
            m_object.transform.position = transforms[i].position;

            jewel.Add(m_object);
        }
    }
    private void Update()
    {
        int count = 0;
        for(int i = 0;i < jewelCount; i++)
        {
            if (!jewel[i].activeInHierarchy)
            {
                count++;
            }
        }
        textMeshProUGUI.text = $": {count} / {jewelCount}";
    }
}
