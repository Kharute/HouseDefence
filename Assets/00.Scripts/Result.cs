using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] star;

    // Update is called once per frame
    void Update()
    {
        ClearCheck();
    }

    void ClearCheck()
    {
        for (int i = 0; i < star.Length; i++)
        {
            star[i].SetActive(IOManager.gameData.StageDATA[IOManager.Inst.curStage][i]);
        }
    }
}
