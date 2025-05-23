using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public interface IModuleRepository
    {
        IEnumerable<Module> GetAll();
        Module GetModule(int number);
        void AddModule(Module module);
        void UpdateModule(Module module);
        void DeleteModule(Module module);
    }
}