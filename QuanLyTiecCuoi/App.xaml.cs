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
using QuanLyTiecCuoi.MVVM.View.Login;
using QuanLyTiecCuoi.MVVM.ViewModel;
using QuanLyTiecCuoi.MVVM.View.HoaDon;
using QuanLyTiecCuoi.MVVM.View.DatTiec;
using QuanLyTiecCuoi.MVVM.View.DichVu;
using QuanLyTiecCuoi.MVVM.View.MonAn;
using QuanLyTiecCuoi.MVVM.ViewModel.MonAn;
using QuanLyTiecCuoi.MVVM.ViewModel.DichVu;
using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.MVVM.View.TuyChinh;
using QuanLyTiecCuoi.MVVM.View.MainVindow;
using QuanLyTiecCuoi.MVVM.View;
using QuanLyTiecCuoi.MVVM.View.NhanVien;


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
                    services.AddTransient<HoaDonRepository>();
                    services.AddTransient<NhanVienRepository>();
                    services.AddTransient<DatTiecRepository>();
                    services.AddTransient<MonAnRepository>();
                    services.AddTransient<DichVuRepository>();
                    services.AddScoped<ChiTietMenuRepository>();
                    services.AddScoped<ChiTietDichVuRepository>();
                    services.AddTransient<CaRepository>();

                    // Dùng BaoCaoService lấy dữ liệu từ AppDataRepository
                    services.AddTransient<BaoCaoService>();
                    services.AddTransient<ChiTietBaoCaoService>();
                    services.AddTransient<HoaDonService>();
                    services.AddTransient<DangNhapService>();
                    services.AddTransient<NhanVienService>();
                    services.AddTransient<DatTiecService>();
                    services.AddTransient<DichVuService>();
                    services.AddTransient<MonAnService>();
                    services.AddScoped<ChiTietDichVuService>();
                    services.AddScoped<ChiTietMenuService>();
                    services.AddTransient<CaService>();


                    // Các ViewModel
                    services.AddTransient<MainWindowViewModel>();
                    services.AddTransient<BaoCaoViewModel>();
                    services.AddTransient<ChiTietBaoCaoViewModel>();
                    services.AddTransient<LoginViewModel>();
                    services.AddTransient<HoaDonViewModel>();
                    services.AddTransient<ControlBarViewModel>();
                    services.AddTransient<TuyChinhViewModel>();
                    services.AddTransient<NhanVienViewModel>();
                    services.AddTransient<DatTiecViewModel>();
                    services.AddTransient<SuaTiecViewModel>();
                    services.AddTransient<ThemTiecViewModel>();
                    services.AddTransient<ChonMonAnViewModel>();
                    services.AddTransient<ChonDichVuViewModel>();
                    services.AddTransient<TuyChinhMonAnViewModel>();
                    services.AddTransient<TuyChinhDichVuViewModel>();
                    services.AddTransient<CaViewModel>();


                    // Các View
                    services.AddTransient<MainWindow>();
                    services.AddTransient<BaoCaoPage>();
                    services.AddTransient<HoaDonPage>();
                    services.AddTransient<ChiTietHoaDonWindow>();
                    services.AddTransient<TuyChinhPage>();
                    services.AddTransient<NhanVienPage>();
                    services.AddTransient<LoginWindow>();
                    services.AddTransient<ChiTietBaoCaoPage>();
                    services.AddTransient<DatTiecView>();
                    services.AddTransient<SuaTiecView>();
                    services.AddTransient<ThemTiecView>();
                    services.AddTransient<ChonMonAn>();
                    services.AddTransient<ChonDichVu>();
                    services.AddTransient<TuyChinhMonAn>();
                    services.AddTransient<TuyChinhDichVu>();
                    services.AddTransient<TuyChinhQuyDinhPage>();
                    services.AddTransient<CaPage>();

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
