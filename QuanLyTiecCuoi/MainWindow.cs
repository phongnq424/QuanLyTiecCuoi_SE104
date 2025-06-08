using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.MVVM.View;
using QuanLyTiecCuoi.MVVM.View.DatTiec;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.Services;
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

namespace QuanLyTiecCuoi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var datTiecViewModel = App.AppHost.Services.GetRequiredService<DatTiecViewModel>();
            var datTiecView = new DatTiecView(datTiecViewModel);
            MainFrame.Navigate(datTiecView);

        }
    }
}