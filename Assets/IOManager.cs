using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

[System.Serializable]
public class PlayerData
{
    public int atk;
    public int def;
    public int spd;
    public List<int> skillLevel;
    public int skillPoint;
}

[System.Serializable]
public class IOManager : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI skillPointObject;

    [ContextMenu("To Json Data")]
    public void SavePlayerDataToJson()
    {
        // ToJson�� ����ϸ� JSON���·� �����õ� ���ڿ��� �����ȴ�  
        string jsonData = JsonUtility.ToJson(playerData, true);
        // �����͸� ������ ��� ����
        string path = Path.Combine(Application.dataPath, "playerData.json");
        // ���� ���� �� ����
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadPlayerDataFromJson()
    {
        // �����͸� �ҷ��� ��� ����
        string path = Path.Combine(Application.dataPath, "playerData.json");
        // ������ �ؽ�Ʈ�� string���� ����
        string jsonData = File.ReadAllText(path);
        // �� Json�����͸� ������ȭ�Ͽ� playerData�� �־���
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);
    }

    private void Awake()
    {
        LoadPlayerDataFromJson();
        SkillLevelGet();
        SkillPointGet();
    }

    void SkillPointGet()
    {
        skillPointObject.text =  "��ų ����Ʈ : "+ playerData.skillPoint;
    }
    void SkillLevelGet()
    {
        List<GameObject> skillLevels = new List<GameObject>();

        skillLevels.AddRange(GameObject.FindGameObjectsWithTag("SkillLevel"));
        for(int i = 0; i < skillLevels.Count; i++)
        {
            TextMeshProUGUI text = skillLevels[i].GetComponent<TextMeshProUGUI>();
            text.text = playerData.skillLevel[i].ToString();
        }
    }
}
