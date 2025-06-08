using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Data.Services
{
    internal class NhanVienService
    {
        public NhanVienService() { }
        private static NhanVienService _ins;

        public static NhanVienService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new NhanVienService();
                }
                return _ins;
            }
            private set { _ins = value; }
        }

        public async Task<NGUOIDUNG> Login(string UserName, string Password)
        {
            try
            {
                using (var context = new WeddingDbContext())
                {
                    int number = int.Parse(UserName);
                    NGUOIDUNG? user = await context.NguoiDungs.Where(
                        x => x.TenDangNhap == number.ToString() && x.MatKhau == Password).FirstOrDefaultAsync();
                    if (user == null)
                    {
                        return null;
                    }

                    return user;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CHUCNANG>> LayChucNang(NGUOIDUNG nv)
        {
            try
            {
                using(var context = new WeddingDbContext())
                {
                    var danhsach = (from pq in context.PhanQuyens
                                    where pq.MaNhom == nv.MaNhom
                                    join cn in context.ChucNangs on pq.MaChucNang equals cn.MaChucNang
                                    select new CHUCNANG
                                    {
                                        MaChucNang = cn.MaChucNang,
                                        TenChucNang = cn.TenChucNang,
                                        TenManHinhDuocLoad = cn.TenManHinhDuocLoad
                                    }).ToListAsync();
                    return await danhsach;
                }
            }
            catch (Exception ex) {
                return null;
            }
        }


        public async Task<List<CHUCNANG>> LayTatCaChucNang()
        {
            try
            {
                using (var context = new WeddingDbContext())
                {
                    var danhsach = (from  cn in context.ChucNangs 
                                    select new CHUCNANG
                                    {
                                        MaChucNang = cn.MaChucNang,
                                        TenChucNang = cn.TenChucNang,
                                        TenManHinhDuocLoad = cn.TenManHinhDuocLoad
                                    }).ToListAsync();
                    return await danhsach;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
