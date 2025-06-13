using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class AddOrEditCaViewModel : BaseViewModel
    {
        public CASANH CaInfo { get; set; } = new();


        public string GioBatDauText
        {
            get => CaInfo.GioBatDau.ToString("HH:mm");
            set
            {
                if (TimeSpan.TryParse(value, out var bd))
                    CaInfo.GioBatDau = DateTime.Today.Add(bd);
                OnPropertyChanged();
            }
        }

        public string GioKetThucText
        {
            get => CaInfo.GioKetThuc.ToString("HH:mm");
            set
            {
                if (TimeSpan.TryParse(value, out var kt))
                    CaInfo.GioKetThuc = DateTime.Today.Add(kt);
                OnPropertyChanged();
            }
        }

        public ICommand LuuCommand { get; }

        public AddOrEditCaViewModel()
        {
            LuuCommand = new RelayCommand<object>(_ => true, OnLuu);
        }

        public event Action<CASANH> LuuThanhCong;

        private void OnLuu(object _)
        {
            if (string.IsNullOrWhiteSpace(CaInfo.TenCa))
            {
                MessageBox.Show("Tên ca không được để trống");
                return;
            }

            LuuThanhCong?.Invoke(CaInfo); // báo về View
        }
    }

}
