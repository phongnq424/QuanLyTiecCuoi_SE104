using System.Collections.Generic;
using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;

namespace QuanLyTiecCuoi.Services
{
    public class CaService
    {
        private readonly CaRepository _repo;

        public CaService(CaRepository repo)
        {
            _repo = repo;
        }

        public List<CASANH> LayDanhSachCa(string tenCa, string gioBD, string gioKT)
        {
            return _repo.GetFilteredCa(tenCa, gioBD, gioKT);
        }

        public void ThemCa(CASANH ca)
        {
            _repo.AddCa(ca);
        }

        public void CapNhatCa(CASANH ca)
        {
            _repo.UpdateCa(ca);
        }

        public void XoaCa(int maCa)
        {
            _repo.DeleteCa(maCa);
        }
    }
}
