using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Services;
using QuanLyTiecCuoi.Repository;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.MVVM.View;

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

                    services.AddTransient<LoaiSanhRepository>();
                    services.AddTransient<SanhRepository>();

                    services.AddTransient<LoaiSanhService>();
                    services.AddTransient<SanhService>();

                    services.AddTransient<LoaiSanhViewModel>();
                    services.AddTransient<SanhViewModel>();

                    services.AddTransient<MainWindow>();
                    services.AddTransient<SanhView>();
                    services.AddTransient<DSLoaiSanhView>();
                    services.AddTransient<DSSanhView>();
                    services.AddTransient<AddOrEditLoaiSanhWindow>();
                    services.AddTransient<AddOrEditSanhWindow>();
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
