using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace TrainMemory
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private List<int> result;
        private ObservableCollection<TextBox> r;
        private string inputText;
        private bool isEnabledTextBox;
        private bool isEnabledShowCards;


        private string time;
        private DateTime start;
        private DispatcherTimer timer;
        private TimeSpan deltaTime;
        private List<int> input;

        public List<int> Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged(nameof(Result));
            }
        }
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
        public ObservableCollection<TextBox> R
        {
            get => r;
            set
            {
                r = value;
                OnPropertyChanged(nameof(R));
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
            Result = new MainWindowModel().GetList();
            R = new ObservableCollection<TextBox>();
            IsEnabledTextBox = false;
            InputText = string.Empty;
            for (int i = 0; i < Result.Count; i++)
            {
                R.Add(new TextBox() { Text = Result[i].ToString(), Background = new SolidColorBrush(Colors.Red) });
            }
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += showTime;
            timer.Start();       
        });

        public ICommand Check => new DelegateCommand(o =>
        {
            IsEnabledTextBox = false;
            IsEnabledShowCards = true;
            input = new List<int>();
            if (InputText.All(c => char.IsDigit(c) || c == ' '))
            {
                input = InputText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            } 
            while (input.Count < Result.Count) input.Add(0);
            R.Clear();
            for(int i=0; i < input.Count; i++)
            {
                if(input[i] == Result[i]) R.Add(new TextBox() { Text = Result[i].ToString(), Background = new SolidColorBrush(Colors.Green) });
                else R.Add(new TextBox() { Text = Result[i].ToString(), Background = new SolidColorBrush(Colors.Red) });
            }
        });

        private void showTime(object obj, EventArgs e)
        {
            if (DateTime.Now - start >= new TimeSpan(0, 0, 0, 10))
            {
                timer.Stop();
                IsEnabledTextBox = true;
                //Result = Enumerable.Repeat(0,10).ToList();
                R.Clear();
                for (int i = 0; i < 10; i++)
                {
                    R.Add(new TextBox() { Text = "", IsEnabled = true });
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