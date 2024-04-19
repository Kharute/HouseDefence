using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public TextMeshProUGUI skillContext;
    public Canvas[] canvas;

    public int stage = -1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
    }
    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called before the first frame update
    public void StartButtonClick()
    {
        
        SceneManager.LoadScene("StageScene");
    }

    public void StartStageButtonClick(int i)
    {
        stage = i;
        SceneManager.LoadScene("MainScene");
    }

    public void EndButtonClick()
    {
        /*if (UnityEditor.EditorApplication.isPlaying)
            UnityEditor.EditorApplication.isPlaying = false;
        else*/
            Application.Quit();
    }
    public void OnMouseStay_Menu()
    {
        Image image = GetComponent<Image>();

        if (image != null)
        {
            image.color = Color.blue;
        }
    }

    public void OnButtonClick_Menu(int k)
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].gameObject.SetActive(false);
        }
        canvas[k].gameObject.SetActive(true);
    }

    public void OnButtonClick_Skill(int layer)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Skill");
        Image[] background = new Image[gameObjects.Length];
        for (int i = 0; i < gameObjects.Length ;i++)
        {
            background[i] = gameObjects[i].GetComponentInChildren<Image>();
            background[i].color = Color.white;
        }

        background[layer].color = Color.blue;
        if (layer == 0)
        {
            skillContext.text = "매직 볼 \n그냥 매직볼이다. 1인기\n 스킬 레벨당 +5 dmg, 쿨다운 -0.1 sec";
        }   
        else if (layer == 1)
        {
            skillContext.text = "메테오 \n광역기이다. \n 스킬 레벨당 +3 dmg, 쿨다운 -0.1 sec";
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene" && stage > -1)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Stage");
            for(int i = 0; i< gameObjects.Length; i++)
                gameObjects[i].SetActive(false);

            if(gameObjects.Length > stage)
                gameObjects[stage].SetActive(true);
        }
        
    }
}
