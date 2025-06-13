using QuanLyTiecCuoi.Data.Models;
using QuanLyTiecCuoi.Repository;
using System.Collections.Generic;

namespace QuanLyTiecCuoi.Services
{
    public class DichVuService
    {
        private readonly DichVuRepository _repository;

        public DichVuService(DichVuRepository repository)
        {
            _repository = repository;
        }

        public List<DICHVU> GetAllDichVu()
        {
            return _repository.GetAll();
        }

        public DICHVU GetDichVuById(int id)
        {
            return _repository.GetById(id);
        }

        public void ThemDichVu(DICHVU dichVu)
        {
            _repository.Add(dichVu);
        }

        public void CapNhatDichVu(DICHVU dichVu)
        {
            _repository.Update(dichVu);
        }

        public void XoaDichVu(DICHVU dichVu)
        {
            _repository.Delete(dichVu);
        }
    }
}
