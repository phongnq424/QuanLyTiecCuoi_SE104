using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.TuyChinh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class TuyChinhViewModel : BaseViewModel
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        #region Command
        public ICommand DoiTrangCommand { get; set; }
        #endregion

        public TuyChinhViewModel() {
            DoiTrangCommand = new RelayCommand<String>((p) => { return true; }, (p) =>
            {
                String viewName = TenManHinh(p);
                CurrentView = LoadViewByName(viewName);
            });
        }

        private string TenManHinh(string p)
        {
            switch (p)
            {
                case "Nhân viên": return "TuyChinh.NhanVienPage";
                case "Loại sảnh": return "DSLoaiSanhView";
                case "Món ăn": return "MonAn.TuyChinhMonAn";
                case "Dịch vụ": return "DichVu.TuyChinhDichVu";
                case "Quy định": return "null";
                default: return "";
                       
            }
        }

        private object LoadViewByName(string viewName)
        {
            string fullTypeName = $"QuanLyTiecCuoi.MVVM.View.{viewName}";
            var assembly = typeof(App).Assembly;
            var type = assembly.GetType(fullTypeName);

            if (type == null)
            {
                MessageBox.Show($"Không tìm thấy type {fullTypeName} trong assembly {assembly.FullName}");
                return null;
            }

            var service = App.AppHost?.Services.GetService(type);
            if (service == null)
            {
                MessageBox.Show($"Không lấy được service cho type {fullTypeName}");
            }
            return service;
        }
    }
}
