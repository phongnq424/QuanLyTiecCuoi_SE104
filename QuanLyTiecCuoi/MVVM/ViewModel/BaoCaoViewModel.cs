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

public class BaoCaoViewModel : BaseViewModel
{
    private readonly BaoCaoService _baoCaoService;

    public ObservableCollection<string> MonthYearOptions { get; set; } = new();
    public ObservableCollection<string> MonthYearOptionsToDate { get; set; } = new();
    public ISeries[] Series { get; set; }
    public Axis[] XAxes { get; set; }

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

        NavigateCommand = new RelayCommand<object>(_ => true, parameter => NavigateToDetailPage(parameter));

        // Lấy danh sách tháng/năm có trong DB
        var availableMonths = _baoCaoService.GetAvailableMonthsAndYears();

        foreach (var item in availableMonths)
        {
            var formatted = $"{item.Thang:00}/{item.Nam}";
            MonthYearOptions.Add(formatted);
        }

        if (MonthYearOptions.Count > 0)
            SelectedTuNgay = MonthYearOptions.First();

        UpdateMonthYearOptionsToDate();
    }

    private void UpdateMonthYearOptionsToDate()
    {
        if (string.IsNullOrEmpty(SelectedTuNgay)) return;

        var start = DateTime.ParseExact("01/" + SelectedTuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        MonthYearOptionsToDate.Clear();

        foreach (var item in MonthYearOptions)
        {
            var itemDate = DateTime.ParseExact("01/" + item, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (itemDate >= start)
                MonthYearOptionsToDate.Add(item);
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

        var reports = _baoCaoService.GetBaoCaoTheoBoLoc(tuNgay: tu, denNgay: den);

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
        var vm = Application.Current.MainWindow.DataContext as MainWindowViewModel;
        if (vm != null)
        {
            var dsBaoCao = App.AppHost.Services.GetRequiredService<DSBaoCao>();
            vm.CurrentView = dsBaoCao;
        }
    }
}
