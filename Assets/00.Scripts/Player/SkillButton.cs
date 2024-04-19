
using TMPro;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    public IOManager IOManager;
    public TextMeshProUGUI SkillPointText;
    public TextMeshProUGUI numberText;
    public TextMeshProUGUI stringText;

    // Start is called before the first frame update
    public void ButtonPressed(int skillNo)
    {
        // 스킬포인트 확인하고 있으면 진행 없으면 빠꾸

        int counter = int.Parse(numberText.text);

        if (stringText.text == "-" && IOManager.playerData.skillLevel[skillNo] > 1)
        {
            IOManager.playerData.skillPoint += counter--;
        }
        else if (stringText.text == "+" && IOManager.playerData.skillPoint >= counter + 1)
        {
            IOManager.playerData.skillPoint -= ++counter;
            
        }
        else
        {
            Debug.Log("스킬포인트 부족 또는 지정스킬은 1미만으로 안내려감.");
        }

        numberText.text = counter + "";
        SkillPointText.text = "스킬포인트 : " + IOManager.playerData.skillPoint;
        IOManager.playerData.skillLevel[skillNo] = counter;

        IOManager.SavePlayerDataToJson();
    }
}
