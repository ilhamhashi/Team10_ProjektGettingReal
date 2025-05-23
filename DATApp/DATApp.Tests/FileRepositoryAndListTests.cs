using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.ViewModel;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DATApp.Tests
{

[TestClass]
    public sealed class FileRepositoryAndListTests
    {
        [TestMethod]
        public void AddUserCommand_ShouldAddUserToRepositoryAndList()
        {
            // Arrange: Opretter instans af mock FileUserRepository og en instans af UsersViewModel
            var userRepositoryTest = new FileUserRepositoryTests("users.txt");
            var viewModelTests = new UsersViewModel();

            // Arrange: Tildeler properties værdier
            viewModelTests.Name = "MyName";
            viewModelTests.Email = "MyEmail";
            viewModelTests.Password = "MyPassword";
            viewModelTests.IsAdmin = true;

            // Act: Kalder AddUserCommand fra UsersViewModel
            viewModelTests.AddUserCommand.CanExecute(null);
            viewModelTests.AddUserCommand.Execute(null);

            // Assert: Tester angivne værdier i mock FileUserRepository og Users listen fra UsersViewModel
            Assert.AreEqual(1, userRepositoryTest.GetAll().ToList().Count);
            Assert.AreEqual(1, viewModelTests.Users.Count);
            Assert.AreEqual("MyName", userRepositoryTest.GetAll().ToList()[0].Name);
            Assert.AreEqual("MyEmail", userRepositoryTest.GetAll().ToList()[0].Email);
            Assert.AreEqual("MyPassword", userRepositoryTest.GetAll().ToList()[0].Password);
            Assert.AreEqual(true, userRepositoryTest.GetAll().ToList()[0].IsAdmin);
            Assert.AreEqual(1, viewModelTests.Users.Count);
        }

        public void AddSkillCommand_ShouldAddSkillToRepositoryAndList()
        {
            // Arrange: Opretter instans af mock FileSkillRepository og en instans af SkillsViewModel
            var skillRepositoryTest = new FileSkillRepositoryTests("skills.txt");
            var viewModelTests = new SkillsViewModel();

            // Arrange: Tildeler properties værdier
            viewModelTests.Number = 1;
            viewModelTests.Name = "MyName";
            viewModelTests.Description = "MyDescription";
            viewModelTests.Module = new Module { Number = 1, Name = "ModuleName", Description = "ModuleDescription"};

            // Act Kalder AddSkillCommand fra SkillsViewModel
            viewModelTests.AddSkillCommand.CanExecute(null);
            viewModelTests.AddSkillCommand.Execute(null);

            // Assert Tester angivne værdier i mock FileSkillRepository og Skills listen fra SkillsViewModel
            Assert.AreEqual(1, skillRepositoryTest.GetAll().ToList().Count);
            Assert.AreEqual(1, viewModelTests.Skills.Count);
            Assert.AreEqual(1, skillRepositoryTest.GetAll().ToList()[0].Number);
            Assert.AreEqual("MyName", skillRepositoryTest.GetAll().ToList()[0].Name);
            Assert.AreEqual("MyDescription", skillRepositoryTest.GetAll().ToList()[0].Description);
            Assert.AreEqual(1, skillRepositoryTest.GetAll().ToList()[0].Module.Number);
            Assert.AreEqual("ModuleName", skillRepositoryTest.GetAll().ToList()[0].Module.Name);
            Assert.AreEqual("ModuleDescription", skillRepositoryTest.GetAll().ToList()[0].Module.Description);
        }

        public void AddModuleCommand_ShouldAddModuleToRepositoryAndList()
        {
            // Arrange: Opretter instans af mock FileModuleRepository og en instans af ModulesViewModel
            var moduleRepositoryTest = new FileModuleRepositoryTests("modules.txt");
            var viewModelTests = new ModulesViewModel();

            // Arrange: Tildeler properties værdier
            viewModelTests.Number = 1;
            viewModelTests.Name = "MyName";
            viewModelTests.Description = "MyDescription";

            // Act Kalder AddModuleCommand fra ModulesViewModel
            viewModelTests.AddModuleCommand.CanExecute(null);
            viewModelTests.AddModuleCommand.Execute(null);

            // Assert Tester angivne værdier i mock FileModuleRepository og Modules listen fra ModulesViewModel
            Assert.AreEqual(1, moduleRepositoryTest.GetAll().ToList().Count);
            Assert.AreEqual(1, viewModelTests.Modules.Count);
            Assert.AreEqual(1, moduleRepositoryTest.GetAll().ToList()[0].Number);
            Assert.AreEqual("MyName", moduleRepositoryTest.GetAll().ToList()[0].Name);
            Assert.AreEqual("MyDescription", moduleRepositoryTest.GetAll().ToList()[0].Description);
        }

        public void AddNoteCommand_ShouldAddNoteToRepositoryAndList()
        {
            // Arrange: Opretter instans af mock FileNoteRepository og en instans af NotesViewModel
            var noteRepositoryTest = new FileNoteRepositoryTests("notes.txt");
            var viewModelTests = new NotesViewModel();

            // Arrange: Tildeler properties værdier
            viewModelTests.Number = 1;
            viewModelTests.Content = "MyContent";
            viewModelTests.DateTime = DateTime.Today;
            viewModelTests.Client = new User { Name = "MyName", Email = "MyEmail", Password = "MyPassword", IsAdmin = false };
            viewModelTests.Skill = new Skill { Number = 1, Name = "SkillName", Description = "SkillDescription", Module = new Module()};

            // Act Kalder AddNoteCommand fra NotesViewModel
            viewModelTests.AddNoteCommand.CanExecute(null);
            viewModelTests.AddNoteCommand.Execute(null);

            // Assert Tester angivne værdier i mock FileNoteRepository og Notes listen fra NotesViewModel
            Assert.AreEqual(1, noteRepositoryTest.GetAll().ToList().Count);
            Assert.AreEqual(1, viewModelTests.Notes.Count);
            Assert.AreEqual(1, noteRepositoryTest.GetAll().ToList()[0].Number);
            Assert.AreEqual("MyContent", noteRepositoryTest.GetAll().ToList()[0].Content);
            Assert.AreEqual("MyName", noteRepositoryTest.GetAll().ToList()[0].Client.Name);
            Assert.AreEqual("MyEmail", noteRepositoryTest.GetAll().ToList()[0].Client.Email);
            Assert.AreEqual("MyPassword", noteRepositoryTest.GetAll().ToList()[0].Client.Password);
            Assert.AreEqual(false, noteRepositoryTest.GetAll().ToList()[0].Client.IsAdmin);
            Assert.AreEqual(1, noteRepositoryTest.GetAll().ToList()[0].Skill.Number);
            Assert.AreEqual("SkillName", noteRepositoryTest.GetAll().ToList()[0].Skill.Name);
            Assert.AreEqual("SkillDescription", noteRepositoryTest.GetAll().ToList()[0].Skill.Description);
            Assert.AreEqual(null, noteRepositoryTest.GetAll().ToList()[0].Skill.Module);

        }
    }
}
