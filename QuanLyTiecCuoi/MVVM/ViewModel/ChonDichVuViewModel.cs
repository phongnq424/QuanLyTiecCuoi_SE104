using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi.Core;
using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using QuanLyTiecCuoi;

public class ChonDichVuViewModel : BaseViewModel
{
    private readonly DichVuService _dichVuService;

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

    public RelayCommand<DICHVU> ChonDichVuCommand { get; set; }

    public ChonDichVuViewModel()
    {
        _dichVuService = App.AppHost.Services.GetRequiredService<DichVuService>();
        LoadDanhSachDichVu();

        ChonDichVuCommand = new RelayCommand<DICHVU>(
            param => param != null,
            param => ChonDichVu(param)
        );
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
}
