using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TrainMemory.Properties;
using TrainMemory.View;

namespace TrainMemory.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private int count;
        private int start;
        private int finish;
        private int seconds;
        private bool isChecked;

        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
        public int Start
        {
            get => start;
            set
            {
                start = value;
                OnPropertyChanged(nameof(Start));
            }
        }
        public int Finish
        {
            get => finish;
            set
            {
                finish = value;
                OnPropertyChanged(nameof(Finish));
            }
        }
        public int Seconds
        {
            get => seconds;
            set
            {
                seconds = value;
                OnPropertyChanged(nameof(Seconds));
            }
        }
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
        public SettingsViewModel()
        {
            Count = int.Parse(Settings.Default["Count"].ToString());
            Start = int.Parse(Settings.Default["Start"].ToString());
            Finish = int.Parse(Settings.Default["Finish"].ToString());
            Seconds = int.Parse(Settings.Default["Seconds"].ToString());
            IsChecked = bool.Parse(Settings.Default["IsChecked"].ToString());
        }
        public ICommand Save => new DelegateCommand(o =>
        {
            if (Count > 0) Settings.Default["Count"] = Count;
            else MessageBox.Show("Количество элементов должно быть больше нуля.");

            if(Start >= 0 && Start < 100) Settings.Default["Start"] = Start;
            else MessageBox.Show("Допускается ввод только положительных чисел до 100.");

            if (Finish > 0 && Finish < 100) Settings.Default["Finish"] = Finish;//последный крайний элемент не может быть равен нулю
            else MessageBox.Show("Допускается ввод только положительных чисел до 100.");

            if (Seconds > 0) Settings.Default["Seconds"] = Seconds;
            else MessageBox.Show("Допускается ввод только положительных чисел.");

            Settings.Default["IsChecked"] = IsChecked;

            Settings.Default.Save();
            ShowNotification();
        });
        public ICommand CloseWindow => new DelegateCommand(o =>
        {
            var window = Application.Current.Windows.OfType<SettingsView>().FirstOrDefault();
            window.Close();
        });
        private void ShowNotification()
        {

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
