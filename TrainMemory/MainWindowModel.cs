using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TrainMemory
{
    class MainWindowModel: INotifyPropertyChanged
    {
        private int count = 10;
        private int finish = 100;
        private int start = 1;
        private bool isChecked = true;

        public List<int> GetList()
        {
            Random rand = new Random();
            var list = new List<int>(count);
            finish++;//finish+1 - это чтобы было включительное значение, по умолчанию не включительно 
            if (finish < start) Swap();/*Для получения рандомного числа нужно обязательно, чтобы start<finish
            и если пользователь ввел данные не в те поля, программа просто поменяет значения и продолжит работу*/

            if (isChecked)
            {
                if (Math.Abs(finish - start) >= count)
                {
                    var nextValue = 0;
                    for (var i = 0; i < count; i++)
                    {
                        nextValue = rand.Next(start, finish);
                        if (list.Contains(nextValue)) i--;
                        else list.Add(nextValue);
                    }
                }
                else MessageBox.Show("В данном диапазоне чисел невозможно получить значения без повторений.");
            }
            else
            {
                for (int i = 0; i < count; i++) list.Add(rand.Next(start, finish));
            }

            return list;
        }
        private void Swap()
        {
            var box = finish;
            finish = start;
            start = box;
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
