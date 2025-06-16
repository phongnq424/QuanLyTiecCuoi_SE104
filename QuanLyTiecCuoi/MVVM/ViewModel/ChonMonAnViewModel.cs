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
        private readonly ChiTietMenuService _chiTietMenuService;
        private readonly DATTIEC _datTiec;

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
                if (decimal.TryParse(value, out decimal gia))
                {
                    GiaMin = gia;
                }
                else
                {
                    GiaMin = null;
                }
                ThucHienTimKiem();
            }
        }

        private decimal? _giaMin;
        public decimal? GiaMin
        {
            get => _giaMin;
            set
            {
                _giaMin = value;
                OnPropertyChanged();
                ThucHienTimKiem();
            }
        }

        public ChonMonAnViewModel(DATTIEC datTiec, ObservableCollection<MONAN> monAnDaChon)
        {
            _datTiec = datTiec;
            _monAnService = App.AppHost.Services.GetRequiredService<MonAnService>();
            _chiTietMenuService = App.AppHost.Services.GetRequiredService<ChiTietMenuService>();

            MonAnDaChon = monAnDaChon ?? new ObservableCollection<MONAN>();
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

            if (GiaMin.HasValue)
            {
                ketQua = ketQua.Where(x => x.DonGia >= GiaMin.Value);
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

        public void LuuChiTietMenu()
        {
            if (_datTiec == null || _datTiec.MaDatTiec == 0)
            {
                return;
            }

            foreach (var monAn in MonAnDaChon)
            {
                var chiTiet = new CHITIETMENU
                {
                    MaDatTiec = _datTiec.MaDatTiec,
                    MaMon = monAn.MaMon,
                    SoLuong = 1,
                    GhiChu = ""
                };

                _chiTietMenuService.ThemChiTietMenu(chiTiet);
            }

            MessageBox.Show("Lưu thực đơn thành công!");
        }
    }
}
