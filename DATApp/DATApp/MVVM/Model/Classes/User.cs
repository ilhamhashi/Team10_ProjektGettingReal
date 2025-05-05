using System;
using System.Data;
using Microsoft.VisualBasic.FileIO;


namespace DATApp.MVVM.Model.Classes
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }        
        public Roles Role { get; set; }


        public override string ToString()
        {
            return $"{Name},{Email},{Password},{UserName},{IsAdmin},{Role}";
        }

        public static User FromString(string input)
        {
            string[] parts = input.Split(','); // Opdeler strengen baseret på kommategn
            return new User
            {
                Name = parts[0],
                Email = parts[1],
                Password = parts[2],
                UserName = parts[3],
                IsAdmin = bool.Parse(parts[4]),
                Role = Enum.Parse<Roles>(parts[5])
            };
        }


        /*public User(string name, string email, string password, string userName, Roles role, bool isAdmin)
    {
        _name = name;
        _email = email;
        _password = password;
        _userName = userName;
        _role = role;
        _isAdmin = isAdmin;
    }*/
    }
}
