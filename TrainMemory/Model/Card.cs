using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TrainMemory.Model
{
    class Card : INotifyPropertyChanged
    {
        private string text;
        private int number;
        private SolidColorBrush background;
        private bool isEnabled;
        private PackIconKind symbol;

        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public PackIconKind Symbol
        {
            get => symbol;
            set
            {
                symbol = value;
                OnPropertyChanged(nameof(Symbol));
            }
        }
        public int Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        public SolidColorBrush Background
        {
            get => background;
            set
            {
                background = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
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
