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

namespace QuanLyTiecCuoi
{
    /// <summary>
    /// Interaction logic for DatTiec.xaml
    /// </summary>
    public partial class DatTiec : Page
    {
        public DatTiec()
        {
            InitializeComponent();
        }

        public void DatTiec_Click(object sender, RoutedEventArgs e)
        {
            // Handle the click event for DatTiec button
            // You can add your logic here
            MessageBox.Show("Đặt tiệc thành công!");
        }

        private void DichVu_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void MonAn_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
