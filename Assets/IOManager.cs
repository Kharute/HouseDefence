using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;

[System.Serializable]
public class PlayerData
{
    public int atk;
    public int def;
    public int spd;
    public Dictionary<int, List<bool>> stageCleard;

    public List<int> skillLevel;
    public int skillPoint;
}

[System.Serializable]
public class IOManager : MonoBehaviour
{
    private static IOManager instance = null;
    public PlayerData playerData;
    public TextMeshProUGUI skillPointObject;
    public int curStage;


    [ContextMenu("To Json Data")]
    public void SavePlayerDataToJson()
    {
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        string jsonData = JsonUtility.ToJson(playerData, true);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(Application.dataPath, "playerData.json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadPlayerDataFromJson()
    {
        // 데이터를 불러올 경로 지정
        string path = Path.Combine(Application.dataPath, "playerData.json");
        // 파일의 텍스트를 string으로 저장
        string jsonData = File.ReadAllText(path);
        // 이 Json데이터를 역직렬화하여 playerData에 넣어줌
        playerData = JsonUtility.FromJson<PlayerData>(jsonData);

        if(playerData.stageCleard == null)
        {
            playerData.stageCleard = new Dictionary<int, List<bool>>()
            {
                {0, new List<bool> {false, false, false }},
                {1, new List<bool> {false, false, false }},
                {2, new List<bool> {false, false, false }},
                {3, new List<bool> {false, false, false }}
            };
        }    

        SavePlayerDataToJson();
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        LoadPlayerDataFromJson();
        
        SkillLevelGet();
        SkillPointGet();
    }

    public static IOManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void SkillPointGet()
    {
        skillPointObject.text =  "스킬 포인트 : "+ playerData.skillPoint;
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
