using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    internal interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(string userName);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(string userName);
    }
}
