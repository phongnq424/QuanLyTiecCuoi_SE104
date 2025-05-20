using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Data.Services;
using QuanLyTiecCuoi.MVVM.View.Login;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    internal class MainWindowViewModel : BaseViewModel
    {
        #region Command
        public ICommand FirstLoadCM { get; set; }
        public ICommand DieuHuongCommand { get; set; }
        public ICommand DangXuatCommand { get; set; }

        #endregion

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

        private ObservableCollection<string> _viewModelList;
        public ObservableCollection<string> ViewModelList
        {
            get { return _viewModelList; }
            set
            {
                _viewModelList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<CHUCNANG> _DanhSachChucNang;
        public ObservableCollection<CHUCNANG> DanhSachChucNang
        {
            get => _DanhSachChucNang;
            set { _DanhSachChucNang = value; OnPropertyChanged(); }
        }

        public static NGUOIDUNG NguoiDungHienTai;
        public MainWindowViewModel()
        {
            FirstLoadCM = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                DanhSachChucNang = new ObservableCollection<CHUCNANG>()
                {
                    new CHUCNANG() { MaChucNang = 0, TenChucNang = "Trang chủ", TenManHinhDuocLoad = "Trangchu"},
                    new CHUCNANG() { MaChucNang = 0, TenChucNang = "Sảnh", TenManHinhDuocLoad = "Sanh"},
                    new CHUCNANG() { MaChucNang = 0, TenChucNang = "Đặt tiệc", TenManHinhDuocLoad = "DatTiec"},
                    new CHUCNANG() { MaChucNang = 0, TenChucNang = "Hóa đơn", TenManHinhDuocLoad = "HoaDon.HoaDonPage"},
                    new CHUCNANG() { MaChucNang = 0, TenChucNang = "Báo cáo", TenManHinhDuocLoad = "Trangchu"},
                    new CHUCNANG() { MaChucNang = 0, TenChucNang = "Tùy chỉnh", TenManHinhDuocLoad = "Trangchu"},
                };
                //if (NguoiDungHienTai != null)
                //{
                //    if (NguoiDungHienTai.NHOMNGUOIDUNG.MaNhom == 0)
                //    {
                //        DanhSachChucNang = new ObservableCollection<CHUCNANG>(await NhanVienService.Ins.LayTatCaChucNang());
                //        if (DanhSachChucNang == null)
                //        {
                //            MessageBox.Show("Lỗi khi đăng nhập");
                //            Logout(p);
                //        }
                //    }
                //    var ds = await NhanVienService.Ins.LayChucNang(NguoiDungHienTai);
                //    if (ds != null)
                //    {
                //        DanhSachChucNang = new ObservableCollection<CHUCNANG>(ds);
                //    }
                //    else
                //    {
                //        MessageBox.Show("Lỗi khi đăng nhập");
                //        Logout(p);
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("Lỗi khi đăng nhập");
                //    Logout(p);
                //}
            });

            DangXuatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Logout(p);
            });


            DieuHuongCommand = new RelayCommand<CHUCNANG>((p) => { return true; }, (p) =>
            {
                CurrentView = LoadViewByName(p.TenManHinhDuocLoad);
            });
        }

        private void Logout(Window p)
        {
            LoginWindow wd = new LoginWindow();
            Application.Current.MainWindow = wd;
            wd.Show();
            p.Close();
        }

        public object LoadViewByName(string viewName)
        {
            string fullTypeName = $"QuanLyTiecCuoi.MVVM.View.{viewName}";
            var type = Type.GetType(fullTypeName);
            if (type == null)
                return null;

            return Activator.CreateInstance(type);
        }
    }
}
