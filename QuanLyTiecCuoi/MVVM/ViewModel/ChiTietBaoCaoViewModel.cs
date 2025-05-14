using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.MVVM.View.BaoCao;
using QuanLyTiecCuoi.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

public class ChiTietBaoCaoViewModel : BaseViewModel
{
    public RelayCommand<object> ReturnCommand { get; private set; }
    private bool CanNavigateToDetailPage(object parameter)
    {
        return true;
    }
    private void NavigateToReportPage(object parameter)
    {
        var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
        if (mainFrame != null)
        {
            mainFrame.Navigate(new BaoCaoPage()); // Chuyển trang
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
            {
                TryLoadBaoCao();
            }
        }
    }

    private int _selectedNam;
    public int SelectedNam
    {
        get => _selectedNam;
        set
        {
            if (SetProperty(ref _selectedNam, value))
            {
                TryLoadBaoCao();
            }
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

    public ChiTietBaoCaoViewModel()
    {
        ReturnCommand = new RelayCommand<object>(CanNavigateToDetailPage, NavigateToReportPage);
        ThangList = new ObservableCollection<int>(Enumerable.Range(1, 12));
        NamList = new ObservableCollection<int> { 2023, 2024, 2025 };

        // Khởi tạo mặc định để tránh null
        SelectedThang = DateTime.Now.Month;
        SelectedNam = DateTime.Now.Year;

        // Gọi lần đầu khi khởi tạo
        LoadBaoCao();
    }

    private void TryLoadBaoCao()
    {
        // Chỉ gọi khi cả hai giá trị đã được chọn (tránh null hoặc 0)
        if (SelectedThang > 0 && SelectedNam > 0)
            LoadBaoCao();
    }

    private void LoadBaoCao()
    {
        var data = GetMockData()
            .Where(x => x.NgayBaoCao.Month == SelectedThang && x.NgayBaoCao.Year == SelectedNam)
            .ToList();
        int index = 1;
        foreach (var item in data)
        {
            item.STT = index++;
        }
        double TongDoanhThu = data.Sum(x => x.DoanhThu);
        TongDoanhThuFormatted = TongDoanhThu.ToString("#,0.##") + " VND";
        TongTiecCuoi = data.Sum(x => x.SoLuongTiecCuoi);
        foreach (var item in data)
        {
            item.DoanhThuFormatted = item.DoanhThu.ToString("#,0.##") + " VND";
        }

        ChiTietBaoCaoList = new ObservableCollection<ChiTietBaoCaoModel>(data);
    }

    private List<ChiTietBaoCaoModel> GetMockData()
    {
        return new List<ChiTietBaoCaoModel>
        {
            new ChiTietBaoCaoModel { STT = 1, NgayBaoCao = new DateTime(2023, 5, 1), SoLuongTiecCuoi = 5, DoanhThu = 100000.68, TiLeDoanhThu = 50, TiLeTiecCuoi = 20 },
            new ChiTietBaoCaoModel { STT = 2, NgayBaoCao = new DateTime(2023, 5, 2), SoLuongTiecCuoi = 3, DoanhThu = 60000, TiLeDoanhThu = 30, TiLeTiecCuoi = 10 },
            new ChiTietBaoCaoModel { STT = 1, NgayBaoCao = new DateTime(2023, 5, 1), SoLuongTiecCuoi = 5, DoanhThu = 100000.68, TiLeDoanhThu = 50, TiLeTiecCuoi = 20 },
            new ChiTietBaoCaoModel { STT = 2, NgayBaoCao = new DateTime(2023, 5, 2), SoLuongTiecCuoi = 3, DoanhThu = 60000, TiLeDoanhThu = 30, TiLeTiecCuoi = 10 },
            new ChiTietBaoCaoModel { STT = 1, NgayBaoCao = new DateTime(2023, 5, 1), SoLuongTiecCuoi = 5, DoanhThu = 100000.68, TiLeDoanhThu = 50, TiLeTiecCuoi = 20 },
            new ChiTietBaoCaoModel { STT = 2, NgayBaoCao = new DateTime(2023, 5, 2), SoLuongTiecCuoi = 3, DoanhThu = 60000, TiLeDoanhThu = 30, TiLeTiecCuoi = 10 },
            new ChiTietBaoCaoModel { STT = 1, NgayBaoCao = new DateTime(2023, 5, 1), SoLuongTiecCuoi = 5, DoanhThu = 100000.68, TiLeDoanhThu = 50, TiLeTiecCuoi = 20 },
            new ChiTietBaoCaoModel { STT = 2, NgayBaoCao = new DateTime(2023, 5, 2), SoLuongTiecCuoi = 3, DoanhThu = 60000, TiLeDoanhThu = 30, TiLeTiecCuoi = 10 },
            new ChiTietBaoCaoModel { STT = 1, NgayBaoCao = new DateTime(2023, 5, 1), SoLuongTiecCuoi = 5, DoanhThu = 100000.68, TiLeDoanhThu = 50, TiLeTiecCuoi = 20 },
            new ChiTietBaoCaoModel { STT = 2, NgayBaoCao = new DateTime(2023, 5, 2), SoLuongTiecCuoi = 3, DoanhThu = 60000, TiLeDoanhThu = 30, TiLeTiecCuoi = 10 },
             new ChiTietBaoCaoModel { STT = 1, NgayBaoCao = new DateTime(2023, 5, 1), SoLuongTiecCuoi = 5, DoanhThu = 100000.68, TiLeDoanhThu = 50, TiLeTiecCuoi = 20 },
            new ChiTietBaoCaoModel { STT = 2, NgayBaoCao = new DateTime(2023, 5, 2), SoLuongTiecCuoi = 3, DoanhThu = 60000, TiLeDoanhThu = 30, TiLeTiecCuoi = 10 },
            new ChiTietBaoCaoModel { STT = 1, NgayBaoCao = new DateTime(2023, 5, 1), SoLuongTiecCuoi = 5, DoanhThu = 100000.68, TiLeDoanhThu = 50, TiLeTiecCuoi = 20 },
            new ChiTietBaoCaoModel { STT = 2, NgayBaoCao = new DateTime(2023, 5, 2), SoLuongTiecCuoi = 3, DoanhThu = 60000, TiLeDoanhThu = 30, TiLeTiecCuoi = 10 },
            new ChiTietBaoCaoModel { STT = 1, NgayBaoCao = new DateTime(2023, 5, 1), SoLuongTiecCuoi = 5, DoanhThu = 100000.68, TiLeDoanhThu = 50, TiLeTiecCuoi = 20 },
            new ChiTietBaoCaoModel { STT = 2, NgayBaoCao = new DateTime(2023, 5, 2), SoLuongTiecCuoi = 3, DoanhThu = 60000, TiLeDoanhThu = 30, TiLeTiecCuoi = 10 },
            new ChiTietBaoCaoModel { STT = 1, NgayBaoCao = new DateTime(2023, 5, 1), SoLuongTiecCuoi = 5, DoanhThu = 100000.68, TiLeDoanhThu = 50, TiLeTiecCuoi = 20 },
            new ChiTietBaoCaoModel { STT = 2, NgayBaoCao = new DateTime(2023, 5, 2), SoLuongTiecCuoi = 3, DoanhThu = 60000, TiLeDoanhThu = 30, TiLeTiecCuoi = 10 },
            new ChiTietBaoCaoModel { STT = 1, NgayBaoCao = new DateTime(2023, 5, 1), SoLuongTiecCuoi = 5, DoanhThu = 100000.68, TiLeDoanhThu = 50, TiLeTiecCuoi = 20 },
            new ChiTietBaoCaoModel { STT = 2, NgayBaoCao = new DateTime(2023, 5, 2), SoLuongTiecCuoi = 3, DoanhThu = 60000, TiLeDoanhThu = 30, TiLeTiecCuoi = 10 },
            new ChiTietBaoCaoModel { STT = 3, NgayBaoCao = new DateTime(2024, 3, 15), SoLuongTiecCuoi = 4, DoanhThu = 80000, TiLeDoanhThu = 40, TiLeTiecCuoi = 15 }
        };
    }
}
