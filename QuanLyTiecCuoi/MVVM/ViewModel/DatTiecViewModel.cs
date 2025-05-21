using QuanLyTiecCuoi.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi.MVVM.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class DatTiecViewModel : INotifyPropertyChanged
    {
        private readonly IDatTiecService _datTiecService;

        public ObservableCollection<DatTiecServices> DanhSachDatTiec { get; set; } = new ObservableCollection<DatTiecServices>();

        public DatTiecViewModel(IDatTiecService datTiecService)
        {
            _datTiecService = datTiecService;
            //LoadData();
        }

        //private void LoadData()
        //{
        //    var list = _datTiecService.GetAll();
        //    DanhSachDatTiec.Clear();

        //    foreach (var item in list)
        //    {
        //        DanhSachDatTiec.Add(item);
        //    }
                
        //}

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
