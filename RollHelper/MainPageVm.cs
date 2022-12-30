using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace RollHelper
{
    public class MainPageVm : INotifyPropertyChanged
    {

        private int _cubeCount = 0;

        public int CubeCount
        {
            get => _cubeCount;
            set
            {
                _cubeCount = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CubeModel> _oldRolls;

        public ObservableCollection<CubeModel> OldRolls
        {
            get => _oldRolls;
            set
            {
                _oldRolls = value;
                OnPropertyChanged();
            }
        }

        public MainPageVm()
        {
            OldRolls = new ObservableCollection<CubeModel>();

            RollCommand = new RelayCommand(Roll);
        }


        public ICommand RollCommand { get; private set; }

        private Random rand = new Random();

        private void Roll(object obj)
        {

            List<int> cubes = new List<int>();

            for (int i = 0; i < CubeCount; i++)
            {
                cubes.Add(rand.Next(1, 11));
            }

            OldRolls.Insert(0, new CubeModel
            {
                Cubes = cubes.ToArray()
            });

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }


    public class CubeModel
    {
        public int[] Cubes { get; set; }
    }


    public class RollConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int[] cubes = (int[]) value;

            string answer = "";

            foreach (var i in cubes.OrderByDescending(x=>x))
            {
                answer += $"{i} ";
            }

            return answer;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
