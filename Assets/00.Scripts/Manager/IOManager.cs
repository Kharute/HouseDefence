using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

[System.Serializable]
public class GameData
{
    PlayerData _playerData;
    Dictionary<int, List<bool>> _stageData;
    List<SkillData> _skillData;

    public PlayerData PlayerDATA
    {
        get { 
            if (_playerData == null)
                _playerData = new PlayerData();

            return _playerData; 
        }
        set { _playerData = value; }
    }
    public Dictionary<int, List<bool>> StageDATA
    { 
        get
        {
            if (_stageData == null)
            {
                _stageData = new Dictionary<int, List<bool>>();

                for (int i = 0; i < 4; i++)
                {
                    List<bool> bools = new List<bool>() { false, false, false };

                    _stageData.Add(i, bools);
                }
            }
            return _stageData;
        }
        set
        {
            _stageData = value;
        }
    }

    public List<SkillData> SkillDATA
    {
        get
        {
            if (_skillData == null)
            {
                _skillData = new List<SkillData>();

                for (int i = 0; i < 2; i++)
                {
                    SkillData sData = new SkillData();
                    sData.InitSkillData();
                    _skillData.Add(sData);
                }
            }

            return _skillData; 
        }
        set { _skillData = value; }
    }
}

public class PlayerData
{
    public PlayerData()
    {
        ATK = 10;
        DEF = 10;
        SPD = 10;
    }
    public int ATK { get; set; }
    public int DEF { get; set; }
    public int SPD { get; set; }
}

public class SkillData
{
    public int _id;
    public void InitSkillData()
    {
        _name = "스킬명";
        _skillLevel = 1;
        _skillCooltime = 5.0f;
        _skillPoint = 10;
        _skillComment = "스킬 정보";
    }

    public string _name { get; set; }
    
    public int _skillLevel { get; set; }
    
    public float _skillCooltime { get; set; }
    
    public int _skillPoint { get; set; }
    
    public string _skillComment { get; set; }
    
}

[System.Serializable]
public class IOManager : MonoBehaviour
{
    private static IOManager instance = null;
    public static GameData gameData = null;

    public static PlayerData playerData = null;

    [SerializeField]
    public List<SkillData> skillData = null;

    private Action<int, int> _skillChangeCallback;

    /*private Action<int, int> _skillChangeCallback;
    private Action<int, int> _skillChangeCallback;*/

    //[TODO : 스테이지 개수 확인할 방법 찾아야됨]
    [SerializeField]
    static int currentStage = 4;

    public TextMeshProUGUI skillPointObject;
    public int curStage;

    public static IOManager Inst
    {
        get
        {
            if (null == instance)
            {
                instance = new IOManager();
                InitIOData();
            }

            return instance;
        }
    }


    #region Request

    public void RequestSkillChange(int skillID, bool isUpgrade)
    {
        var curSkillLevel = skillData[skillID]._skillLevel;
        var curSkillPoint = skillData[skillID]._skillPoint;

        if (isUpgrade)
        {

            if (curSkillPoint > curSkillLevel)
            {
                skillData[skillID]._skillPoint -= curSkillLevel;
                skillData[skillID]._skillLevel++;
            }
            else
            {
                Debug.Log("스킬포인트가 적어요");
            }
        }
        else
        {
            if (curSkillLevel > 1)
            {
                skillData[skillID]._skillPoint += curSkillLevel;
                skillData[skillID]._skillLevel--;
            }
            else
            {
                Debug.Log("스킬레벨이 1이에요.");
            }
        }

        // CallBack(현재 스킬 레벨, 포인트 반환)
        _skillChangeCallback.Invoke(curSkillLevel, curSkillPoint);
    }

    #endregion

    #region Callback
    public void Register_SkillChangeCallback(Action<int, int> skillChangeCallback)
    {
        _skillChangeCallback += skillChangeCallback;
    }
    public void UnRegister_SkillChangeCallback(Action<int, int> skillChangeCallback)
    {
        _skillChangeCallback -= skillChangeCallback;
    }

    #endregion


    #region Method

    static void InitIOData()
    {
        LoadPlayerDataFromJson();
        /*SkillLevelGet();
        SkillPointGet();*/
    }

    public void RefreshSkillInfo(int reqId, Action<string, int, float, int, string> callback)
    {
        /*if (skillData[reqId] != null)
        {
            var curSkill = skillData[reqId];
            callback.Invoke(curSkill._name, curSkill._skillLevel, curSkill._skillCooltime, curSkill._skillPoint, curSkill._skillComment);
        }*/
    }

    #endregion

    #region Save/Load
    public void SaveToThisMethod()
    {
        SavePlayerDataToJson();
    }

    [ContextMenu("To Json Data")]
    public void SavePlayerDataToJson()
    {
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        //string jsonData = JsonUtility.ToJson(gameData, true);
        //string jsonData = JsonUtility.ToJson(gameData.SkillDATA, true);
        skillData = gameData.SkillDATA;
        string jsonData = JsonUtility.ToJson(skillData.Count);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(Application.dataPath, "playerData.json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public static void LoadPlayerDataFromJson()
    {
        if (gameData == null)
        {
            gameData = new GameData();
            //SavePlayerDataToJson();
            return;
        }

        /*if (skillData == null)
        {
            skillData = gameData.SkillDATA;
            //SavePlayerDataToJson();
            return;
        }*/

        string path = Path.Combine(Application.dataPath, "playerData.json");
        string jsonData = File.ReadAllText(path);

        gameData = JsonUtility.FromJson<GameData>(jsonData);
    }
    #endregion

    // 바뀔 때마다 저장하지말고 저장버튼 만들어서 저장할 것.

    //View가 Model쪽에 요청하는 쪽으로 접근
    /*static void SkillPointGet()
    {
        var skillData = GameData._skillData.Values;
        skillPointObject.text =  $"스킬 포인트 : {GameData._skillData.Values}";
    }

    static void SkillLevelGet()
    {
        List<GameObject> skillLevels = new List<GameObject>();

        skillLevels.AddRange(GameObject.FindGameObjectsWithTag("SkillLevel"));
        for(int i = 0; i < skillLevels.Count; i++)
        {
            TextMeshProUGUI text = skillLevels[i].GetComponent<TextMeshProUGUI>();
            //text.text = playerData.skillLevel[i].ToString();
        }
    }*/
}
