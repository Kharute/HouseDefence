using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    // load ��, .json���� Ư�� �� ȣ��
    // skillContext �ٿ��ߵǰŵ�.

    public TextMeshProUGUI skillContext;
    public Canvas[] canvas;

    public int stage = -1;

    
    public void StartStageButtonClick(int stage)
    {
        //IOManager iOManager = IOManager.LoadPlayerDataFromJson();
        IOManager.Inst.curStage = stage;
        switch (stage)
        {
            case 0:
                SceneManager.LoadScene("Stage1");
                break;
            case 1:
                SceneManager.LoadScene("Stage2");
                break;
            case 2:
                SceneManager.LoadScene("Stage3");
                break;
            case 3:
                SceneManager.LoadScene("Stage4");
                break;
            case -1:
                break;
        }
        
    }

    public void OnClick_SkillInfo()
    {
        Image image = GetComponent<Image>();

        if (image != null)
        {
            image.color = Color.blue;
        }
    }

    public void OnButtonClick_Menu(int k)
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].gameObject.SetActive(false);
        }
        canvas[k].gameObject.SetActive(true);
    }

    public void OnButtonClick_Skill(int layer)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Skill");
        Image[] background = new Image[gameObjects.Length];

        for (int i = 0; i < gameObjects.Length ;i++)
        {
            background[i] = gameObjects[i].GetComponentInChildren<Image>();
            background[i].color = Color.white;
        }

        background[layer].color = Color.blue;
        if (layer == 0)
        {
            skillContext.text = "���� �� \n�׳� �������̴�. 1�α�\n ��ų ������ +5 dmg, ��ٿ� -0.1 sec";
        }   
        else if (layer == 1)
        {
            skillContext.text = "���׿� \n�������̴�. \n ��ų ������ +3 dmg, ��ٿ� -0.1 sec";
        }
    }
    public void OnClick_SaveSkillLevel()
    {
        IOManager.Inst.SaveToThisMethod();
    }
}
