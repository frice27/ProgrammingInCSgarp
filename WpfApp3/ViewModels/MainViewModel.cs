using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfPrac1.Models;

namespace WpfPrac1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime? _birthDate;
        private string _output;
        private bool _isProcessing;

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); UpdateCanExecute(); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); UpdateCanExecute(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); UpdateCanExecute(); }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); UpdateCanExecute(); }
        }

        public string Output
        {
            get => _output;
            set { _output = value; OnPropertyChanged(nameof(Output)); }
        }

        public ICommand ProceedCommand { get; }

        public MainViewModel()
        {
            ProceedCommand = new RelayCommand(async () => await Proceed(), () => CanProceed);
        }

        private bool CanProceed =>
            !_isProcessing &&
            !string.IsNullOrWhiteSpace(FirstName) &&
            !string.IsNullOrWhiteSpace(LastName) &&
            !string.IsNullOrWhiteSpace(Email) &&
            BirthDate.HasValue;

        private void UpdateCanExecute()
        {
            (ProceedCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private async Task Proceed()
        {
            _isProcessing = true;
            UpdateCanExecute();

            await Task.Delay(500); // Імітація обчислення

            try
            {
                var person = new Person(FirstName, LastName, Email, BirthDate.Value);
                Output = $"Ім’я: {person.FirstName}\n" +
                         $"Прізвище: {person.LastName}\n" +
                         $"Email: {person.Email}\n" +
                         $"Дата народження: {person.BirthDate:d}\n" +
                         $"Дорослий: {(person.IsAdult ? "Так" : "Ні")}\n" +
                         $"Знак зодіаку: {person.SunSign}\n" +
                         $"Китайський знак: {person.ChineseSign}\n" +
                         $"Сьогодні день народження: {(person.IsBirthday ? "Так" : "Ні")}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            _isProcessing = false;
            UpdateCanExecute();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;
        public void Execute(object parameter) => _execute?.Invoke();
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
