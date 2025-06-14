using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System.Collections.Generic;

namespace QuanLyTiecCuoi.Services
{
    public class ChiTietMenuService
    {
        private readonly ChiTietMenuRepository _repository;

        public ChiTietMenuService(ChiTietMenuRepository repository)
        {
            _repository = repository;
        }

        public void ThemChiTietMenu(CHITIETMENU chiTiet)
        {
            _repository.Add(chiTiet);
        }

        public void ThemNhieuChiTietMenu(List<CHITIETMENU> danhSachChiTiet)
        {
            _repository.AddRange(danhSachChiTiet);
        }

        public List<CHITIETMENU> LayChiTietTheoMaDatTiec(int maDatTiec)
        {
            return _repository.GetByMaDatTiec(maDatTiec);
        }

        public void XoaTheoMaDatTiec(int maDatTiec)
        {
            _repository.DeleteByMaDatTiec(maDatTiec);
        }
    }
}
