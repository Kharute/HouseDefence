using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public Canvas[] canvas;

    // Start is called before the first frame update
    public void StartButtonClick()
    {
        SceneManager.LoadScene("StageScene");
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

    public void OnButtonClick_Menu_Stage()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].gameObject.SetActive(false);
        }
        canvas[0].gameObject.SetActive(true);
    }
    public void OnButtonClick_Menu_Store()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].gameObject.SetActive(false);
        }
        canvas[1].gameObject.SetActive(true);
    }
    public void OnButtonClick_Menu_Skill()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].gameObject.SetActive(false);
        }
        canvas[2].gameObject.SetActive(true);
    }
    public void OnButtonClick_Menu_Inventory()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].gameObject.SetActive(false);
        }
        canvas[3].gameObject.SetActive(true);
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
    }
}
