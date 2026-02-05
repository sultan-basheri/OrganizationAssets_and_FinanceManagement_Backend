using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IRentMaster
    {
        public Task<ResponseResult> GetRentMasterList();
        public Task<ResponseResult> GetRentMasterById(int Id);
        public Task<ResponseResult> addRentMaster(RentMaster rentMaster);
        public Task<ResponseResult> updateRentMaster(int Id, RentMaster rentMaster);
    }
}
