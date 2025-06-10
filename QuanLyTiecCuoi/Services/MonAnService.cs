using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repositories;
using System.Collections.Generic;

namespace QuanLyTiecCuoi.Services
{
    public class MonAnService
    {
        private readonly MonAnRepository _repository;

        public MonAnService(MonAnRepository repository)
        {
            _repository = repository;
        }

        public List<MONAN> GetAllMonAn()
        {
            return _repository.GetAll();
        }

        public void XoaMonAn(MONAN monAn)
        {
            _repository.Delete(monAn);
        }

        public void ThemMonAn(MONAN monAn)
        {
            _repository.Add(monAn);
        }

        public void CapNhatMonAn(MONAN monAn)
        {
            _repository.Update(monAn);
        }
    }
}
