using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Repository
{
    public class LoaiSanhRepository
    {
        private readonly WeddingDbContext _context;

        public LoaiSanhRepository(WeddingDbContext context)
        {
            _context = context;
        }

        // Lấy toàn bộ danh sách loại sảnh
        public List<LOAISANH> GetAll()
        {
            return _context.LoaiSanhs.ToList();
        }

        // Lấy loại sảnh theo mã
        public LOAISANH GetById(int maLoaiSanh)
        {
            return _context.LoaiSanhs.FirstOrDefault(ls => ls.MaLoaiSanh == maLoaiSanh);
        }

        // Thêm loại sảnh mới
        public void AddLoaiSanh(LOAISANH loaiSanh)
        {
            _context.LoaiSanhs.Add(loaiSanh);
            _context.SaveChanges();
        }

        // Cập nhật loại sảnh
        public void UpdateLoaiSanh(LOAISANH loaiSanh)
        {
            _context.LoaiSanhs.Update(loaiSanh);
            _context.SaveChanges();
        }

        // Xóa loại sảnh
        public void DeleteLoaiSanh(int maLoaiSanh)
        {
            var loaiSanh = _context.LoaiSanhs.Find(maLoaiSanh);
            if (loaiSanh != null)
            {
                _context.LoaiSanhs.Remove(loaiSanh);
                _context.SaveChanges();
            }
        }
    }
}
