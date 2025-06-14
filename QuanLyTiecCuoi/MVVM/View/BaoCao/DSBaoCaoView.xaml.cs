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
using QuanLyTiecCuoi.MVVM.ViewModel;

namespace QuanLyTiecCuoi.MVVM.View.BaoCao
{
    /// <summary>
    /// Interaction logic for DSBaoCao.xaml
    /// </summary>
    public partial class DSBaoCao : Page
    {
        public DSBaoCao(DSBaoCaoViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

        }
    }
}
