using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ViewModel;

public class SkillViewModel : ViewModelBase
{
    public int _id;
    public string _name { get; set; }
    public int _skillLevel { get; set; }
    public float _skillCooltime { get; set; }
    public int _skillPoint { get; set; }
    public string _skillComment { get; set; }


}
