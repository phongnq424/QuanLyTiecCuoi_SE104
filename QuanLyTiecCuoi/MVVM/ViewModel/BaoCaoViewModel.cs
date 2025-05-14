using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore.Migrations;
using QuanLyTiecCuoi;
using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.MVVM.View.BaoCao;
using QuanLyTiecCuoi.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows;

public class BaoCaoViewModel : BaseViewModel
{
    public ObservableCollection<string> MonthYearOptions { get; set; }
    public ObservableCollection<string> MonthYearOptionsToDate { get; set; }
    public SeriesCollection SeriesCollection { get; set; }
    public List<string> Labels { get; set; }

    private string _selectedTuNgay;
    public RelayCommand<object> NavigateCommand { get; private set; }
    private bool CanNavigateToDetailPage(object parameter)
    {
        return true; // Điều kiện kiểm tra có thể cho phép luôn thực thi lệnh
    }
    private void NavigateToDetailPage(object parameter)
    {
        var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
        if (mainFrame != null)
        {
            mainFrame.Navigate(new ChiTietBaoCaoPage()); // Chuyển trang
        }
    }
    public string SelectedTuNgay
    {
        get => _selectedTuNgay;
        set
        {
            _selectedTuNgay = value;
            OnPropertyChanged(nameof(SelectedTuNgay));
            UpdateMonthYearOptionsToDate(); // Cập nhật lại các tháng Đến ngày khi Từ ngày thay đổi
            LoadChartData(); // Load lại dữ liệu khi Từ ngày thay đổi
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
            LoadChartData(); // Load lại dữ liệu khi Đến ngày thay đổi
        }
    }

    public BaoCaoViewModel()
    {
        NavigateCommand = new RelayCommand<object>(CanNavigateToDetailPage, NavigateToDetailPage);
        MonthYearOptions = new ObservableCollection<string>();
        MonthYearOptionsToDate = new ObservableCollection<string>();

        var start = new DateTime(2023, 1, 1);
        var end = DateTime.Now;
        


        // Thêm tất cả các tháng từ 01/2023 đến hiện tại vào MonthYearOptions
        while (start <= end)
        {
            MonthYearOptions.Add(start.ToString("MM/yyyy"));
            start = start.AddMonths(1);
        }

        if (MonthYearOptions.Count >= 1)
        {
            SelectedTuNgay = MonthYearOptions[0]; // Mặc định chọn tháng đầu tiên
        }

        // Cập nhật MonthYearOptionsToDate cho ComboBox Đến ngày khi khởi tạo
        UpdateMonthYearOptionsToDate();
    }

    // Cập nhật danh sách các tháng từ Từ ngày đến Hiện tại cho ComboBox Đến ngày
    private void UpdateMonthYearOptionsToDate()
    {
        var start = DateTime.ParseExact("01/" + SelectedTuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        var end = DateTime.Now;

        MonthYearOptionsToDate.Clear();

        while (start <= end)
        {
            MonthYearOptionsToDate.Add(start.ToString("MM/yyyy"));
            start = start.AddMonths(1);
        }

        // Nếu SelectedDenNgay không nằm trong danh sách này, mặc định chọn tháng cuối cùng
        if (!MonthYearOptionsToDate.Contains(SelectedDenNgay))
        {
            SelectedDenNgay = MonthYearOptionsToDate.LastOrDefault();
        }
    }

    public void LoadChartData()
    {
        if (string.IsNullOrEmpty(SelectedTuNgay) || string.IsNullOrEmpty(SelectedDenNgay))
            return;

        // Lấy từ ngày
        var tu = DateTime.ParseExact("01/" + SelectedTuNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        // Lấy đến ngày là ngày hiện tại
        var den = DateTime.ParseExact("01/" + SelectedDenNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        if (tu > den)
            return;

        var labels = new List<string>();
        var weddingCounts = new ChartValues<int>();
        var revenues = new ChartValues<double>();

        var rand = new Random();
        while (tu <= den)
        {
            labels.Add(tu.ToString("MM/yyyy"));

            // Random values for wedding count and revenue
            weddingCounts.Add(rand.Next(5, 15)); // Thay thế bằng dữ liệu thực tế khi có
            revenues.Add(rand.Next(10, 40)); // Thay thế bằng dữ liệu thực tế khi có

            tu = tu.AddMonths(1);
        }

        Labels = labels;
        OnPropertyChanged(nameof(Labels));

        SeriesCollection = new SeriesCollection
        {
            new ColumnSeries
            {
                Title = "Số tiệc cưới",
                Values = weddingCounts,
                ColumnPadding = 2
            },
            new ColumnSeries
            {
                Title = "Doanh thu (triệu VNĐ)",
                Values = revenues,
                ColumnPadding = 2
            }
        };
        OnPropertyChanged(nameof(SeriesCollection));
    }
}
