using UnityEngine;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    public GameObject m_Menu;
    bool isOpened = false;

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (context.control.name == "k" && !isOpened)
            {
                m_Menu.gameObject.SetActive(true);
                isOpened = true;
            }
            else if (isOpened)
            {
                m_Menu.gameObject.SetActive(false);
                isOpened = false;
            }
            /*else if (context.control.name == "i")
            {

            }*/
        }
        
    }
}
