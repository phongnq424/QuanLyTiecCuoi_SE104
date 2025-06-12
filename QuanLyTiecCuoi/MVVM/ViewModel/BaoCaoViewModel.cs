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
using System.Windows.Controls;

public class BaoCaoViewModel : BaseViewModel
{
    private readonly BaoCaoService _baoCaoService;

    public ObservableCollection<string> MonthYearOptions { get; set; } = new();
    public ObservableCollection<string> MonthYearOptionsToDate { get; set; } = new();
    public ISeries[] Series { get; set; } // Thay SeriesCollection bằng ISeries[]
    public Axis[] XAxes { get; set; } // Thay Labels bằng Axis[]

    public RelayCommand<object> NavigateCommand { get; private set; }

    private string _selectedTuNgay;
    public string SelectedTuNgay
    {
        get => _selectedTuNgay;
        set
        {
            _selectedTuNgay = value;
            OnPropertyChanged(nameof(SelectedTuNgay));
            UpdateMonthYearOptionsToDate();
            LoadChartData();
        }
    }

    private string _selectedDenNgay;
    public string SelectedDenNgay
    {
        get => _selectedDenNgay;
        set
        {
            _selectedDenNgay = value;
            OnPropertyChanged(nameof(SelectedDenNgay));
            LoadChartData();
        }
    }

    public BaoCaoViewModel(BaoCaoService baoCaoService)
    {
        _baoCaoService = baoCaoService;

        NavigateCommand = new RelayCommand<object>(_ => true, NavigateToDetailPage);

        var start = new DateTime(2023, 1, 1);
        var end = DateTime.Now;

        while (start <= end)
        {
            MonthYearOptions.Add(start.ToString("MM/yyyy"));
            start = start.AddMonths(1);
        }

        if (MonthYearOptions.Count > 0)
            SelectedTuNgay = MonthYearOptions[0];

        UpdateMonthYearOptionsToDate();
    }

    private void UpdateMonthYearOptionsToDate()
    {
        if (string.IsNullOrEmpty(SelectedTuNgay)) return;

        var start = DateTime.ParseExact("01/" + SelectedTuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        var end = DateTime.Now;

        MonthYearOptionsToDate.Clear();
        while (start <= end)
        {
            MonthYearOptionsToDate.Add(start.ToString("MM/yyyy"));
            start = start.AddMonths(1);
        }

        if (!MonthYearOptionsToDate.Contains(SelectedDenNgay))
            SelectedDenNgay = MonthYearOptionsToDate.LastOrDefault();
    }

    public void LoadChartData()
    {
        if (string.IsNullOrEmpty(SelectedTuNgay) || string.IsNullOrEmpty(SelectedDenNgay))
            return;

        var tu = DateTime.ParseExact("01/" + SelectedTuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        var den = DateTime.ParseExact("01/" + SelectedDenNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        if (tu > den) return;

        var reports = _baoCaoService.GetBaoCaoTuNgayDenNgay(tu, den);

        var labels = new List<string>();
        var weddingCounts = new List<int>();
        var revenues = new List<double>();

        foreach (var r in reports)
        {
            labels.Add($"{r.Thang:00}/{r.Nam}");
            weddingCounts.Add(r.TongTiecCuoi);
            double doanhThuTrieuDong = Math.Round((double)r.TongDoanhThu / 1_000_000, 2);
            revenues.Add(doanhThuTrieuDong);
        }

        // Cấu hình Series cho biểu đồ
        Series = new ISeries[]
        {
            new ColumnSeries<int>
            {
                Name = "Số tiệc cưới",
                Values = weddingCounts,
                Stroke = null,
                Fill = new SolidColorPaint(SKColors.Blue)
            },
            new ColumnSeries<double>
            {
                Name = "Doanh thu (triệu VNĐ)",
                Values = revenues,
                Stroke = null,
                Fill = new SolidColorPaint(SKColors.Green)
            }
        };

        // Cấu hình trục X
        XAxes = new Axis[]
        {
            new Axis
            {
                Labels = labels,
                LabelsRotation = 45,
                TextSize = 12
            }
        };

        OnPropertyChanged(nameof(Series));
        OnPropertyChanged(nameof(XAxes));
    }

    private void NavigateToDetailPage(object parameter)
    {
        var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
        if (mainFrame != null)
        {
            var chiTietPage = App.AppHost.Services.GetRequiredService<ChiTietBaoCaoPage>();
            mainFrame.Navigate(chiTietPage);
        }
    }
}