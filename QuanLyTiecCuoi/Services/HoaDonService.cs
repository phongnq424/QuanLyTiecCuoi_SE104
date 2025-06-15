using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace QuanLyTiecCuoi.Services
{
    public class HoaDonService
    {
        private readonly HoaDonRepository _hoaDonRepository;
        private readonly ThamSoRepository _thamSoRepository;

        public HoaDonService(HoaDonRepository HoaDonRepo, ThamSoRepository thamSoRepository)
        {
            _hoaDonRepository = HoaDonRepo;
            _thamSoRepository = thamSoRepository;
        }

        public async Task<THAMSO> LayThamSo()
        {
            return await _thamSoRepository.LayDanhSachThamSo();
        }

        public async Task<List<HOADON>> GetAllHoaDonsAsync()
        {
            return await _hoaDonRepository.GetAllAsync();
        }

        public async Task<List<HOADON>> GetHoaDonByIdAsync(String id)
        {
            return await _hoaDonRepository.GetByIdAsync(id);
        }
        public async Task<List<HOADON>> GetHoaDonByMaDatTiecAsync(String id)
        {
            return await _hoaDonRepository.GetByMaDatTiecAsync(id);
        }

        public async Task<List<HOADON>> GetHoaDonTheoNgayThanhToan(DateTime ngaytt)
        {
            return await _hoaDonRepository.GetByNgayThanhToanAsync(ngaytt);
        }

        public async Task<List<HOADON>> GetHoaDonsByTongTienRangeAsync(decimal min, decimal max)
        {
            if (min > max)
                throw new ArgumentException("Giá trị min phải nhỏ hơn hoặc bằng max.");

            return await _hoaDonRepository.GetByTongTienRangeAsync(min, max);
        }

        public async Task<List<HOADON>> GetHoaDonsByNgayThanhToanAsync(DateTime ngayThanhToan)
        {
            return await _hoaDonRepository.GetByNgayThanhToanAsync(ngayThanhToan);
        }

        public async Task<List<HOADON>> GetHoaDonsByTrangThaiThanhToanAsync(bool daThanhToan)
        {
            return await _hoaDonRepository.GetByTrangThaiThanhToanAsync(daThanhToan);
        }

        public async Task<HOADON> AddHoaDonAsync(HOADON hoaDon)
        {
            if (hoaDon == null)
                throw new ArgumentNullException(nameof(hoaDon));

            return await _hoaDonRepository.AddAsync(hoaDon);
        }

        public async Task<HOADON?> UpdateHoaDonAsync(HOADON hoaDon)
        {
            if (hoaDon == null)
                throw new ArgumentNullException(nameof(hoaDon));

            return await _hoaDonRepository.UpdateAsync(hoaDon);
        }

        public async Task<HOADON?> DeleteHoaDonAsync(int maHoaDon)
        {
            if (maHoaDon <= 0)
                throw new ArgumentException("Mã hóa đơn không hợp lệ.");

            return await _hoaDonRepository.DeleteAsync(maHoaDon);
        }

        public async Task<List<CHITIETDVTIEC>> GetCTDVT(int maDatTiec)
        {
            return await _hoaDonRepository.GetCTDV(maDatTiec);
        }

        public async Task<DATTIEC?> GetDatTiec(int maDatTiec)
        {
            return await _hoaDonRepository.GetDatTiec(maDatTiec);
        }

        public async Task<List<CHITIETMENU>> GetMenu(int maDatTiec)
        {
            return await _hoaDonRepository.GetMenu(maDatTiec);
        }

        public async Task<List<DICHVU>> GetDV()
        {
            return await _hoaDonRepository.GetDichVu();
        }

        public async Task<bool> AddCTDVT(List<CHITIETDVTIEC> dsCTDV)
        {
            var tasks = dsCTDV
                .Where(item => item.MaCTDV == 0)
                .Select(item => _hoaDonRepository.AddCTDV(item));

            var results = await Task.WhenAll(tasks);

            return results.All(r => r != null);
        }

        public async Task<bool> XoaCTDVT(List<CHITIETDVTIEC> dsCTDV)
        {
            var tasks = dsCTDV
                .Select(item => _hoaDonRepository.DeleteCTDV(item.MaCTDV));

            var results = await Task.WhenAll(tasks);

            return results.All(r => r != null);
        }

        public async Task<HOADON?> ThanhToan(HOADON hoadon)
        {
            if (hoadon != null && hoadon.NgayThanhToan == null)
            {
                return await _hoaDonRepository.ThanhToan(hoadon);
            }
            return null;
        }
        public async Task<List<HOADON>> GetHoaDonsByNgayThanhToanRangeAsync(DateTime from, DateTime to)
        {
            return await _hoaDonRepository.GetByNgayThanhToanRangeAsync(from, to);
        }
    }

}
