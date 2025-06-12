using System.Configuration;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.MVVM.View.BaoCao;
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi.Repository;
<<<<<<< HEAD
using QuanLyTiecCuoi.MVVM.View.Login;
using QuanLyTiecCuoi.MVVM.View.MainVindow;
using QuanLyTiecCuoi.Data.Services;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.MVVM.View.HoaDon;
using QuanLyTiecCuoi.Core;
=======
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.MVVM.View.MainVindow;
>>>>>>> main


namespace QuanLyTiecCuoi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }
        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    string connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    Console.WriteLine(">>> Connection string: " + connectionString);

                    services.AddDbContext<WeddingDbContext>(options =>
                        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

                    //interface 
                    services.AddSingleton<IWindowService, WindowService>();

                    services.AddTransient<BaoCaoRepository>();
                    services.AddTransient<ChiTietBaoCaoRepository>();
<<<<<<< HEAD
                    services.AddTransient<HoaDonRepository>();
=======
>>>>>>> main
                    services.AddTransient<NhanVienRepository>();

                    // Dùng BaoCaoService lấy dữ liệu từ AppDataRepository
                    services.AddTransient<BaoCaoService>();
                    services.AddTransient<ChiTietBaoCaoService>();
<<<<<<< HEAD
                    services.AddTransient<HoaDonService>();
                    services.AddTransient<NhanVienService>();
                    services.AddTransient<DangNhapService>();
=======
                    services.AddTransient<NhanVienService>();
>>>>>>> main

                    // Các ViewModel
                    services.AddTransient<MainWindowViewModel>();
                    services.AddTransient<BaoCaoViewModel>();
                    services.AddTransient<ChiTietBaoCaoViewModel>();
<<<<<<< HEAD
                    services.AddTransient<HoaDonViewModel>();
                    services.AddTransient<ControlBarViewModel>();
=======
>>>>>>> main
                    services.AddTransient<LoginViewModel>();

                    // Các View
                    services.AddTransient<MainWindow>();
<<<<<<< HEAD
                    services.AddTransient<LoginWindow>();
=======
                    services.AddSingleton<MainWindowViewModel>();
>>>>>>> main
                    services.AddTransient<BaoCaoPage>();
                    services.AddTransient<ChiTietBaoCaoPage>();
                    services.AddTransient<HoaDonPage>();
                    services.AddTransient<ChiTietHoaDonWindow>();
                })
                .Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {

            await AppHost.StartAsync();

            // Tự show LoginWindow (ví dụ)

            var loginWindow = AppHost.Services.GetRequiredService<LoginWindow>();
            loginWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            AppHost.Dispose();
            base.OnExit(e);
        }
    }

}
