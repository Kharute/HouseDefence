using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public Canvas[] canvas;
    public TextMeshProUGUI skillContext;

    // Start is called before the first frame update
    public void StartButtonClick()
    {
        SceneManager.LoadScene("StageScene");
    }

    public void StartStageButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void EndButtonClick()
    {
        if (UnityEditor.EditorApplication.isPlaying)
            UnityEditor.EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }
    public void OnMouseStay_Menu()
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
            skillContext.text = "매직 볼 \n그냥 매직볼이다. 1인기\n 스킬 레벨당 +5 dmg, 쿨다운 -0.1 sec";
        }   
        else if (layer == 1)
        {
            skillContext.text = "메테오 \n광역기이다. \n 스킬 레벨당 +3 dmg, 쿨다운 -0.1 sec";
        }
    }
}
