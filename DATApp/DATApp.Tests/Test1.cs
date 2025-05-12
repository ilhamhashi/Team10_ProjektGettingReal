using DATApp.MVVM.Model.Classes;
using DATApp.MVVM.Model.Repositories;
using DATApp.MVVM.ViewModel;

namespace DATApp.Tests
{
    [TestClass]
    public sealed class CreateUser
    {
        [TestMethod]
        public void CreateUserToStringFromString()
        {
            // Arrange
            var userRepositoryTest = new FileUserRepository("userstest.txt");
           //vr viewModel = new MainWindowViewModel();
            //new User { "Bo", "bo@gmail.com", "adgangskode", "BO1234", false, Roles.Klient };

            // Act

            // Assert
        }
    }
}
