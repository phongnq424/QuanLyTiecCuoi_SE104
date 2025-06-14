using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.MVVM.Model
{
    public class LoaiSanh : INotifyPropertyChanged
    {
        private string _tenLoaiSanh;
        private decimal? _donGiaBanToiThieu;

        public int MaLoaiSanh { get; set; }

        public string TenLoaiSanh
        {
            get => _tenLoaiSanh;
            set
            {
                if (_tenLoaiSanh != value)
                {
                    _tenLoaiSanh = value;
                    OnPropertyChanged(nameof(TenLoaiSanh));
                }
            }
        }

        public decimal? DonGiaBanToiThieu
        {
            get => _donGiaBanToiThieu;
            set
            {
                if (_donGiaBanToiThieu != value)
                {
                    _donGiaBanToiThieu = value;
                    OnPropertyChanged(nameof(DonGiaBanToiThieu));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
