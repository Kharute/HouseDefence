
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
            Debug.Log("스킬포인트 부족 또는 지정스킬은 1미만으로 안내려감.");
        }

        numberText.text = counter + "";
        SkillPointText.text = "스킬포인트 : " + IOManager.Instance.playerData.skillPoint;
        IOManager.Instance.playerData.skillLevel[skillNo] = counter;

        IOManager.Instance.SavePlayerDataToJson();
    }
}
