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
using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi.MVVM.View.Login;
<<<<<<< HEAD
using QuanLyTiecCuoi.Services;
=======
using QuanLyTiecCuoi.MVVM.View.BaoCao;
using Microsoft.Extensions.DependencyInjection;
>>>>>>> main

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly DangNhapService _DangNhapService;

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
        public MainWindowViewModel(DangNhapService dangNhapService)
        {
            _DangNhapService = dangNhapService;

            FirstLoadCM = new RelayCommand<Window>((p) => { return true; },async (p) =>
            {
<<<<<<< HEAD
                DanhSachChucNang = new ObservableCollection<CHUCNANG>(await _DangNhapService.LayChucNangNguoiDung(NguoiDungHienTai));
=======
                DanhSachChucNang = new ObservableCollection<CHUCNANG>()
                {
                    new CHUCNANG() { MaChucNang = 0, TenChucNang = "Trang chủ", TenManHinhDuocLoad = "Trangchu"},
                    new CHUCNANG() { MaChucNang = 0, TenChucNang = "Sảnh", TenManHinhDuocLoad = "Sanh"},
                    new CHUCNANG() { MaChucNang = 0, TenChucNang = "Đặt tiệc", TenManHinhDuocLoad = "DatTiec"},
                    new CHUCNANG() { MaChucNang = 0, TenChucNang = "Hóa đơn", TenManHinhDuocLoad = "HoaDon.HoaDonPage"},
                    new CHUCNANG() { MaChucNang = 0, TenChucNang = "Báo cáo", TenManHinhDuocLoad = "BaoCao.BaoCaoPage"},
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
>>>>>>> main
            });

            DangXuatCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Logout(p);
            });


            DieuHuongCommand = new RelayCommand<CHUCNANG>((p) => { return true; }, (p) =>
            {
                CurrentView = LoadViewByName(p.TenManHinhDuocLoad);
            });
            _DangNhapService = dangNhapService;
        }

        private void Logout(Window p)
        {
            var loginWindow = App.AppHost?.Services.GetRequiredService<LoginWindow>();
            if (loginWindow != null)
            {
                Application.Current.MainWindow = loginWindow;
                loginWindow.Show();
                p?.Close();
            }
        }

        public object LoadViewByName(string viewName)
        {
            if (viewName == "BaoCao.BaoCaoPage")
                return App.AppHost.Services.GetService<BaoCaoPage>();

            // fallback cho các view không cần DI
            string fullTypeName = $"QuanLyTiecCuoi.MVVM.View.{viewName}";
<<<<<<< HEAD
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
=======
            var type = Type.GetType(fullTypeName);
            if (type == null) return null;
            return Activator.CreateInstance(type);
>>>>>>> main
        }
    }
}
