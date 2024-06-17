

namespace ViewModel.Extention
{
    public static class SkillModelExtention
    {
        public static void RefreshViewModel(this SkillViewModel vm, int id)
        {
            IOManager.Inst.RefreshSkillInfo(id, vm.OnRefreshViewModel);
        }

        public static void OnRefreshViewModel(this SkillViewModel vm, string name, int skillLevel, float skillCooltime, int skillPoint, string skillComment)
        {
            vm._name = name;
            vm._skillLevel = skillLevel;
            vm._skillCooltime = skillCooltime;
            vm._skillPoint = skillPoint;
            vm._skillComment = skillComment;
        }
        public static void RegisterEventsOnEnable(this SkillViewModel vm)
        {
            IOManager.Inst.Register_SkillChangeCallback(vm.OnResponseSkillChange);
        }
        public static void UnRegisterEventsOnDisable(this SkillViewModel vm)
        {
            IOManager.Inst.UnRegister_SkillChangeCallback(vm.OnResponseSkillChange);
        }

        public static void OnResponseSkillChange(this SkillViewModel vm, int sId, int sLevel)
        {
            if (vm._id != sId)
                return;

            vm._skillLevel = sLevel;
        }
    }
}

