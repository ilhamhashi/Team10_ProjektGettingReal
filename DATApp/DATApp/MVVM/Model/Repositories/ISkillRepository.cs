using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public interface ISkillRepository
    {
        IEnumerable<Skill> GetAll();
        Skill GetSkill(int number);
        void AddSkill(Skill skill);
        void UpdateSkill(Skill skill);
        void DeleteSkill(Skill skill);
    } 
}
