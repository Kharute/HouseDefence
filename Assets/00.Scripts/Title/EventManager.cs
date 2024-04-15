using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum StageState
{
    Stage, Store, Skill, Inventory
}

public class EventManager : MonoBehaviour
{
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

    public void OnButtonClick_Menu()
    {
        
    }
}
