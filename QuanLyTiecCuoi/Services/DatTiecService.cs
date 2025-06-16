using LiveChartsCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.MVVM.Model;
using QuanLyTiecCuoi.Repository;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QuanLyTiecCuoi.Services
{
    public class DatTiecService
    {
        private readonly DatTiecRepository _datTiecRepo;

        public DatTiecService(DatTiecRepository datTiecRepo)
        {
            _datTiecRepo = datTiecRepo;
        }

        /// <summary>
        /// Trả về toàn bộ danh sách tiệc cưới.
        /// </summary>
        public List<DATTIEC> GetAllDatTiec()
        {
            return _datTiecRepo.GetAllDatTiec();
        }

        /// <summary>
        /// Lọc danh sách theo tên cô dâu.
        /// </summary>
        public List<DATTIEC> GetDatTiecByCoDau(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return GetAllDatTiec();

            return _datTiecRepo.GetDatTiecByCoDau(name);
        }
        public List<DATTIEC> GetDatTiecByChuRe(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return GetAllDatTiec();

            return _datTiecRepo.GetDatTiecByChuRe(name);
        }
        /// <summary>
        /// Lọc danh sách đặt tiệc theo SDT.
        /// </summary>
        public List<DATTIEC> GetDatTiecByPhone(String SDT)
        {
            return _datTiecRepo.GetDatTiecByPhone(SDT);
        }

        public DATTIEC? LayPhieuDatTiec(int maSanh, DateTime ngay, int maCa)
        {
            return _datTiecRepo.GetDATTIEC(maSanh, ngay, maCa);
        }

        public void AddDatTiec(DATTIEC datTiec)
        {
            if (datTiec == null)
                throw new ArgumentNullException(nameof(datTiec));

            _datTiecRepo.AddDatTiec(datTiec);

        }
        public List<CASANH> GetAllCaSanhs()
        {
            return _datTiecRepo.GetAllCaSanhs();
        }

        public List<SANH> GetAllSanhs()
        {
            return _datTiecRepo.GetAllSanhs();
        }
        public void UpdateDatTiec(DATTIEC updatedTiec)
        {
            if (updatedTiec == null)
                throw new ArgumentNullException(nameof(updatedTiec));

            // Gọi repository để update
            _datTiecRepo.UpdateDatTiec(updatedTiec);
        }
        public void DeleteDatTiec(int maDatTiec)
        {
            _datTiecRepo.DeleteDatTiec(maDatTiec);
        }

        public bool KiemTraSanhDaDat(int maSanh, DateTime ngay, int maCa)
        {
            return _datTiecRepo.KiemTraSanhDaDat(maSanh, ngay, maCa);
        }

        public static implicit operator int(DatTiecService v)
        {
            throw new NotImplementedException();
        }
        public HOADON? GetHoaDonTheoMaDatTiec(int maDatTiec)
        {
            return _datTiecRepo.GetHoaDonTheoMaDatTiec(maDatTiec);
        }
        public void AddHoaDon(DATTIEC datTiec)
        {
           _datTiecRepo.AddHoaDon(datTiec);
        }
        HoaDonRepository _hoaDonRepository;
        public async Task<HOADON?> UpdateHoaDonAsync(HOADON hoaDon)
        {
            _hoaDonRepository = App.AppHost.Services.GetRequiredService<HoaDonRepository>();
            return await _hoaDonRepository.UpdateAsync(hoaDon);
        }

    }
}