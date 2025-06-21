using QuanLyTiecCuoi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using QuanLyTiecCuoi.Data;

namespace QuanLyTiecCuoi.Repository
{
    public class DichVuRepository
    {
        private readonly WeddingDbContext _context;

        public DichVuRepository(WeddingDbContext context)
        {
            _context = context;
        }

        public List<DICHVU> GetAll()
        {
            return _context.DichVus
                           .AsNoTracking()
                           .Where(dv => !dv.TinhTrang)
                           .ToList();
        }


        public DICHVU GetById(int id)
        {
            return _context.DichVus.Find(id);
        }

        public void Add(DICHVU dv)
        {
            _context.DichVus.Add(dv);
            _context.SaveChanges();
        }

        public void Update(DICHVU dv)
        {
            _context.DichVus.Update(dv);
            _context.SaveChanges();
        }

        public void SoftDelete(DICHVU dv)
        {
            var entity = _context.DichVus.FirstOrDefault(d => d.MaDichVu == dv.MaDichVu);
            if (entity != null)
            {
                entity.TinhTrang = true;
                _context.SaveChanges();
            }
        }

    }
}
