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
    /// Interaction logic for Tiec_tracuu.xaml
    /// </summary>
    public partial class Tiec_tracuu : Page
    {
        public Tiec_tracuu()
        { 
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DatTiec());
        }

        private void InHoaDon_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DatTiec());
        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e) {}
        public void TextBox_LostFocus(object sender, RoutedEventArgs e) { }
    }
}
