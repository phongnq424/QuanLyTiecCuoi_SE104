using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.Core;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.View.HoaDon;
using QuanLyTiecCuoi.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Services
{
    class WindowService : IWindowService
    {
        private readonly IServiceProvider _provider;

        public WindowService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public void ShowChiTietHoaDon(HoaDonViewModel vm)
        {
            var window = new ChiTietHoaDonWindow(vm);
            window.ShowDialog();
        }
    }
}
