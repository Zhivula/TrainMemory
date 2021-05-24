using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TrainMemory.Model;
using TrainMemory.View;
using Card = TrainMemory.Model.Card;

namespace TrainMemory
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Card> result;
        private string inputText;
        private bool isEnabledTextBox;
        private bool isEnabledShowCards;
        private List<int> numbers;


        private string time;
        private DateTime start;
        private DispatcherTimer timer;
        private TimeSpan deltaTime;

        public bool IsEnabledTextBox
        {
            get => isEnabledTextBox;
            set
            {
                isEnabledTextBox = value;
                OnPropertyChanged(nameof(IsEnabledTextBox));
            }
        }
        public bool IsEnabledShowCards
        {
            get => isEnabledShowCards;
            set
            {
                isEnabledShowCards = value;
                OnPropertyChanged(nameof(IsEnabledShowCards));
            }
        }
        public string InputText
        {
            get => inputText;
            set
            {
                inputText = value;
                OnPropertyChanged(nameof(InputText));
            }
        }
        public ObservableCollection<Card> Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged(nameof(Result));
            }
        }
        public string Time
        {
            get => time;
            set
            {
                time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
        public MainWindowViewModel()
        {
            InputText = string.Empty;
            IsEnabledTextBox = false;
            IsEnabledShowCards = true;
        }

        public ICommand ShowCards => new DelegateCommand(o =>
        {
            IsEnabledShowCards = false;
            start = DateTime.Now;
            numbers = new MainWindowModel().GetList();
            Result = new ObservableCollection<Card>();
            IsEnabledTextBox = false;
            InputText = string.Empty;
            for (int i = 0; i < numbers.Count; i++)
            {
                Result.Add(new Card() { Text = numbers[i].ToString(), Background = new SolidColorBrush(Colors.Red), IsEnabled=true, Symbol = PackIconKind.Eye });
            }
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += showTime;
            timer.Start();       
        });
        public ICommand Settings => new DelegateCommand(o =>
        {
            var windowSettings = new SettingsView();
            windowSettings.Show();
        });
        public ICommand IconButton => new DelegateCommand(o =>
        {
            
        });
        public ICommand Check => new DelegateCommand(o =>
        {
            IsEnabledTextBox = false;
            IsEnabledShowCards = true;
            var input = new List<int>();
            if (InputText.All(c => char.IsDigit(c) || c == ' '))
            {
                input = InputText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            } 
            while (input.Count < Result.Count) input.Add(0);
            Result.Clear();
            for(int i=0; i < input.Count; i++)
            {
                if(input[i] == numbers[i]) Result.Add(new Card() { Text = numbers[i].ToString(), Background = new SolidColorBrush(Colors.Green), IsEnabled = true, Symbol = PackIconKind.Eye });
                else Result.Add(new Card() { Text = numbers[i].ToString(), Background = new SolidColorBrush(Colors.Red), IsEnabled = true, Symbol = PackIconKind.Eye });
            }
        });

        //Срабатывает после того, как таймер отсчитает время
        private void showTime(object obj, EventArgs e)
        {
            if (DateTime.Now - start >= new TimeSpan(0, 0, 0, 10))
            {
                timer.Stop();
                IsEnabledTextBox = true;
                Result.Clear();
                for (int i = 0; i < numbers.Count; i++)
                {
                    Result.Add(new Card());
                }
            }
            deltaTime = DateTime.Now - start;
            Time = (DateTime.Now - start).ToString(@"hh\:mm\:ss");
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}