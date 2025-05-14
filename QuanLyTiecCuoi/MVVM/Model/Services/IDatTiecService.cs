using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTiecCuoi.MVVM.Model.Services
{
    public interface IDatTiecService
    {
        List<DatTiecModel> GetAll();
        void Add(DatTiecModel tiec);
    }
}
