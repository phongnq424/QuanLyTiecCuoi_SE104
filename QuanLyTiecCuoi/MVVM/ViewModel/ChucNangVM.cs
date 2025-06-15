using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Core;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class ChucNangVM : BaseViewModel
    {
        public int MaChucNang { get; set; }
        public string TenChucNang { get; set; }
        public string TenManHinhDuocLoad { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged();
                }
            }
        }
    }

}
