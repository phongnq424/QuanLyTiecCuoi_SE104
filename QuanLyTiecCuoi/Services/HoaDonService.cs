using Microsoft.EntityFrameworkCore;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyTiecCuoi.Services
{
    public class HoaDonService
    {
        private readonly HoaDonRepository _repo;

        public HoaDonService(HoaDonRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<HOADON>> GetAll()
        {
            return await _repo.GetAllAsync();
        }
    }

}
