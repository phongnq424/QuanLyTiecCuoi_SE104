using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace QuanLyTiecCuoi.MVVM.ViewModel.DichVu
{
    public class ChonDichVuViewModel : BaseViewModel
    {
        private readonly DichVuService _dichVuService;
        private readonly ChiTietDichVuService _chiTietDichVuService;
        private readonly DATTIEC _datTiec;

        private List<DICHVU> _allDichVu = new();

        private ObservableCollection<DICHVU> _danhSachDichVu;
        public ObservableCollection<DICHVU> DanhSachDichVu
        {
            get => _danhSachDichVu;
            set
            {
                _danhSachDichVu = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DICHVU> _dichVuDaChon = new();
        public ObservableCollection<DICHVU> DichVuDaChon
        {
            get => _dichVuDaChon;
            set
            {
                _dichVuDaChon = value;
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

        public ChonDichVuViewModel(DATTIEC datTiec, ObservableCollection<DICHVU> dichVuDaChon)
        {
            _datTiec = datTiec;
            _dichVuService = App.AppHost.Services.GetRequiredService<DichVuService>();
            _chiTietDichVuService = App.AppHost.Services.GetRequiredService<ChiTietDichVuService>();
            _dichVuDaChon = dichVuDaChon;
          //  DichVuDaChon = dichVuDaChon ?? new ObservableCollection<DICHVU>();
            LoadDanhSachDichVu();
        }

        private void LoadDanhSachDichVu()
        {
            _allDichVu = _dichVuService.GetAllDichVu();
            DanhSachDichVu = new ObservableCollection<DICHVU>(_allDichVu);
        }

        private void ThucHienTimKiem()
        {
            IEnumerable<DICHVU> ketQua = _allDichVu;

            if (!string.IsNullOrWhiteSpace(TuKhoaTen))
            {
                ketQua = ketQua.Where(x => x.TenDichVu?.IndexOf(TuKhoaTen.Trim(), System.StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (GiaMin.HasValue)
            {
                ketQua = ketQua.Where(x => x.DonGia >= GiaMin.Value);
            }

            DanhSachDichVu = new ObservableCollection<DICHVU>(ketQua);
        }

        public void ChonDichVu(DICHVU dichVu)
        {
            if (!DichVuDaChon.Contains(dichVu))
            {
                DichVuDaChon.Add(dichVu);
            }
        }

        public void LuuChiTietDichVu()
        {
            if (_datTiec == null || _datTiec.MaDatTiec <= 0)
                return;

            foreach (var dichVu in DichVuDaChon)
            {
                var chiTiet = new CHITIETDVTIEC
                {
                    MaDatTiec = _datTiec.MaDatTiec,
                    MaDichVu = dichVu.MaDichVu,
                    SoLuong = 1, // hoặc bạn cho người dùng chọn số lượng
                    DonGia = dichVu.DonGia,
                    // ThanhTien = DonGia * SoLuong => sẽ được tính tự động nếu bạn có property readonly
                };

                _chiTietDichVuService.ThemChiTiet(chiTiet);
            }
        }
    }
}