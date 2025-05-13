using QuanLyTiecCuoi.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Service
{
    public class SanhService
    {
        private ObservableCollection<Sanh> DanhSachSanh;

        public SanhService()
        {
            // Giả lập dữ liệu từ database
            DanhSachSanh = new ObservableCollection<Sanh>
            {
                new Sanh { MaSanh = 1, TenSanh = "Sảnh 1", MaLoaiSanh = 1, SoLuongBanToiDa = 20, GhiChu = "Sảnh nhỏ", LoaiSanh = new LoaiSanh { MaLoaiSanh = 1, TenLoaiSanh = "A", DonGiaBanToiThieu = 1000000 }, HinhAnh = "/Resources/sanh1.jpg" },
                new Sanh { MaSanh = 2, TenSanh = "Sảnh 2", MaLoaiSanh = 2, SoLuongBanToiDa = 30, GhiChu = "Sảnh lớn", LoaiSanh = new LoaiSanh { MaLoaiSanh = 2, TenLoaiSanh = "B", DonGiaBanToiThieu = 1100000 }, HinhAnh = "/Resources/sanh2.jpg" }
            };
        }

        // Lấy tất cả các Sảnh
        public ObservableCollection<Sanh> GetAll()
        {
            return DanhSachSanh;
        }

        // Thêm Sảnh mới
        public void AddSanh(Sanh sanh)
        {
            if (sanh != null)
            {
                sanh.MaSanh = DanhSachSanh.Any() ? DanhSachSanh.Max(s => s.MaSanh) + 1 : 1;
                DanhSachSanh.Add(sanh);
            }
        }

        // Chỉnh sửa Sảnh
        public void EditSanh(Sanh sanh)
        {
            var existingSanh = DanhSachSanh.FirstOrDefault(s => s.MaSanh == sanh.MaSanh);
            if (existingSanh != null)
            {
                existingSanh.TenSanh = sanh.TenSanh;
                existingSanh.SoLuongBanToiDa = sanh.SoLuongBanToiDa;
                existingSanh.GhiChu = sanh.GhiChu;
                existingSanh.HinhAnh = sanh.HinhAnh;
            }
        }

        // Xóa một Sảnh
        public void DeleteSanh(Sanh sanh)
        {
            if (sanh != null)
            {
                DanhSachSanh.Remove(sanh);
            }
        }
    }
}
