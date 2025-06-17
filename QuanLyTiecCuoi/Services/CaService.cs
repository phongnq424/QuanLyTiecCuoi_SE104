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

        public bool ThemCa(CASANH ca, out string error)
        {
            error = null;
            if (_repo.IsTimeConflict(ca))
            {
                error = "Thời gian ca bị trùng với một ca khác.";
                return false;
            }

            _repo.AddCa(ca);
            return true;
        }

        public bool CapNhatCa(CASANH ca, out string error)
        {
            error = null;
            if (_repo.IsTimeConflict(ca))
            {
                error = "Thời gian ca bị trùng với một ca khác.";
                return false;
            }

            _repo.UpdateCa(ca);
            return true;
        }


        public bool XoaCa(int maCa, out string error)
        {
            return _repo.DeleteCa(maCa, out error);
        }
    }
}
