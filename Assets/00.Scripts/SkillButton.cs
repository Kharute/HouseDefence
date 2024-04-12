using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class SkillButton : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI stringText;

    // Start is called before the first frame update
    public void ButtonPressed()
    {
        int counter = int.Parse(numberText.text);
        if (stringText.text == "-")
            counter--;
        else if (stringText.text == "+")
            counter++;

        numberText.text = counter + "";
    }
}
