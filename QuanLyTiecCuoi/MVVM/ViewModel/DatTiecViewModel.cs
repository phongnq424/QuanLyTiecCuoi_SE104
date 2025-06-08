using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.DatTiec;
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.Core;
using System.Windows;
public class DatTiecViewModel : BaseViewModel
{
    private readonly DatTiecService _datTiecService;

    private List<DATTIEC> _allDatTiec = new();
    public ObservableCollection<DATTIEC> DanhSachDatTiec { get; set; } = new();

    public ObservableCollection<string> DanhSachTieuChi { get; set; } = new()
    {
        "Tên cô dâu", "Tên chú rể", "Số điện thoại", "Ngày đãi"
    };

    private string _tieuChiDangChon = "Tên cô dâu";
    public string TieuChiDangChon
    {
        get => _tieuChiDangChon;
        set
        {
            _tieuChiDangChon = value;
            OnPropertyChanged();
            ThucHienTimKiem();
        }
    }

    private string _tuKhoaTimKiem;
    public string TuKhoaTimKiem
    {
        get => _tuKhoaTimKiem;
        set
        {
            _tuKhoaTimKiem = value;
            OnPropertyChanged();
            ThucHienTimKiem();
        }
    }

    public RelayCommand<object> NavigateCommand { get; private set; }

    public DatTiecViewModel(DatTiecService datTiecService)
    {
        _datTiecService = datTiecService;
        NavigateCommand = new RelayCommand<object>(_ => true, NavigateToDatTiecPage);
        LoadDanhSachDatTiec();
    }

    public DatTiecViewModel()
    {
        _datTiecService = App.AppHost.Services.GetRequiredService<DatTiecService>();
        NavigateCommand = new RelayCommand<object>(_ => true, NavigateToDatTiecPage);
        LoadDanhSachDatTiec();
    }

    public void LoadDanhSachDatTiec()
    {
        _allDatTiec = _datTiecService.GetAllDatTiec();
        DanhSachDatTiec = new ObservableCollection<DATTIEC>(_allDatTiec);
        OnPropertyChanged(nameof(DanhSachDatTiec));
    }

    private void ThucHienTimKiem()
    {
        if (string.IsNullOrWhiteSpace(TuKhoaTimKiem))
        {
            DanhSachDatTiec = new ObservableCollection<DATTIEC>(_allDatTiec);
            OnPropertyChanged(nameof(DanhSachDatTiec));
            return;
        }

        var keyword = TuKhoaTimKiem.Trim();
        IEnumerable<DATTIEC> ketQua = _allDatTiec;

        switch (TieuChiDangChon)
        {
            case "Tên cô dâu":
                ketQua = _allDatTiec.Where(x => x.TenCoDau?.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
                break;
            case "Tên chú rể":
                ketQua = _allDatTiec.Where(x => x.TenChuRe?.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0);
                break;
            case "Số điện thoại":
                ketQua = _allDatTiec.Where(x => x.SDT?.Contains(keyword) == true);
                break;
            case "Ngày đãi":
                ketQua = _allDatTiec.Where(x => x.NgayDaiTiec.ToString("dd/MM/yyyy").Contains(keyword));
                break;
        }

        DanhSachDatTiec = new ObservableCollection<DATTIEC>(ketQua);
        OnPropertyChanged(nameof(DanhSachDatTiec));
    }

    private void NavigateToDatTiecPage(object parameter)
    {
        var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
        if (mainFrame != null)
        {
            var datTiecPage = App.AppHost.Services.GetRequiredService<DatTiecView>();
            mainFrame.Navigate(datTiecPage);
        }
    }
    private void NavigateToSuaTiecPage(object parameter)
    {
        var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
        if (mainFrame != null)
        {
            var suaTiecPage = App.AppHost.Services.GetRequiredService<SuaTiecView>();
            mainFrame.Navigate(suaTiecPage);
        }
    }
    public void LocTheoNgay(DateTime ngay)
    {
        DanhSachDatTiec = new ObservableCollection<DATTIEC>(
            _allDatTiec.Where(x => x.NgayDaiTiec.Date == ngay.Date));
        OnPropertyChanged(nameof(DanhSachDatTiec));
    }
}
