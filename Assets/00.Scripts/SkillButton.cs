using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class SkillButton : MonoBehaviour
{
    public IOManager IOManager;
    public TextMeshProUGUI SkillPointText;
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI stringText;

    // Start is called before the first frame update
    public void ButtonPressed(int skillNo)
    {
        // ��ų����Ʈ Ȯ���ϰ� ������ ���� ������ ����

        int counter = int.Parse(numberText.text);

        if (stringText.text == "-" && IOManager.playerData.skillLevel[skillNo] > 1)
        {
            IOManager.playerData.skillPoint += IOManager.playerData.skillLevel[skillNo];
            counter--;
        }
        else if (stringText.text == "+" && IOManager.playerData.skillPoint >= IOManager.playerData.skillLevel[skillNo])
        {
            IOManager.playerData.skillPoint -= IOManager.playerData.skillLevel[skillNo];
            counter++;
        }
        else
        {
            Debug.Log("��ų����Ʈ ���� �Ǵ� ������ų�� 1�̸����� �ȳ�����.");
        }

        numberText.text = counter + "";
        SkillPointText.text = "��ų����Ʈ : " + IOManager.playerData.skillPoint;
        IOManager.playerData.skillLevel[skillNo] = counter;

        IOManager.SavePlayerDataToJson();
    }
}
