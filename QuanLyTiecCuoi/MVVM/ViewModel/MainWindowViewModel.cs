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
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi.MVVM.View.Login;
using QuanLyTiecCuoi.MVVM.View.BaoCao;

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

            FirstLoadCM = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                DanhSachChucNang = new ObservableCollection<CHUCNANG>(await _DangNhapService.LayChucNangNguoiDung(NguoiDungHienTai));
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
            // fallback cho các view không cần DI
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
