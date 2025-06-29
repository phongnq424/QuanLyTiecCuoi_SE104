﻿using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System.Collections.Generic;

namespace QuanLyTiecCuoi.Services
{
    public class ChiTietMenuService
    {
        private readonly ChiTietMenuRepository _repository;
        private readonly MonAnRepository _monAnRepository;


        public ChiTietMenuService(ChiTietMenuRepository repository, MonAnRepository monAnRepository)
        {
            _repository = repository;
            _monAnRepository = monAnRepository;
        }
        public List<MONAN> LayDanhSachMonAnTheoDatTiec(int maDatTiec)
        {
            var chiTietMenus = _repository.GetByMaDatTiec(maDatTiec);
            var danhSachMonAn = new List<MONAN>();

            foreach (var ct in chiTietMenus)
            {
                var monAn = _monAnRepository.GetById(ct.MaMon);
                if (monAn != null)
                {
                    monAn.SoLuong = ct.SoLuong; 
                    danhSachMonAn.Add(monAn);
                }
            }

            return danhSachMonAn;
        }


        /// <summary>
        /// Thêm 1 chi tiết menu vào DB.
        /// </summary>
        public void ThemChiTietMenu(CHITIETMENU chiTiet)
        {
            if (chiTiet != null)
                _repository.Add(chiTiet);
        }

        /// <summary>
        /// Thêm nhiều chi tiết menu vào DB.
        /// </summary>
        public void ThemNhieuChiTietMenu(List<CHITIETMENU> danhSachChiTiet)
        {
            if (danhSachChiTiet != null && danhSachChiTiet.Count > 0)
                _repository.AddRange(danhSachChiTiet);
        }

        /// <summary>
        /// Lấy danh sách chi tiết menu theo mã đặt tiệc.
        /// </summary>
        public List<CHITIETMENU> LayChiTietTheoMaDatTiec(int maDatTiec)
        {
            return _repository.GetByMaDatTiec(maDatTiec);
        }

        /// <summary>
        /// Xoá toàn bộ chi tiết menu của 1 đặt tiệc.
        /// </summary>
        public void XoaTheoMaDatTiec(int maDatTiec)
        {
            _repository.DeleteByMaDatTiec(maDatTiec);
        }
        public void CapNhatChiTietMenu(int maDatTiec, List<CHITIETMENU> danhSachMoi)
        {
            _repository.CapNhatChiTietMenu(maDatTiec, danhSachMoi);
        }
    }
}
