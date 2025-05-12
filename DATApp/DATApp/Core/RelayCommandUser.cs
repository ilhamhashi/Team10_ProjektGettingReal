using System.Windows.Input;

namespace DATApp.Core
{
    class RelayCommandUser : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommandUser(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter) => canExecute == null || canExecute();
        public void Execute(object parameter) => execute();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
