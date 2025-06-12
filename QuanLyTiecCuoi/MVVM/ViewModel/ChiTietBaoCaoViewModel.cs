using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi;
using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.MVVM.View.BaoCao;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

public class ChiTietBaoCaoViewModel : BaseViewModel
{
    private readonly ChiTietBaoCaoService _chiTietBaoCaoService;

    public RelayCommand<object> ReturnCommand { get; private set; }

    private bool CanNavigateToReportPage(object parameter) => true;

    private void NavigateToReportPage(object parameter)
    {
        var vm = Application.Current.MainWindow.DataContext as MainWindowViewModel;
        if (vm != null)
        {
            var reportPage = App.AppHost.Services.GetRequiredService<BaoCaoPage>();
            vm.CurrentView = reportPage;
        }
    }




    private ObservableCollection<ChiTietBaoCaoModel> _chiTietBaoCaoList;
    public ObservableCollection<ChiTietBaoCaoModel> ChiTietBaoCaoList
    {
        get => _chiTietBaoCaoList;
        set => SetProperty(ref _chiTietBaoCaoList, value);
    }

    public ObservableCollection<int> ThangList { get; set; }
    public ObservableCollection<int> NamList { get; set; }

    private int _selectedThang;
    public int SelectedThang
    {
        get => _selectedThang;
        set
        {
            if (SetProperty(ref _selectedThang, value))
                TryLoadBaoCao();
        }
    }

    private int _selectedNam;
    public int SelectedNam
    {
        get => _selectedNam;
        set
        {
            if (SetProperty(ref _selectedNam, value))
                TryLoadBaoCao();
        }
    }

    private string _tongDoanhThuFormatted;
    public string TongDoanhThuFormatted
    {
        get => _tongDoanhThuFormatted;
        set => SetProperty(ref _tongDoanhThuFormatted, value);
    }

    private int _tongTiecCuoi;
    public int TongTiecCuoi
    {
        get => _tongTiecCuoi;
        set => SetProperty(ref _tongTiecCuoi, value);
    }

    public ChiTietBaoCaoViewModel(ChiTietBaoCaoService chiTietBaoCaoService)
    {
        _chiTietBaoCaoService = chiTietBaoCaoService;
        ReturnCommand = new RelayCommand<object>(CanNavigateToReportPage, NavigateToReportPage);
        ThangList = new ObservableCollection<int>(Enumerable.Range(1, 12));
        NamList = new ObservableCollection<int> { 2023, 2024, 2025 };

        SelectedThang = DateTime.Now.Month;
        SelectedNam = DateTime.Now.Year;

        LoadBaoCao();
    }

    private void TryLoadBaoCao()
    {
        if (SelectedThang > 0 && SelectedNam > 0)
            LoadBaoCao();
    }

    private void LoadBaoCao()
    {
        var data = _chiTietBaoCaoService.GetChiTietBaoCaoByMonthYear(SelectedThang, SelectedNam);

        TongTiecCuoi = data.Sum(x => x.SoLuongTiecCuoi);
        double tongDoanhThu = data.Sum(x => x.DoanhThu);
        TongDoanhThuFormatted = tongDoanhThu.ToString("#,0.##") + " VND";

        ChiTietBaoCaoList = new ObservableCollection<ChiTietBaoCaoModel>(data);
    }
}
