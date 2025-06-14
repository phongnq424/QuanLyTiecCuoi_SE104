using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.Services
{
    public class ThamSoService
    {
        private readonly ThamSoRepository _thamSoRepository;

        public ThamSoService(ThamSoRepository ThamSoRepo)
        {
            _thamSoRepository = ThamSoRepo;
        }

        public async Task<THAMSO?> LayThamSo()
        {
            return await _thamSoRepository.LayDanhSachThamSo();
        }

        public async Task<THAMSO?> CapNhatThamSo(THAMSO thamso)
        {
            return await _thamSoRepository.CapNhatThamSo(thamso);
        }
    }
}
