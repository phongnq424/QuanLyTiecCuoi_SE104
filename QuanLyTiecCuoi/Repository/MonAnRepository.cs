using QuanLyTiecCuoi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using QuanLyTiecCuoi.Data;
using System.Windows;

namespace QuanLyTiecCuoi.Repositories
{
    public class MonAnRepository
    {
        private readonly WeddingDbContext _context;

        public MonAnRepository(WeddingDbContext context)
        {
            _context = context;
        }

        public List<MONAN> GetAll()
        {
            var result = _context.MonAns.AsNoTracking().ToList();
            MessageBox.Show($"[DEBUG] Số lượng món ăn từ DB: {result.Count}");
            return result;
        }

        public MONAN GetById(int id)
        {
            return _context.MonAns.Find(id);
        }

        public void Add(MONAN monAn)
        {
            _context.MonAns.Add(monAn);
            _context.SaveChanges();
        }

        public void Update(MONAN monAn)
        {
            _context.MonAns.Update(monAn);
            _context.SaveChanges();
        }

        public void Delete(MONAN monAn)
        {
            _context.MonAns.Remove(monAn);
            _context.SaveChanges();
        }
    }
}
