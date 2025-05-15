using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public interface IModuleRepository
    {
        IEnumerable<Module> GetAllModules();
        Module GetModule(int moduleNumber);
        void AddModule(Module module);
        void UpdateModule(Module module);
        void DeleteModule(Module module);
    }
}