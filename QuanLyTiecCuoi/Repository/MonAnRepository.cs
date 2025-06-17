using QuanLyTiecCuoi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using QuanLyTiecCuoi.Data;
using System.Windows;

namespace QuanLyTiecCuoi.Repository
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
            return _context.MonAns
                           .AsNoTracking()
                           .Where(m => !m.isDelelte)
                           .ToList();
        }
        public void SoftDelete(MONAN monAn)
        {
            var entity = _context.MonAns.FirstOrDefault(m => m.MaMon == monAn.MaMon);
            if (entity != null)
            {
                entity.isDelelte = true;
                _context.SaveChanges();
            }
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
