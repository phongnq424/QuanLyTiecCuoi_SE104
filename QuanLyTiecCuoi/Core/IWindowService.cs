using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Core
{
    public interface IWindowService
    {
        void ShowChiTietHoaDon(HoaDonViewModel vm);
    }
}
