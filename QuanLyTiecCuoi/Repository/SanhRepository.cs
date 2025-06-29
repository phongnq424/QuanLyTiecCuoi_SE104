﻿using QuanLyTiecCuoi.Data;
using QuanLyTiecCuoi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Repository
{
    public class SanhRepository
    {
        private readonly WeddingDbContext _context;

        public SanhRepository(WeddingDbContext context)
        {
            _context = context;
        }

        public bool IsUsedByAnyDatTiec(int maSanh)
        {
            return _context.DatTiecs.Any(dt => dt.MaSanh == maSanh && dt.NgayDaiTiec.Date >= DateTime.Now.Date);
        }

        public List<SANH> GetAll()
        {
            return _context.Sanhs.Include(s => s.LoaiSanh).Where(s => !s.TinhTrang).ToList();
        }

        public SANH GetById(int maSanh)
        {
            return _context.Sanhs.Include(s => s.LoaiSanh).FirstOrDefault(s => s.MaSanh == maSanh);
        }

        public void AddSanh(SANH sanh)
        {
            _context.Sanhs.Add(sanh);
            _context.SaveChanges();
        }

        public void UpdateSanh(SANH sanh)
        {
            _context.Sanhs.Update(sanh);
            _context.SaveChanges();
        }

        public void DeleteSanh(int maSanh)
        {
            var sanh = _context.Sanhs.Find(maSanh);
            if (sanh != null)
            {
                sanh.TinhTrang = true;
                //_context.Sanhs.Remove(sanh);
                _context.SaveChanges();
            }
        }
    }
}
