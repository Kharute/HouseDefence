using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChange : MonoBehaviour
{
    public void OnClick_StartStageUI()
    {
        //[TODO - KDH :  Scene -> UI change]
        gameObject.SetActive(false);
    }
    public void OnClick_EndButton()
    {
        Application.Quit();
    }

}
