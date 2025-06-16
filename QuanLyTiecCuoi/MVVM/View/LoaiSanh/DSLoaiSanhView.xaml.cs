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

namespace QuanLyTiecCuoi.MVVM.View
{
    /// <summary>
    /// Interaction logic for DSLoaiSanhView.xaml
    /// </summary>
    public partial class DSLoaiSanhView : Page
    {
        public DSLoaiSanhView(LoaiSanhViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void ClearFilter_Click(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is LoaiSanhViewModel vm)
            {
                vm.FilterTenLoaiSanh = string.Empty;
                vm.FilterDonGiaBanToiThieu = string.Empty;
            }
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string text)
        {
            return text.All(char.IsDigit);
        }
    }
}
