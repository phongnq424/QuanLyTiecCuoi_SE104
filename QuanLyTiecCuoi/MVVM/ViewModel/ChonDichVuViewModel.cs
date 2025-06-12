using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi.Core;
using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Linq;
using QuanLyTiecCuoi;

public class ChonDichVuViewModel : BaseViewModel
{
    private readonly DichVuService _dichVuService;
    private readonly ChiTietDichVuService _chiTietDichVuService;

    private List<DICHVU> _allDichVu = new();
    public ObservableCollection<DICHVU> DanhSachDichVu { get; set; } = new();
    public ObservableCollection<DICHVU> DichVuDaChon { get; set; } = new();

    private string _tuKhoaTimTen;
    public string TuKhoaTimTen
    {
        get => _tuKhoaTimTen;
        set
        {
            _tuKhoaTimTen = value;
            OnPropertyChanged();
            ThucHienTimKiem();
        }
    }

    private string _tuKhoaTimGia;
    public string TuKhoaTimGia
    {
        get => _tuKhoaTimGia;
        set
        {
            _tuKhoaTimGia = value;
            OnPropertyChanged();
            ThucHienTimKiem();
        }
    }

    private int _maDatTiec;
    public int MaDatTiec
    {
        get => _maDatTiec;
        set
        {
            _maDatTiec = value;
            OnPropertyChanged();
        }
    }

    public ChonDichVuViewModel()
    {
        _dichVuService = App.AppHost.Services.GetRequiredService<DichVuService>();
        _chiTietDichVuService = App.AppHost.Services.GetRequiredService<ChiTietDichVuService>();
        LoadDanhSachDichVu();
    }

    private void LoadDanhSachDichVu()
    {
        _allDichVu = _dichVuService.GetAllDichVu();
        DanhSachDichVu = new ObservableCollection<DICHVU>(_allDichVu);
        OnPropertyChanged(nameof(DanhSachDichVu));
    }

    private void ThucHienTimKiem()
    {
        IEnumerable<DICHVU> ketQua = _allDichVu;

        if (!string.IsNullOrWhiteSpace(TuKhoaTimTen))
        {
            ketQua = ketQua.Where(x => x.TenDichVu?.IndexOf(TuKhoaTimTen.Trim(), StringComparison.OrdinalIgnoreCase) >= 0);
        }

        if (!string.IsNullOrWhiteSpace(TuKhoaTimGia) && decimal.TryParse(TuKhoaTimGia.Trim(), out decimal gia))
        {
            ketQua = ketQua.Where(x => x.DonGia == gia);
        }

        DanhSachDichVu = new ObservableCollection<DICHVU>(ketQua);
        OnPropertyChanged(nameof(DanhSachDichVu));
    }

    public void ChonDichVu(DICHVU dichVu)
    {
        if (!DichVuDaChon.Contains(dichVu))
        {
            DichVuDaChon.Add(dichVu);
            OnPropertyChanged(nameof(DichVuDaChon));
        }
    }

    public void LuuChiTietDichVu()
    {
        foreach (var dichVu in DichVuDaChon)
        {
            var chiTiet = new CHITIETDVTIEC
            {
                MaDatTiec = _maDatTiec,
                MaDichVu = dichVu.MaDichVu,
                SoLuong = 1 // hoặc cho chọn số lượng nếu có giao diện
            };

            _chiTietDichVuService.ThemChiTiet(chiTiet);
        }

        MessageBox.Show("Đã lưu dịch vụ vào chi tiết đặt tiệc thành công!");
    }
}