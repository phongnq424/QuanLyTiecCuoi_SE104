using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuanLyTiecCuoi.Core;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel
    {
        #region Command
        public ICommand FirstLoadCM { get; set; }
        public ICommand DieuHuongCommand { get; set; }

        #endregion

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _viewModelList;
        public ObservableCollection<string> ViewModelList
        {
            get { return _viewModelList; }
            set
            {
                _viewModelList = value;
                OnPropertyChanged();
            }
        }
        public MainWindowViewModel()
        {
            FirstLoadCM = new RelayCommand<Window>((p) => { return true; }, (p) => {
            });
        }


    }
}
