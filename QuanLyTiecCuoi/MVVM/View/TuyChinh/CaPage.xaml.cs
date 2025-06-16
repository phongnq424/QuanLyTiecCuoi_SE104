using QuanLyTiecCuoi.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyTiecCuoi.MVVM.View.TuyChinh
{
    /// <summary>
    /// Interaction logic for CaPage.xaml
    /// </summary>
    public partial class CaPage : Page
    {
        public CaPage(CaViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void ClearFilter_Click(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is CaViewModel vm)
            {
                vm.FilterTenSanh = string.Empty;
                vm.FilterGioBatDau = string.Empty;
                vm.FilterGioKetThuc = string.Empty;
            }
        }
    }
}
