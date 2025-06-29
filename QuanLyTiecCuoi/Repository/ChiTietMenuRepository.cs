﻿using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyTiecCuoi.Repository
{
    public class ChiTietMenuRepository
    {
        private readonly WeddingDbContext _context;

        public ChiTietMenuRepository(WeddingDbContext context)
        {
            _context = context;
        }

        public void Add(CHITIETMENU entity)
        {
            _context.ChiTietMenus.Add(entity);
            _context.SaveChanges();
        }

        public void AddRange(List<CHITIETMENU> entities)
        {
            _context.ChiTietMenus.AddRange(entities);
            _context.SaveChanges();
        }

        public List<CHITIETMENU> GetByMaDatTiec(int maDatTiec)
        {
            return _context.ChiTietMenus
                           .Where(x => x.MaDatTiec == maDatTiec)
                           .ToList();
        }

        public void DeleteByMaDatTiec(int maDatTiec)
        {
            var items = _context.ChiTietMenus
                                .Where(x => x.MaDatTiec == maDatTiec)
                                .ToList();

            if (items.Any())
            {
                _context.ChiTietMenus.RemoveRange(items);
                _context.SaveChanges();
            }
        }
        public void CapNhatChiTietMenu(int maDatTiec, List<CHITIETMENU> danhSachMoi)
        {
            // Xóa toàn bộ trước, sau đó thêm lại
            DeleteByMaDatTiec(maDatTiec);
            _context.ChiTietMenus.AddRange(danhSachMoi);
            _context.SaveChanges();
        }
    }
}
