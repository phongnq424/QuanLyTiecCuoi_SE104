using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace QuanLyTiecCuoi.MVVM.ViewModel.DichVu
{
    public class TuyChinhDichVuViewModel : BaseViewModel
    {
        private readonly DichVuService _dichVuService;

        private List<DICHVU> _allDichVu = new();
        public ObservableCollection<DICHVU> DanhSachDichVu { get; set; } = new();

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

        public TuyChinhDichVuViewModel()
        {
            _dichVuService = App.AppHost.Services.GetRequiredService<DichVuService>();
            LoadDanhSachDichVu();
        }

        public void LoadDanhSachDichVu()
        {
            _allDichVu = _dichVuService.GetAllDichVu();
            DanhSachDichVu = new ObservableCollection<DICHVU>(_allDichVu);
            OnPropertyChanged(nameof(DanhSachDichVu));
        }

        private void ThucHienTimKiem()
        {
            IEnumerable<DICHVU> ketQua = _allDichVu;

            if (!string.IsNullOrWhiteSpace(TuKhoaTen))
            {
                ketQua = ketQua.Where(x => x.TenDichVu?.IndexOf(TuKhoaTen.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (!string.IsNullOrWhiteSpace(TuKhoaGia) && decimal.TryParse(TuKhoaGia.Trim(), out decimal gia))
            {
                ketQua = ketQua.Where(x => x.DonGia == gia);
            }

            DanhSachDichVu = new ObservableCollection<DICHVU>(ketQua);
            OnPropertyChanged(nameof(DanhSachDichVu));
        }

        public void XoaDichVu(DICHVU dv)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa dịch vụ này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _dichVuService.XoaDichVu(dv);
                LoadDanhSachDichVu();
            }
        }
    }
}
