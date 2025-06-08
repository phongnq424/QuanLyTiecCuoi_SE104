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
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.MVVM.View.DatTiec;


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

                    services.AddTransient<BaoCaoRepository>();
                    services.AddTransient<ChiTietBaoCaoRepository>();
                    services.AddTransient<DatTiecRepository>();

                    // Dùng BaoCaoService lấy dữ liệu từ AppDataRepository
                    services.AddTransient<BaoCaoService>();
                    services.AddTransient<ChiTietBaoCaoService>();
                    services.AddTransient<DatTiecService>();
                    // Các ViewModel
                    services.AddTransient<BaoCaoViewModel>();
                    services.AddTransient<ChiTietBaoCaoViewModel>();
                    services.AddTransient<DatTiecViewModel>();
                    services.AddTransient<ThemTiecViewModel>();
                    // Các View
                    services.AddTransient<MainWindow>();
                    services.AddTransient<BaoCaoPage>();
                    services.AddTransient<ChiTietBaoCaoPage>();
                    services.AddTransient<DatTiecView>();
                    services.AddTransient<ThemTiecView>();
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
