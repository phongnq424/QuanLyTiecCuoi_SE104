using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class SanhViewModel : BaseViewModel
    {
        public ObservableCollection<Sanh> DanhSachSanh { get; set; }
        public ObservableCollection<LoaiSanh> DanhSachLoaiSanh { get; set; }

        private Sanh _sanhDangChon;
        public Sanh SanhDangChon
        {
            get => _sanhDangChon;
            set { _sanhDangChon = value; OnPropertyChanged(); }
        }

        public ICommand ThemSanhCommand { get; }

        public SanhViewModel()
        {
            // Database
            DanhSachLoaiSanh = new ObservableCollection<LoaiSanh>
            {
                new LoaiSanh { MaLoaiSanh = 1, TenLoaiSanh = "A", DonGiaBanToiThieu = 1000000},
                new LoaiSanh { MaLoaiSanh = 2, TenLoaiSanh = "B", DonGiaBanToiThieu = 1100000}
            };

            DanhSachSanh = new ObservableCollection<Sanh>
            {
                new Sanh { MaSanh = 1, TenSanh = "Sảnh 1", MaLoaiSanh = 1, SoLuongBanToiDa = 20, GhiChu = "Sảnh nhỏ", LoaiSanh = DanhSachLoaiSanh[0], HinhAnh = "/Resources/sanh1.jpg" },
                new Sanh { MaSanh = 2, TenSanh = "Sảnh 2", MaLoaiSanh = 2, SoLuongBanToiDa = 30, GhiChu = "Sảnh lớn", LoaiSanh = DanhSachLoaiSanh[1], HinhAnh = "/Resources/sanh2.jpg" }
            };

            // Khởi tạo lệnh Thêm Sảnh 
            ThemSanhCommand = new RelayCommand<object>(
                canExecute: _ => true,
                execute: _ => ThemSanh()
            );
        }

        public int SoLuongSanh
        {
            get { return DanhSachSanh.Count; }
        }

        private void ThemSanh()
        {
            // Thêm sảnh mới vào danh sách
            DanhSachSanh.Add(new Sanh
            {
                MaSanh = DanhSachSanh.Count + 1,
                TenSanh = "Sảnh mới",
                MaLoaiSanh = 1,
                SoLuongBanToiDa = 5,
                GhiChu = "Sảnh mới thêm",
                LoaiSanh = DanhSachLoaiSanh[0]
            });
        }
    }
}
