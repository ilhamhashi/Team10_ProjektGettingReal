using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using DATApp.MVVM.ViewModel;

namespace DATApp.Tests
{
    internal class FileUserRepositoryTests
    {
        private readonly string _userFilePath;

        public FileUserRepositoryTests(string filePath)
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

    internal class FileSkillRepositoryTests
    {
        private readonly string _skillFilePath;

        public FileSkillRepositoryTests(string filePath)
        {
            _skillFilePath = filePath;

            if (!File.Exists(_skillFilePath))
            {
                File.Create(_skillFilePath).Close();
            }
        }

        public void AddSkill(Skill skill)
        {
            if (GetAll().ToList().Count == 0)
            {
                skill.Number = 1;
            }
            else
            {
                skill.Number = GetAll().Max(s => s.Number) + 1;
            }
            try
            {
                File.AppendAllText(_skillFilePath, skill.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        public void DeleteSkill(Skill skill)
        {
            List<Skill> skills = GetAll().ToList();
            skills.RemoveAll(s => s.Number == skill.Number);
            RewriteFile(skills);
        }

        public IEnumerable<Skill> GetAll()
        {
            try
            {
                return File.ReadAllLines(_skillFilePath)
                           .Where(line => !string.IsNullOrEmpty(line)) // Avoid empty lines
                           .Select(Skill.FromString)
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return new List<Skill>(); // Return an empty list if there is an error
            }
        }

        public Skill GetSkill(int skillNumber)
        {
            return GetAll().FirstOrDefault(s => s.Number == skillNumber);
        }



        public void UpdateSkill(Skill skill)
        {
            List<Skill> skills = GetAll().ToList();
            int index = skills.FindIndex(s => s.Number == skill.Number);
            if (index != -1)
            {
                skills[index] = skill;
                RewriteFile(skills);
            }
        }

        private void RewriteFile(List<Skill> skills)
        {
            try
            {
                File.WriteAllLines(_skillFilePath, skills.Select(s => s.ToString()));
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }

        }
    }

    public class FileModuleRepositoryTests : IModuleRepository
    {
        private readonly string _moduleFilePath;

        public FileModuleRepositoryTests(string filePath)
        {
            _moduleFilePath = filePath;
            if (!File.Exists(_moduleFilePath))
            {
                File.Create(_moduleFilePath).Close();
            }
        }

        public void AddModule(Module module)
        {
            if (GetAll().ToList().Count == 0)
            {
                module.Number = 1;
            }
            else
            {
                module.Number = GetAll().Max(m => m.Number) + 1;
            }
            try
            {
                File.AppendAllText(_moduleFilePath, module.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        public void DeleteModule(Module module)
        {
            List<Module> modules = GetAll().ToList();
            modules.RemoveAll(m => m.Number == module.Number);
            RewriteFile(modules);
        }

        public IEnumerable<Module> GetAll()
        {
            try
            {
                return File.ReadAllLines(_moduleFilePath)
                           .Where(line => !string.IsNullOrEmpty(line)) // Undgå tomme linjer
                           .Select(Module.FromString)
                           .ToList();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved læsning af fil: {ex.Message}");
                return [];
            }
        }

        public Module GetModule(int number)
        {
            return GetAll().FirstOrDefault(m => m.Number == number);
        }

        public void UpdateModule(Module module)
        {
            List<Module> modules = GetAll().ToList();
            int index = modules.FindIndex(m => m.Number == module.Number);
            if (index != -1)
            {
                modules[index] = module;
                RewriteFile(modules);
            }
        }

        private void RewriteFile(List<Module> modules)
        {
            try
            {
                File.WriteAllLines(_moduleFilePath, modules.Select(m => m.ToString()));
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }
    }

    internal class FileNoteRepositoryTests : INoteRepository
    {
        private readonly string _noteFilePath;

        public FileNoteRepositoryTests(string filePath)
        {
            _noteFilePath = filePath;

            if (!File.Exists(_noteFilePath))
            {
                File.Create(_noteFilePath).Close();
            }
        }

        public void Add(Note note)
        {
            var notes = GetAll().ToList();
            if (notes.Count == 0)
            {
                note.Number = 1;
            }
            else
            {
                note.Number = notes.Max(n => n.Number) + 1;
            }
            note.Client = MainWindowViewModel.CurrentUser;
            notes.Add(note);

            try
            {
                File.AppendAllText(_noteFilePath, note.ToString() + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved skrivning til fil: {ex.Message}");
            }
        }

        public void Delete(Note note)
        {
            List<Note> notes = GetAll().ToList();
            notes.RemoveAll(n => n.Number == note.Number);
            File.WriteAllLines(_noteFilePath, notes.Select(n => n.ToString()));
        }

        public IEnumerable<Note> GetAll()
        {
            List<Note> notes = File.ReadAllLines(_noteFilePath).Where(line => !string.IsNullOrEmpty(line)).Select(Note.FromString).ToList();

            if (MainWindowViewModel.CurrentUser == null)
            {
                return [];
            }

            else if (MainWindowViewModel.CurrentUser.IsAdmin)
            {
                return notes;
            }
            else
            {
                var userNotes = notes
                    .Where(n => n.Client != null && n.Client.Email == MainWindowViewModel.CurrentUser.Email);
                return userNotes;
            }
        }

        public Note GetNote(int number)
        {
            return GetAll().FirstOrDefault(n => n.Number == number);
        }

        public void Update(Note note)
        {
            var existingNote = GetAll().ToList().FirstOrDefault(n => n.Number == note.Number);
            if (existingNote != null)
            {
                existingNote.Name = note.Name;
                existingNote.Content = note.Content;
                existingNote.Number = note.Number;

                try
                {
                    File.WriteAllLines(_noteFilePath, GetAll().Select(n => n.ToString()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl ved opdatering af fil: {ex.Message}");
                }
            }
        }
    }
}
