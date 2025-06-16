using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using QuanLyTiecCuoi.MVVM;
using QuanLyTiecCuoi.Repository;
using System.Windows.Input;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.HoaDon;
using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.Services;


namespace QuanLyTiecCuoi.MVVM.View.DatTiec
{
    /// <summary>
    /// Interaction logic for DatTiec.xaml
    /// </summary>
    public partial class DatTiecView : Page
    {
        private readonly string placeholderText = "Nhập tên tiệc cưới...";
        private bool isPlaceholderActive = true;
        private bool isEditMode = false;
        public DatTiecView(DatTiecViewModel datTiecViewModel)
        {
            InitializeComponent();
            DataContext = datTiecViewModel;
            datTiecViewModel.LoadDanhSachCa();
            datTiecViewModel.LoadDanhSachSanh();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var themTiecView = new ThemTiecView();
            themTiecView.viewModel.DanhSachChanged += () =>
            {
                // Sau khi sửa xong, load lại danh sách
                (this.DataContext as DatTiecViewModel)?.LoadDanhSachDatTiec();
            };
            NavigationService?.Navigate(themTiecView);
        }

        private void InHoaDon_Click(object sender, RoutedEventArgs e)
        {
            // Lấy ViewModel đang dùng
            if (DataContext is DatTiecViewModel vm)
            {
                // Lấy tiệc đang chọn từ DataGrid
                if (MyDataGrid.SelectedItem is DATTIEC selectedTiec)
                {
                    vm.InHoaDonCommand.Execute(selectedTiec);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một tiệc cưới để in hóa đơn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
        private DataGridRow editableRow = null;

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            isEditMode = true; // Đặt chế độ chỉnh sửa
            if (btn != null)
            {
                var row = FindVisualParent<DataGridRow>(btn);
                if (row != null)
                {
                    editableRow = row;
                    DATTIEC selected = row.Item as DATTIEC;
                    var suaTiecView = new SuaTiecView(selected);
                    suaTiecView.viewModel.DanhSachChanged += () =>
                    {
                        // Sau khi sửa xong, load lại danh sách
                        (this.DataContext as DatTiecViewModel)?.LoadDanhSachDatTiec();
                    };
                    NavigationService?.Navigate(suaTiecView);
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            var row = FindVisualParent<DataGridRow>(button);
            if (row == null)
                return;

            var selected = row.Item as DATTIEC;
            if (selected == null)
                return;

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa tiệc cưới của {selected.TenCoDau} và {selected.TenChuRe} không?",
                                         "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                var viewModel = DataContext as DatTiecViewModel;
                viewModel?.XoaTiec(selected);
                MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void FocusCell(DataGridRow row, string columnName)
        {
            var column = MyDataGrid.Columns.FirstOrDefault(c =>
            {
                if (c is DataGridTextColumn textCol)
                {
                    var binding = textCol.Binding as System.Windows.Data.Binding;
                    return binding != null && binding.Path.Path == columnName;
                }
                return false;
            });

            if (column != null)
            {
                MyDataGrid.CurrentCell = new DataGridCellInfo(row.Item, column);
                MyDataGrid.BeginEdit();

                var cellContent = column.GetCellContent(row);
                if (cellContent != null)
                    cellContent.Focus();
            }
        }

        private void MyDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var row = FindVisualParent<DataGridRow>((DependencyObject)e.OriginalSource);

            // Nếu double click vào row và row không phải là row đang được chỉnh sửa
            if ((row!=null && isEditMode == false) || row != editableRow)
            {
                MessageBox.Show("Vui lòng ấn nút sửa để sửa thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                MyDataGrid.IsReadOnly = true;
                editableRow = null;
            }
        }

        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            if (parentObject is T parent) return parent;
            else return FindVisualParent<T>(parentObject);
        }
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = FilterComboBox.SelectedItem as string;
           
            if (selected == "Ngày đãi")
            {
                sFilterTextBox.Visibility = Visibility.Collapsed;
                searchCa.Visibility = Visibility.Collapsed;
                searchSanh.Visibility = Visibility.Collapsed;
                FilterDatePicker.Visibility = Visibility.Visible;
            }
            else if (selected == "Tên Ca") 
            {
                sFilterTextBox.Visibility = Visibility.Collapsed;
                searchCa.Visibility = Visibility.Visible;
                searchCa.SelectedItem = null;
                searchSanh.Visibility = Visibility.Collapsed;
                FilterDatePicker.Visibility = Visibility.Collapsed;
            }
            else if (selected == "Tên Sảnh") 
            {
                sFilterTextBox.Visibility = Visibility.Collapsed;
                searchCa.Visibility = Visibility.Collapsed;
                searchSanh.Visibility = Visibility.Visible;
                searchSanh.SelectedItem = null;
                FilterDatePicker.Visibility = Visibility.Collapsed;
            }
            else
            {
                sFilterTextBox.Visibility = Visibility.Visible;
                searchCa.Visibility = Visibility.Collapsed;
                searchSanh.Visibility = Visibility.Collapsed;
                FilterDatePicker.Visibility = Visibility.Collapsed;
            }
            var viewModel = DataContext as DatTiecViewModel;
            viewModel?.LoadDanhSachDatTiec();
        }

        private void FilterDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as DatTiecViewModel;
            if (viewModel == null)
                return;

            DatePicker datePicker = sender as DatePicker;
            if (datePicker.SelectedDate == null || datePicker.SelectedDate.ToString() == "")
            {
                // Nếu không chọn ngày => load lại danh sách đầy đủ
                viewModel.LoadDanhSachDatTiec();
            }
            else
            {
                // Nếu có chọn ngày => lọc theo ngày
                DateTime selectedDate = datePicker.SelectedDate.Value.Date;
                viewModel.LocTheoNgay(selectedDate);
            }
        }
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            HintTextBlock.Visibility = Visibility.Collapsed;
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterTextBox.Text))
            {
                HintTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterTextBox.Text))
            {
                if (!FilterTextBox.IsFocused)
                    HintTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                HintTextBlock.Visibility = Visibility.Collapsed;
            }
        }



    }
}
