using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillMenu : MonoBehaviour
{
    List<float> skillLevel = new List<float>();

    GameObject[] skills;
    private void Awake()
    {
        skills = GameObject.FindGameObjectsWithTag("Skill");
    }
}
