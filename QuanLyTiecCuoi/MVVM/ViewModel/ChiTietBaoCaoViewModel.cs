using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi;
using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.MVVM.View.BaoCao;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;

public class ChiTietBaoCaoViewModel : BaseViewModel
{
    private readonly ChiTietBaoCaoService _chiTietBaoCaoService;
    private readonly BaoCaoService _baoCaoService;


    public RelayCommand<object> ReturnCommand { get; private set; }
    public RelayCommand<object> PrintCommand { get; private set; }

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

    public ChiTietBaoCaoViewModel(ChiTietBaoCaoService chiTietBaoCaoService, BaoCaoService baoCaoService, int thang, int nam)
    {
        _chiTietBaoCaoService = chiTietBaoCaoService;
        _baoCaoService = baoCaoService;
        ReturnCommand = new RelayCommand<object>(_ => true, NavigateToReportPage);
        PrintCommand = new RelayCommand<object>(_ => true, OnPrint);
        ThangList = new ObservableCollection<int>();
        NamList = new ObservableCollection<int>();

        LoadThangNamOptions();

        SelectedThang = thang;
        SelectedNam = nam;

        LoadBaoCao(thang, nam);
    }

    private void LoadThangNamOptions()
    {
        var options = _baoCaoService.GetAvailableMonthsAndYears();

        ThangList.Clear();
        NamList.Clear();

        foreach (var opt in options)
        {
            if (!ThangList.Contains(opt.Thang))
                ThangList.Add(opt.Thang);

            if (!NamList.Contains(opt.Nam))
                NamList.Add(opt.Nam);
        }
    }


    private void TryLoadBaoCao()
    {
        if (SelectedThang > 0 && SelectedNam > 0)
            LoadBaoCao(SelectedThang, SelectedNam);
    }

    private void LoadBaoCao(int thang, int nam)
    {
        var data = _chiTietBaoCaoService.GetChiTietBaoCaoByMonthYear(thang, nam);

        TongTiecCuoi = data.Sum(x => x.SoLuongTiecCuoi);
        double tongDoanhThu = data.Sum(x => x.DoanhThu);
        TongDoanhThuFormatted = tongDoanhThu.ToString("#,0.##") + " VND";

        ChiTietBaoCaoList = new ObservableCollection<ChiTietBaoCaoModel>(data);
    }

    private void NavigateToReportPage(object parameter)
    {
        var vm = Application.Current.MainWindow.DataContext as MainWindowViewModel;
        if (vm != null)
        {
            var reportPage = App.AppHost.Services.GetRequiredService<DSBaoCao>();
            vm.CurrentView = reportPage;
        }
    }
    private void OnPrint(object parameter)
    {
        try
        {
            double tongDoanhThu = ChiTietBaoCaoList.Sum(x => x.DoanhThu);
            int tongTiecCuoi = ChiTietBaoCaoList.Sum(x => x.SoLuongTiecCuoi);

            _chiTietBaoCaoService.PrintBaoCao(
                ChiTietBaoCaoList.ToList(),
                SelectedThang,
                SelectedNam,
                tongDoanhThu,
                tongTiecCuoi
            );
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi khi in báo cáo: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

}
