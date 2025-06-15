using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.DatTiec;
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.Core;
using System.Windows;
using System.Windows.Documents;
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
        KhoiTaoLenh();
    }
    public RelayCommand<DATTIEC> InHoaDonCommand { get; private set; }
    public DatTiecViewModel()
    {
        _datTiecService = App.AppHost.Services.GetRequiredService<DatTiecService>();
        KhoiTaoLenh();
        LoadDanhSachDatTiec();
    }
    private void InHoaDon(DATTIEC datTiec)
    {
        if (datTiec == null) return;

        var hoaDon = _datTiecService.GetHoaDonTheoMaDatTiec(datTiec.MaDatTiec);
        if (hoaDon == null)
        {
            MessageBox.Show("Không tìm thấy hóa đơn cho tiệc này.");
            return;
        }
        DateTime ngayHienTai = DateTime.Now.Date;
        DateTime ngayDaiTiec = datTiec.NgayDaiTiec.Date;

        if (ngayHienTai < ngayDaiTiec)
        {
            MessageBox.Show("Tiệc chưa diễn ra.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        if (!hoaDon.NgayThanhToan.HasValue)
        {
            decimal tongTien = hoaDon.TienPhaiThanhToan;
            decimal tienPhat = 0;
            decimal tienPhaiThanhToan = tongTien + tienPhat;

            hoaDon.NgayThanhToan = ngayHienTai;
            hoaDon.TienPhaiThanhToan = tienPhaiThanhToan;
            hoaDon.TienPhat = tienPhat;
            _datTiecService.UpdateHoaDonAsync(hoaDon);

        }
        FlowDocument document = TaoDocument(hoaDon);
        PrintDialog printDialog = new PrintDialog();
        if (printDialog.ShowDialog() == true)
        {
            printDialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Hóa đơn");
        }
    }
    private FlowDocument TaoDocument(HOADON hoaDon)
    {
        FlowDocument doc = new FlowDocument
        {
            FontFamily = new System.Windows.Media.FontFamily("Tahoma"),
            FontSize = 12,
            PagePadding = new Thickness(20),
            PageWidth = 300,
            ColumnWidth = double.PositiveInfinity
        };

        Paragraph title = new Paragraph(new Run("HÓA ĐƠN THANH TOÁN"))
        {
            FontSize = 18,
            FontWeight = FontWeights.Bold,
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 0, 0, 20)
        };
        doc.Blocks.Add(title);

        doc.Blocks.Add(new Paragraph(new Run("SereniteWedding\nKhu phố 6, P.Linh Trung, Tp.Thủ Đức, TP.HCM"))
        {
            TextAlignment = TextAlignment.Center
        });

        Paragraph content = new Paragraph();
        content.Inlines.Add(new Bold(new Run("Ngày thanh toán: ")));
        content.Inlines.Add(new Run(hoaDon.NgayThanhToan?.ToString("dd/MM/yyyy") ?? "N/A"));
        content.Inlines.Add(new LineBreak());

        content.Inlines.Add(new Bold(new Run("Khách hàng: ")));
        content.Inlines.Add(new Run(hoaDon.DATTIEC?.TenChuRe + " & " + hoaDon.DATTIEC?.TenCoDau));
        content.Inlines.Add(new LineBreak());

        content.Inlines.Add(new Bold(new Run("Tổng tiền: ")));
        content.Inlines.Add(new Run($"{hoaDon.TongTienHD:C0}"));

        doc.Blocks.Add(content);

        return doc;
    }
    private DATTIEC _datTiecDangChon;
    public DATTIEC DatTiecDangChon
    {
        get => _datTiecDangChon;
        set
        {
            _datTiecDangChon = value;
            OnPropertyChanged();
        }
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
    public void XoaTiec(DATTIEC tiec)
    {
        if (DanhSachDatTiec.Contains(tiec))
        {
            DanhSachDatTiec.Remove(tiec);

            // Xóa trong database (nếu bạn đã cài đặt repository hoặc service để xử lý)
            _datTiecService?.DeleteDatTiec(tiec.MaDatTiec); // gọi đến lớp xử lý thực tế nếu có
        }
    }
    private void KhoiTaoLenh()
    {
        NavigateCommand = new RelayCommand<object>(_ => true, NavigateToDatTiecPage);
        InHoaDonCommand = new RelayCommand<DATTIEC>(x => true, InHoaDon);

    }
}
