using QuanLyTiecCuoi.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Service
{
    public class LoaiSanhService
    {
        public ObservableCollection<LoaiSanh> DanhSachLoaiSanh { get; set; }

        public LoaiSanhService()
        {
            // Giả lập dữ liệu từ database
            DanhSachLoaiSanh = new ObservableCollection<LoaiSanh>
            {
                new LoaiSanh { MaLoaiSanh = 1, TenLoaiSanh = "A", DonGiaBanToiThieu = 1000000 },
                new LoaiSanh { MaLoaiSanh = 2, TenLoaiSanh = "B", DonGiaBanToiThieu = 1100000 }
            };
        }

        // Lấy tất cả các LoaiSanh từ database
        public ObservableCollection<LoaiSanh> GetAll()
        {
            return DanhSachLoaiSanh;
        }

        // Thêm mới Loại Sảnh 
        public void Add(LoaiSanh loaiSanh)
        {
            if (loaiSanh != null)
            {
                loaiSanh.MaLoaiSanh = DanhSachLoaiSanh.Any() ? DanhSachLoaiSanh.Max(x => x.MaLoaiSanh) + 1 : 1;
                DanhSachLoaiSanh.Add(loaiSanh);
            }    
                
        }

        // Chỉnh sửa Loại Sảnh
        public void Edit(LoaiSanh loaiSanh)
        {
            var existing = DanhSachLoaiSanh.FirstOrDefault(l => l.MaLoaiSanh == loaiSanh.MaLoaiSanh);
            if (existing != null)
            {
                existing.TenLoaiSanh = loaiSanh.TenLoaiSanh;
                existing.DonGiaBanToiThieu = loaiSanh.DonGiaBanToiThieu;
            }
        }

        // Xóa Loại Sảnh
        public void Delete(LoaiSanh loaiSanh)
        {
            if (loaiSanh != null)
                DanhSachLoaiSanh.Remove(loaiSanh);
        }
    }
}
