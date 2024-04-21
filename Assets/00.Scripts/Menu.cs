using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    bool isOpened_skill = false;
    bool isOpened_inventory = false;
    bool isOpened_todoList = false;

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            if (context.control.name == "i")
            {
                isOpened_inventory = !isOpened_inventory;
                transform.GetChild(0).gameObject.SetActive(isOpened_inventory);
                //m_Menu[1].gameObject.SetActive(isOpened_inventory);
            }
            else if (context.control.name == "tab")
            {
                isOpened_todoList = !isOpened_todoList;
                transform.GetChild(1).gameObject.SetActive(isOpened_todoList);
                //m_Menu[2].gameObject.SetActive(isOpened_todoList);
            }
        }
    }
}
