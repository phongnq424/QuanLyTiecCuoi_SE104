using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace QuanLyTiecCuoi.MVVM.ViewModel.MonAn
{
    public class TuyChinhMonAnViewModel : BaseViewModel
    {
        private readonly MonAnService _monAnService;

        private List<MONAN> _allMonAn = new();
        public ObservableCollection<MONAN> DanhSachMonAn { get; set; } = new();

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

        public TuyChinhMonAnViewModel()
        {
            _monAnService = App.AppHost.Services.GetRequiredService<MonAnService>();
            LoadDanhSachMonAn();
        }

        public void LoadDanhSachMonAn()
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

            if (GiaMin.HasValue)
            {
                ketQua = ketQua.Where(x => x.DonGia >= GiaMin.Value);
            }

            DanhSachMonAn = new ObservableCollection<MONAN>(ketQua);
            OnPropertyChanged(nameof(DanhSachMonAn));
        }

        public void XoaMonAn(MONAN monAn)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                _monAnService.XoaMonAn(monAn);
                LoadDanhSachMonAn();
            }
        }

        public void HienThiChiTiet(MONAN monAn)
        {
            MessageBox.Show($"Tên: {monAn.TenMon}\nGiá: {monAn.DonGia:N0} VND", "Chi tiết món ăn", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void SuaMonAn(MONAN monAn)
        {
            MessageBox.Show($"Mở giao diện chỉnh sửa cho: {monAn.TenMon}", "Sửa món ăn");
        }
    }
}
