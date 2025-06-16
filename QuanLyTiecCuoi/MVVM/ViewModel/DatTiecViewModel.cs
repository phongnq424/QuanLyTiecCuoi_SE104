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
using System.Globalization;
using System.Text;
using QuanLyTiecCuoi.MVVM.View.MainVindow;
using QuanLyTiecCuoi.MVVM.View.HoaDon;
using QuanLyTiecCuoi.MVVM.ViewModel;
public class DatTiecViewModel : BaseViewModel
{
    private readonly DatTiecService _datTiecService;

    private List<DATTIEC> _allDatTiec = new();
    public ObservableCollection<DATTIEC> DanhSachDatTiec { get; set; } = new();

    public ObservableCollection<string> DanhSachTieuChi { get; set; } = new()
    {
        "--Chọn tiêu chí--", "Tên cô dâu", "Tên chú rể", "Số điện thoại", "Ngày đãi", "Tên Ca", "Tên Sảnh"
    };
    public ObservableCollection<CASANH> DanhSachCa { get; set; } = new();
    public ObservableCollection<SANH> DanhSachSanh { get; set; } = new();

    private string _tieuChiDangChon = "--Chọn tiêu chí--";
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
    private readonly HoaDonService _hoaDonService;
    private readonly IWindowService _windowService;
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
    // Tạo ViewModel và truyền vào ChiTietHoaDonWindow
        var vm = new HoaDonViewModel(_hoaDonService, _windowService)
        {
            HoaDonDuocChon = hoaDon,
            TiecDuocChon = datTiec,
        };

        // Mở cửa sổ Chi tiết hóa đơn
        var chiTietWindow = new ChiTietHoaDonWindow(vm);
        chiTietWindow.ShowDialog();
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
    private CASANH _caDuocChon;
    public CASANH CaDuocChon
    {
        get => _caDuocChon;
        set
        {
            _caDuocChon = value;
            OnPropertyChanged();
            if (TieuChiDangChon == "Tên Ca")
                ThucHienTimKiem();
        }
    }

    private SANH _sanhDuocChon;
    public SANH SanhDuocChon
    {
        get => _sanhDuocChon;
        set
        {
            _sanhDuocChon = value;
            OnPropertyChanged();
            if (TieuChiDangChon == "Tên Sảnh")
                ThucHienTimKiem();
        }
    }

    private void ThucHienTimKiem()
    {
        // Nếu chưa chọn tiêu chí cụ thể, không làm gì
        if (TieuChiDangChon == "Chọn tiêu chí")
        {
            DanhSachDatTiec = new ObservableCollection<DATTIEC>(_allDatTiec);
            OnPropertyChanged(nameof(DanhSachDatTiec));
            return;
        }

        // Trường hợp không có từ khóa (text search)
        if (string.IsNullOrWhiteSpace(TuKhoaTimKiem) && CaDuocChon == null && SanhDuocChon == null)
        {
            DanhSachDatTiec = new ObservableCollection<DATTIEC>(_allDatTiec);
            OnPropertyChanged(nameof(DanhSachDatTiec));
            return;
        }

        var keyword = TuKhoaTimKiem?.Trim() ?? "";
        keyword = RemoveDiacritics(keyword.ToLower());
        IEnumerable<DATTIEC> ketQua = _allDatTiec;

        switch (TieuChiDangChon)
        {
            case "Tên cô dâu":
                ketQua = _allDatTiec.Where(x =>
                    RemoveDiacritics(x.TenCoDau ?? "").ToLower().Contains(keyword));
                break;

            case "Tên chú rể":
                ketQua = _allDatTiec.Where(x =>
                    RemoveDiacritics(x.TenChuRe ?? "").ToLower().Contains(keyword));
                break;

            case "Số điện thoại":
                ketQua = _allDatTiec.Where(x => x.SDT?.Contains(keyword) == true);
                break;

            case "Ngày đãi":
                ketQua = _allDatTiec.Where(x =>
                    x.NgayDaiTiec.ToString("dd/MM/yyyy").Contains(keyword));
                break;

            case "Tên Ca":
                if (CaDuocChon != null)
                    ketQua = _allDatTiec.Where(x => x.MaCa == CaDuocChon.MaCa);
                else
                    ketQua = Enumerable.Empty<DATTIEC>();
                break;

            case "Tên Sảnh":
                if (SanhDuocChon != null)
                    ketQua = _allDatTiec.Where(x => x.MaSanh == SanhDuocChon.MaSanh);
                else
                    ketQua = Enumerable.Empty<DATTIEC>();
                break;
        }

        DanhSachDatTiec = new ObservableCollection<DATTIEC>(ketQua);
        OnPropertyChanged(nameof(DanhSachDatTiec));
    }

    public void LoadDanhSachCa()
        {
            try
            {
                var danhSachCa = _datTiecService.GetAllCaSanhs();
                DanhSachCa.Clear();
                foreach (var ca in danhSachCa)
                    DanhSachCa.Add(ca);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LoadDanhSachCa: " + ex.Message);
            }
        }
        public void LoadDanhSachSanh()
        {
            try
            {
                var danhSachSanh = _datTiecService.GetAllSanhs();
                DanhSachSanh.Clear();
                foreach (var sanh in danhSachSanh)
                    DanhSachSanh.Add(sanh);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi LoadDanhSachSanh: " + ex.Message);
            }
        }

    public static string RemoveDiacritics(string text)
{
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

    return sb.ToString().Normalize(NormalizationForm.FormC);
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
