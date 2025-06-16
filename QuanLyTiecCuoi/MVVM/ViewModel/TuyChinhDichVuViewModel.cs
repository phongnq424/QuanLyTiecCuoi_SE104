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

            if (GiaMin.HasValue)
            {
                ketQua = ketQua.Where(x => x.DonGia >= GiaMin.Value);
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
