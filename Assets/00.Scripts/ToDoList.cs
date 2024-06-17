using UnityEngine;


public class ToDoList : MonoBehaviour
{
    public GameObject[] enemySpawn;
    public GameObject[] star;
    public GameTimer timer;

    // Update is called once per frame
    void Update()
    {
        /*if (!IOManager.Instance.playerData.stageCleard[IOManager.Instance.curStage][0])
        {
            if (timer.timer < 0.1f)
                IOManager.Instance.playerData.stageCleard[IOManager.Instance.curStage][0] = true;
        }
        if (!IOManager.Instance.playerData.stageCleard[IOManager.Instance.curStage][1])
        {
            if (!(enemySpawn[0].gameObject.activeInHierarchy || enemySpawn[1].gameObject.activeInHierarchy))
                IOManager.Instance.playerData.stageCleard[IOManager.Instance.curStage][1] = true;
        }
        if (!IOManager.Instance.playerData.stageCleard[IOManager.Instance.curStage][2])
        {
            if (GameObject.Find("Jewel(Clone)") == null)
                IOManager.Instance.playerData.stageCleard[IOManager.Instance.curStage][2] = true;

            ClearCheck();
        }*/
        void ClearCheck()
        {
            for (int i = 0; i < star.Length; i++)
            {
                star[i].SetActive(IOManager.gameData.StageDATA[IOManager.Inst.curStage][i]);
            }
        }
    }
}
