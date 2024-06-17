using System.ComponentModel;
using TMPro;
using UnityEngine;
using ViewModel.Extention;

//View

public class SkillButton : MonoBehaviour
{
    // 스킬 1개당 하나만 넣고
    [SerializeField] TextMeshProUGUI skillLevelText;
    [SerializeField] TextMeshProUGUI commentText;
    [SerializeField] int skillId;
    private SkillViewModel _vm;

    //스킬만 꺼낸다.

    private void OnEnable()
    {
        if(_vm == null)
        {
            _vm = new SkillViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.RegisterEventsOnEnable();
            _vm.RefreshViewModel(skillId);
        }
        
    }
    private void OnDisable()
    {
        if (_vm != null)
        {
            _vm.UnRegisterEventsOnDisable();
            _vm.PropertyChanged -= OnPropertyChanged;
            _vm = null;
        }
    }
    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm._skillLevel):
                skillLevelText.text = $"{_vm._skillLevel}";
                break;
            case nameof(_vm._id):
                commentText.text = $"{_vm._name} \n {_vm._skillComment}";
                break;
        }
    }


    // 자네가 관리하지말고 IO Manager에게 던져주셈 
    public void ButtonPressed(int skillID, bool isUpgrade)
    {
        IOManager.Inst.RequestSkillChange(skillID, isUpgrade);
    }
}
