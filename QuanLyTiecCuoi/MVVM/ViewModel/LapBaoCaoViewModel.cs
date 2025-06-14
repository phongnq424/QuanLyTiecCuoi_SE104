using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.ViewModel.BaoCao
{
    public class LapBaoCaoViewModel : BaseViewModel
    {
        private readonly BaoCaoService _baoCaoService;

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

        public LapBaoCaoViewModel(BaoCaoService baoCaoService)
        {
            _baoCaoService = baoCaoService;

            var available = _baoCaoService.GetAvailableMonthsAndYears();
            foreach (var thang in available.Select(x => x.Thang).Distinct().OrderBy(x => x))
                ThangList.Add(thang);
            foreach (var nam in available.Select(x => x.Nam).Distinct().OrderBy(x => x))
                NamList.Add(nam);

            SelectedThang = ThangList.FirstOrDefault();
            SelectedNam = NamList.LastOrDefault();

            LapBaoCaoCommand = new RelayCommand<object>(_ => true, _ => ExecuteLapBaoCao());
        }

        private void ExecuteLapBaoCao()
        {
            if (!_baoCaoService.CoHoaDonTrongThangNam(SelectedThang, SelectedNam))
            {
                MessageBox.Show("Không có hóa đơn trong tháng/năm đã chọn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Application.Current.Windows
                .OfType<Window>()
                .SingleOrDefault(x => x.IsActive)
                ?.Close();

            // Đánh dấu xác nhận thành công (báo hiệu cho DSBaoCaoViewModel biết)
            Application.Current.Properties["BaoCao_Thang"] = SelectedThang;
            Application.Current.Properties["BaoCao_Nam"] = SelectedNam;
            Application.Current.Properties["BaoCao_OK"] = true;
        }
    }
}
