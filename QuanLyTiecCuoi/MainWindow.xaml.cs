using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyTiecCuoi.MVVM.View;
using QuanLyTiecCuoi.MVVM.View.HoaDon;
using QuanLyTiecCuoi.MVVM.View.BaoCao;
using QuanLyTiecCuoi.Services;

namespace QuanLyTiecCuoi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BaoCaoPage _baoCaoPage;
        private readonly ChiTietBaoCaoPage _chiTietBaoCaoPage;

        public MainWindow(BaoCaoPage baoCaoPage, ChiTietBaoCaoPage chiTietBaoCaoPage)
        {
            InitializeComponent();
            _baoCaoPage = baoCaoPage;
            _chiTietBaoCaoPage = chiTietBaoCaoPage;

            MainFrame.Navigate(_baoCaoPage);
        }
    }
}