using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Linq;

namespace QuanLyTiecCuoi.MVVM.ViewModel.MonAn
{
    public class ChonMonAnViewModel : BaseViewModel
    {
        private readonly MonAnService _monAnService;

        private List<MONAN> _allMonAn = new();
        public ObservableCollection<MONAN> DanhSachMonAn { get; set; } = new();
        public ObservableCollection<MONAN> MonAnDaChon { get; set; } = new();

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
            OnPropertyChanged(nameof(DanhSachMonAn));
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
            OnPropertyChanged(nameof(DanhSachMonAn));
        }

        public void ChonMonAn(MONAN monAn)
        {
            if (!MonAnDaChon.Contains(monAn))
            {
                MonAnDaChon.Add(monAn);
                OnPropertyChanged(nameof(MonAnDaChon));
            }
        }
    }
}
