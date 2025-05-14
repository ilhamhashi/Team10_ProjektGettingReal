using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        bool ValidateUserLogin(string email, string password);
    }
}
