using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.MVVM.View.BaoCao;
using QuanLyTiecCuoi.MVVM.ViewModel.BaoCao;
using QuanLyTiecCuoi.Services;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class DSBaoCaoViewModel : BaseViewModel
    {
        private readonly BaoCaoService _baoCaoService;

        public ObservableCollection<BaoCaoModel> DSBaoCao { get; set; }
        public ObservableCollection<string> MonthYearOptions { get; set; } = new();
        public ObservableCollection<string> MonthYearOptionsTo { get; set; } = new();

        private string _selectedTuNgay;
        public string SelectedFromMonthYear
        {
            get => _selectedTuNgay;
            set
            {
                _selectedTuNgay = value;
                OnPropertyChanged();
                UpdateDenNgayOptions();
                ApplyFilter();
            }
        }
        private BaoCaoModel _selectedBaoCao;
        public BaoCaoModel SelectedBaoCao
        {
            get => _selectedBaoCao;
            set
            {
                _selectedBaoCao = value;
                OnPropertyChanged();
            }
        }

        private string _selectedDenNgay;
        public string SelectedToMonthYear
        {
            get => _selectedDenNgay;
            set
            {
                _selectedDenNgay = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private string _minDoanhThu;
        public string MinDoanhThu
        {
            get => _minDoanhThu;
            set
            {
                _minDoanhThu = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private string _maxDoanhThu;
        public string MaxDoanhThu
        {
            get => _maxDoanhThu;
            set
            {
                _maxDoanhThu = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private string _minTiecCuoi;
        public string MinTiecCuoi
        {
            get => _minTiecCuoi;
            set
            {
                _minTiecCuoi = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private string _maxTiecCuoi;
        public string MaxTiecCuoi
        {
            get => _maxTiecCuoi;
            set
            {
                _maxTiecCuoi = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        public RelayCommand<object> ReturnCommand { get; set; }
        public RelayCommand<object> LapBaoCaoCommand { get; set; }
        public RelayCommand<object> XemChiTietCommand { get; set; }

        public DSBaoCaoViewModel(BaoCaoService baoCaoService)
        {
            _baoCaoService = baoCaoService;
            DSBaoCao = new ObservableCollection<BaoCaoModel>();
            ReturnCommand = new RelayCommand<object>(_ => true, _ => OnReturn());
            LapBaoCaoCommand = new RelayCommand<object>(_ => true, _ => OnLapBaoCao());
            XemChiTietCommand = new RelayCommand<object>(_ => SelectedBaoCao != null, _ => OnXemChiTiet());


            LoadThangNamOptions();
            LoadBaoCao();
        }
        private void OnXemChiTiet()
        {
            if (SelectedBaoCao == null)
            {
                MessageBox.Show("Vui lòng chọn một báo cáo!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var vmFactory = App.AppHost.Services.GetRequiredService<Func<int, int, ChiTietBaoCaoViewModel>>();
            var chiTietVM = vmFactory(SelectedBaoCao.Thang, SelectedBaoCao.Nam);

            var chiTietPage = App.AppHost.Services.GetRequiredService<ChiTietBaoCaoPage>();
            chiTietPage.DataContext = chiTietVM;

            var mainVM = Application.Current.MainWindow.DataContext as MainWindowViewModel;
            if (mainVM != null)
                mainVM.CurrentView = chiTietPage;

        }

        public async Task OnLapBaoCao()
        {
            var window = new LapBaoCaoWindow();
            var vm = App.AppHost.Services.GetRequiredService<LapBaoCaoViewModel>();
            window.DataContext = vm;

            // Reset trước khi mở form
            Application.Current.Properties["BaoCao_OK"] = false;

            window.ShowDialog();

            // Kiểm tra sau khi đóng
            if (Application.Current.Properties["BaoCao_OK"] is bool ok && ok)
            {
                int thang = (int)Application.Current.Properties["BaoCao_Thang"];
                int nam = (int)Application.Current.Properties["BaoCao_Nam"];

                var baoCaoService = App.AppHost.Services.GetRequiredService<BaoCaoService>();
                await baoCaoService.TaoBaoCaoAsync(thang, nam);

                LoadThangNamOptions();
                ApplyFilter();

                MessageBox.Show("Lập báo cáo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                var vmFactory = App.AppHost.Services.GetRequiredService<Func<int, int, ChiTietBaoCaoViewModel>>();
                var chiTietVM = vmFactory(thang, nam);

                var chiTietPage = App.AppHost.Services.GetRequiredService<ChiTietBaoCaoPage>();
                chiTietPage.DataContext = chiTietVM;

                if (Application.Current.MainWindow.DataContext is MainWindowViewModel mainVM)
                    mainVM.CurrentView = chiTietPage;
            }
        }

        private void LoadThangNamOptions()
        {
            var available = _baoCaoService.GetAvailableMonthsAndYears();

            foreach (var item in available)
            {
                var formatted = $"{item.Thang:00}/{item.Nam}";
                MonthYearOptions.Add(formatted);
            }

            if (MonthYearOptions.Count > 0)
                SelectedFromMonthYear = MonthYearOptions.First();
        }

        private void UpdateDenNgayOptions()
        {
            MonthYearOptionsTo.Clear();

            if (string.IsNullOrEmpty(SelectedFromMonthYear)) return;

            var tu = DateTime.ParseExact("01/" + SelectedFromMonthYear, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            foreach (var item in MonthYearOptions)
            {
                var date = DateTime.ParseExact("01/" + item, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (date >= tu)
                    MonthYearOptionsTo.Add(item);
            }

            if (!MonthYearOptionsTo.Contains(SelectedToMonthYear))
                SelectedToMonthYear = MonthYearOptionsTo.LastOrDefault();
        }

        private void LoadBaoCao()
        {
            var list = _baoCaoService.GetBaoCaoTheoBoLoc();

            DSBaoCao.Clear();
            int stt = 1;
            foreach (var item in list)
            {
                DSBaoCao.Add(new BaoCaoModel
                {
                    STT = stt++,
                    Thang = item.Thang,
                    Nam = item.Nam,
                    TongDoanhThu = item.TongDoanhThu,
                    TongTiecCuoi = item.TongTiecCuoi,
                });
            }
        }

        private void ApplyFilter()
        {
            decimal? minDT = null;
            decimal? maxDT = null;
            int? minTC = null;
            int? maxTC = null;

            if (decimal.TryParse(MinDoanhThu, out var parsedMinDT)) minDT = parsedMinDT;
            if (decimal.TryParse(MaxDoanhThu, out var parsedMaxDT)) maxDT = parsedMaxDT;
            if (int.TryParse(MinTiecCuoi, out var parsedMinTC)) minTC = parsedMinTC;
            if (int.TryParse(MaxTiecCuoi, out var parsedMaxTC)) maxTC = parsedMaxTC;

            DateTime? tu = null, den = null;
            if (!string.IsNullOrEmpty(SelectedFromMonthYear))
                tu = DateTime.ParseExact("01/" + SelectedFromMonthYear, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(SelectedToMonthYear))
                den = DateTime.ParseExact("01/" + SelectedToMonthYear, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var list = _baoCaoService.GetBaoCaoTheoBoLoc(
                tuNgay: tu,
                denNgay: den,
                minDoanhThu: minDT,
                maxDoanhThu: maxDT,
                minTiecCuoi: minTC,
                maxTiecCuoi: maxTC
            );

            DSBaoCao.Clear();
            int stt = 1;
            foreach (var item in list)
            {
                DSBaoCao.Add(new BaoCaoModel
                {
                    STT = stt++,
                    Thang = item.Thang,
                    Nam = item.Nam,
                    TongDoanhThu = item.TongDoanhThu,
                    TongTiecCuoi = item.TongTiecCuoi,
                });
            }
        }

        private void OnReturn()
        {
            var vm = Application.Current.MainWindow.DataContext as MainWindowViewModel;
            if (vm != null)
            {
                var reportPage = App.AppHost.Services.GetRequiredService<BaoCaoPage>();
                vm.CurrentView = reportPage;
            }
        }
    }
}
