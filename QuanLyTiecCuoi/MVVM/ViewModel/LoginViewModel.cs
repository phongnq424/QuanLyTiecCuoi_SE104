using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi.MVVM.View.MainVindow;
using QuanLyTiecCuoi.Services;
using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.MVVM.View.Login;

namespace QuanLyTiecCuoi.MVVM.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly DangNhapService _DangNhapService;


        private Visibility _ErrorMessVisability = Visibility.Hidden;
        public Visibility ErrorMessVisability
        {
            get { return _ErrorMessVisability; }
            set { _ErrorMessVisability = value; OnPropertyChanged(); }
        }



        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private bool _dangDangNhap = false;
        #region Command
        public ICommand FirstLoadCommand { get; set; }
        public ICommand LoginButtonCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        #endregion
        public LoginViewModel(DangNhapService dangNhapService)
        {
            _DangNhapService = dangNhapService;
            ErrorMessVisability = Visibility.Hidden;
            FirstLoadCommand = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {

            });

            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            ForgotPasswordCommand = new RelayCommand<Window>((P) => { return true; }, (p) =>
            {
                p.Close();
                //bo chuc nang nay
            });

            LoginCommand = new RelayCommand<Window>((p) =>
            { return true; }, (p) =>
            {

                if (!loginCondition())
                {
                    ErrorMessVisability = Visibility.Visible;
                    return;
                }
                Login(p);
            });
        }

        private async void Login(Window p)
        {
            if (_dangDangNhap) return;
            _dangDangNhap = true;

            try
            {
                var nguoidung = await _DangNhapService.Login(UserName, Password);
                if (nguoidung != null)
                {
                    MainWindowViewModel.NguoiDungHienTai = nguoidung;
                    var wd = App.AppHost?.Services.GetRequiredService<MainWindow>();
                    if (wd != null)
                    {
                        Application.Current.MainWindow = wd;
                        wd.Show();
                        p?.Close();
                    }
                }
                else
                {
                    ErrorMessVisability = Visibility.Visible;
                }
            }
            finally
            {
                _dangDangNhap = false;
            }
        }

        private bool loginCondition()
        {
            return (UserName != null) && (UserName.Length > 0) && Password != null && (Password.Length > 0);
        }

    }
}
