using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuanLyTiecCuoi.Data.Models;

namespace QuanLyTiecCuoi.Data
{
    internal class WeddingDbContext : DbContext
    {
        public DbSet<SANH> Sanhs { get; set; }
        public DbSet<LOAISANH> LoaiSanhs { get; set; }
        public DbSet<DATTIEC> DatTiecs { get; set; }
        public DbSet<CHITIETDVTIEC> ChiTietDVTiecs { get; set; }
        public DbSet<CHITIETMENU> ChiTietMenus { get; set; }
        public DbSet<DICHVU> DichVus { get; set; }
        public DbSet<CASANH> CaSanhs { get; set; }
        public DbSet<MONAN> MonAns { get; set; }
        public DbSet<HOADON> HoaDons { get; set; }
        public DbSet<BAOCAOTHANG> BaoCaoThangs { get; set; }
        public DbSet<CHITIETBAOCAO> ChiTietBaoCaos { get; set; }
        public DbSet<THAMSO> ThamSos { get; set; }
        public DbSet<CHUCNANG> ChucNangs { get; set; }
        public DbSet<NHOMNGUOIDUNG> NhomNguoiDungs { get; set; }
        public DbSet<PHANQUYEN> PhanQuyens { get; set; }
        public DbSet<NGUOIDUNG> NguoiDungs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 34)));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PHANQUYEN>()
                .HasKey(p => new { p.MaNhom, p.MaChucNang });
        }
    }
}
