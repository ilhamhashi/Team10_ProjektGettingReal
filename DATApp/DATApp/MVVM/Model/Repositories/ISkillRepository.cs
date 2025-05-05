using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public interface ISkillRepository
    {
        IEnumerable<Skill> GetAllSkills();
        Skill GetSkill(int skillNumber);
        void AddSkill(Skill skill);
        void UpdateSkill(Skill skill);
        void DeleteSkill(int skillNumber);
    } 
}
