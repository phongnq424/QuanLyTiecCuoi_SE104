using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System;

namespace QuanLyTiecCuoi.MVVM.ViewModel.BaoCao
{
    public class LapBaoCaoViewModel : BaseViewModel
    {
        private readonly HoaDonService _hoaDonService;

        public ObservableCollection<int> ThangList { get; set; } = new();
        public ObservableCollection<int> NamList { get; set; } = new();

        private int _selectedThang;
        public int SelectedThang
        {
            get => _selectedThang;
            set { _selectedThang = value; OnPropertyChanged(); }
        }

        private int _selectedNam;
        public int SelectedNam
        {
            get => _selectedNam;
            set { _selectedNam = value; OnPropertyChanged(); }
        }

        public ICommand LapBaoCaoCommand { get; }

        public LapBaoCaoViewModel(HoaDonService hoaDonService)
        {
            _hoaDonService = hoaDonService;

            LoadThangNamFromHoaDon();

            LapBaoCaoCommand = new RelayCommand<object>(_ => true, _ => ExecuteLapBaoCao());
        }

        private async void LoadThangNamFromHoaDon()
        {
            var dsHoaDon = await _hoaDonService.GetAllHoaDonsAsync();
            var now = DateTime.Now;
            var thangNamList = dsHoaDon
                .Where(hd => hd.NgayThanhToan.HasValue)
                .Select(hd => new { Thang = hd.NgayThanhToan.Value.Month, Nam = hd.NgayThanhToan.Value.Year })
                .Distinct()
                .Where(tn => !(tn.Thang == now.Month && tn.Nam == now.Year))
                .ToList();

            var thangList = thangNamList.Select(tn => tn.Thang).Distinct().OrderBy(x => x);
            var namList = thangNamList.Select(tn => tn.Nam).Distinct().OrderBy(x => x);

            ThangList.Clear();
            NamList.Clear();

            foreach (var thang in thangList)
                ThangList.Add(thang);

            foreach (var nam in namList)
                NamList.Add(nam);

            SelectedThang = ThangList.FirstOrDefault();
            SelectedNam = NamList.LastOrDefault();
        }


        private async void ExecuteLapBaoCao()
        {
            DateTime from = new DateTime(SelectedNam, SelectedThang, 1);
            DateTime to = from.AddMonths(1);

            var hoaDons = await _hoaDonService.GetHoaDonsByNgayThanhToanRangeAsync(from, to);
            if (hoaDons == null || !hoaDons.Any())
            {
                MessageBox.Show("Không có hóa đơn trong tháng/năm đã chọn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(x => x.IsActive)
                ?.Close();

            Application.Current.Properties["BaoCao_Thang"] = SelectedThang;
            Application.Current.Properties["BaoCao_Nam"] = SelectedNam;
            Application.Current.Properties["BaoCao_OK"] = true;
        }

    }
}
