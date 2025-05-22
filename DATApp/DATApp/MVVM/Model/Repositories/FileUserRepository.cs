using System.IO;
using DATApp.MVVM.Model.Classes;

namespace DATApp.MVVM.Model.Repositories
{
    public class FileUserRepository : IUserRepository
    {
        private readonly string _userFilePath;

        public FileUserRepository(string filePath)
        {
            _userFilePath = filePath;
            if (!File.Exists(_userFilePath))
            {
                File.Create(_userFilePath).Close();
            }
        }

        public void AddUser(User user)
        {
            try
            {
                File.AppendAllText(_userFilePath, user.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        public void DeleteUser(User user)
        {
            List<User> users = GetAll().ToList();
            users.RemoveAll(u => u.Email == user.Email);
            RewriteFile(users);
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return File.ReadAllLines(_userFilePath)
                           .Where(line => !string.IsNullOrEmpty(line)) // Undgå tomme linjer
                           .Select(User.FromString)
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return [];
            }
        }

        public User GetUser(string email)
        {
            return GetAll().FirstOrDefault(u => u.Email == email);
        }

        public void UpdateUser(User user)
        {
            List<User> users = GetAll().ToList();
            int index = users.FindIndex(u => u.Email == user.Email);
            if (index != -1)
            {
                users[index] = user;
                RewriteFile(users);
            }
        }

        public bool ValidateUserLogin(string email, string password)
        {
            try
            {
                foreach (User user in GetAll())
                {
                    var storedEmail = user.Email;
                    var storedPassword = user.Password;
                    if (storedEmail == email && storedPassword == password)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return false;
            }
        }

        private void RewriteFile(List<User> users)
        {
            try
            {
                File.WriteAllLines(_userFilePath, users.Select(u => u.ToString()));
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }
    }
}
