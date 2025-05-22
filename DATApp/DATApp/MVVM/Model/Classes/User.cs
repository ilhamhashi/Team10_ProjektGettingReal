namespace DATApp.MVVM.Model.Classes
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public override string ToString()
        {
            return $"{Name},{Email},{Password},{IsAdmin}";
        }

        public static User FromString(string input)
        {
            string[] parts = input.Split(','); // Opdeler strengen baseret på kommategn
            return new User
            {
                Name = parts[0],
                Email = parts[1],
                Password = parts[2],
                IsAdmin = bool.Parse(parts[3]),
            };
        }
    }
}
