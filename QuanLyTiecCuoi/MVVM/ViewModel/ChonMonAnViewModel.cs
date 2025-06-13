using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace QuanLyTiecCuoi.MVVM.ViewModel.MonAn
{
    public class ChonMonAnViewModel : BaseViewModel
    {
        private readonly MonAnService _monAnService;

        private List<MONAN> _allMonAn = new();

        private ObservableCollection<MONAN> _danhSachMonAn;
        public ObservableCollection<MONAN> DanhSachMonAn
        {
            get => _danhSachMonAn;
            set
            {
                _danhSachMonAn = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MONAN> _monAnDaChon = new();
        public ObservableCollection<MONAN> MonAnDaChon
        {
            get => _monAnDaChon;
            set
            {
                _monAnDaChon = value;
                OnPropertyChanged();
            }
        }

        private string _tuKhoaTen;
        public string TuKhoaTen
        {
            get => _tuKhoaTen;
            set
            {
                _tuKhoaTen = value;
                OnPropertyChanged();
                ThucHienTimKiem();
            }
        }

        private string _tuKhoaGia;
        public string TuKhoaGia
        {
            get => _tuKhoaGia;
            set
            {
                _tuKhoaGia = value;
                OnPropertyChanged();
                ThucHienTimKiem();
            }
        }

        public ChonMonAnViewModel()
        {
            _monAnService = App.AppHost.Services.GetRequiredService<MonAnService>();
            LoadDanhSachMonAn();
        }

        private void LoadDanhSachMonAn()
        {
            _allMonAn = _monAnService.GetAllMonAn();
            DanhSachMonAn = new ObservableCollection<MONAN>(_allMonAn);
        }

        private void ThucHienTimKiem()
        {
            IEnumerable<MONAN> ketQua = _allMonAn;

            if (!string.IsNullOrWhiteSpace(TuKhoaTen))
            {
                ketQua = ketQua.Where(x => x.TenMon?.IndexOf(TuKhoaTen.Trim(), System.StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrWhiteSpace(TuKhoaGia) && decimal.TryParse(TuKhoaGia.Trim(), out decimal gia))
            {
                ketQua = ketQua.Where(x => x.DonGia == gia);
            }

            DanhSachMonAn = new ObservableCollection<MONAN>(ketQua);
        }

        public void ChonMonAn(MONAN monAn)
        {
            if (!MonAnDaChon.Contains(monAn))
            {
                MonAnDaChon.Add(monAn);
            }
        }

        private int _maDatTiec;
        private readonly ChiTietMenuService _chiTietMenuService;

        public void LuuChiTietMenu()
        {
            foreach (var monAn in MonAnDaChon)
            {
                var chiTiet = new CHITIETMENU
                {
                    MaDatTiec = _maDatTiec,
                    MaMon = monAn.MaMon,
                    SoLuong = 1,
                    GhiChu = "" // hoặc để null nếu không cần
                };

                _chiTietMenuService.ThemChiTietMenu(chiTiet);
            }

            MessageBox.Show("Đã lưu món ăn vào thực đơn thành công!");
        }
    }
}
