using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private static readonly Regex _decimalRegex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");

        private void DecimalOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            string fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            e.Handled = !_decimalRegex.IsMatch(fullText);
        }
        

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string text = (string)e.DataObject.GetData(DataFormats.Text);
                if (!_decimalRegex.IsMatch(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
