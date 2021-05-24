using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand Save => new DelegateCommand(o =>
        {
            Settings.Default["Count"] = Count;
            Settings.Default["Start"] = Start;
            Settings.Default["Finish"] = Finish;

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
