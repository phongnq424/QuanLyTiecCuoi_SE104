using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class ThemTiecViewModel : INotifyPropertyChanged
    {
        //public DatTiecServices TiecMoi { get; set; } = new DatTiecServices();
        //public ICommand LuuCommand { get; set; }

        //private readonly IDatTiecService _service;

        //public ThemTiecViewModel(IDatTiecService service)
        //{
        //    _service = service;
        //    LuuCommand = new RelayCommand(ThemTiec);
        //}

        //private void ThemTiec()
        //{
        //    try
        //    {
        //        _service.Add(TiecMoi);
        //        MessageBox.Show("Lưu tiệc thành công!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi: {ex.Message}");
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
