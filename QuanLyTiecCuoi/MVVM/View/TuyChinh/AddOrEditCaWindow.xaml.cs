using QuanLyTiecCuoi.Data.Models;
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
using QuanLyTiecCuoi.MVVM.ViewModel;
using System.Windows.Shapes;

namespace QuanLyTiecCuoi.MVVM.View.TuyChinh
{
    /// <summary>
    /// Interaction logic for AddOrEditCaWindow.xaml
    /// </summary>
    public partial class AddOrEditCaWindow : Window
    {
        public AddOrEditCaWindow(CASANH ca = null)
        {
            InitializeComponent();

            var vm = new AddOrEditCaViewModel();

            if (ca != null)
                vm.CaInfo = new CASANH
                {
                    MaCa = ca.MaCa,
                    TenCa = ca.TenCa,
                    GioBatDau = ca.GioBatDau,
                    GioKetThuc = ca.GioKetThuc
                };

            vm.LuuThanhCong += result =>
            {
                DialogResult = true;
                Close();
            };

            DataContext = vm;
        }
    }

}
