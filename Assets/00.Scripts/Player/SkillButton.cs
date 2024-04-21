
using TMPro;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public TextMeshProUGUI SkillPointText;
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI stringText;

    // Start is called before the first frame update
    public void ButtonPressed(int skillNo)
    {

        int counter = int.Parse(numberText.text);

        if (stringText.text == "-" && IOManager.Instance.playerData.skillLevel[skillNo] > 1)
        {
            IOManager.Instance.playerData.skillPoint += counter--;
        }
        else if (stringText.text == "+" && IOManager.Instance.playerData.skillPoint >= counter + 1)
        {
            IOManager.Instance.playerData.skillPoint -= ++counter;
            
        }
        else
        {
            Debug.Log("��ų����Ʈ ���� �Ǵ� ������ų�� 1�̸����� �ȳ�����.");
        }

        numberText.text = counter + "";
        SkillPointText.text = "��ų����Ʈ : " + IOManager.Instance.playerData.skillPoint;
        IOManager.Instance.playerData.skillLevel[skillNo] = counter;

        IOManager.Instance.SavePlayerDataToJson();
    }
}
