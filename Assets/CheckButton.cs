using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckButton : MonoBehaviour
{
    public GameObject updateSystem;
    UpdateInventory updateInventory;
    private void Awake()
    {
        updateInventory = updateSystem.GetComponent<UpdateInventory>();
    }
    public void OnButtonClick(bool isWin)
    {
        if(isWin)
        {
            updateInventory.ItemUpdate();
        }
        SceneManager.LoadScene("StageScene");
    }
}
