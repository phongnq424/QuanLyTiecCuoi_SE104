using System.Configuration;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi.Repositories;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.MVVM.View.MonAn;
using QuanLyTiecCuoi.MVVM.View.DichVu;
using QuanLyTiecCuoi.MVVM.ViewModel.MonAn;
using QuanLyTiecCuoi.MVVM.ViewModel.DichVu;


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

                    services.AddDbContext<WeddingDbContext>(options =>
                        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

                    services.AddTransient<MonAnRepository>();
                    services.AddTransient<DichVuRepository>();
                    services.AddScoped<ChiTietMenuRepository>();
                    services.AddScoped<ChiTietDichVuRepository>();

                    // Dùng BaoCaoService lấy dữ liệu từ AppDataRepository
                    services.AddTransient<DichVuService>();
                    services.AddTransient<MonAnService>();
                    services.AddScoped<ChiTietDichVuService>();
                    services.AddScoped<ChiTietMenuService>();

                    // Các ViewModel
                    services.AddTransient<ChonMonAnViewModel>();
                    services.AddTransient<ChonDichVuViewModel>();
                    services.AddTransient<TuyChinhMonAnViewModel>();
                    services.AddTransient<TuyChinhDichVuViewModel>();
                    // Các View
                    services.AddTransient<MainWindow>();
                    services.AddTransient<ChonMonAn>();
                    services.AddTransient<ChonDichVu>();
                    services.AddTransient<TuyChinhMonAn>();
                    services.AddTransient<TuyChinhDichVu>();
                })
                .Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            // Tự show LoginWindow (ví dụ)
            var loginWindow = AppHost.Services.GetRequiredService<MainWindow>();
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