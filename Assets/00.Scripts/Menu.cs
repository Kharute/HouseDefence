using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    public GameObject[] m_Menu;
    bool isOpened_skill = false;
    bool isOpened_inventory = false;

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (context.control.name == "k")
            {
                isOpened_skill = !isOpened_skill;
                m_Menu[0].gameObject.SetActive(isOpened_skill);
            }

            else if (context.control.name == "i")
            {
                isOpened_inventory = !isOpened_inventory;
                m_Menu[1].gameObject.SetActive(isOpened_inventory);
            }
        }
    }
}
