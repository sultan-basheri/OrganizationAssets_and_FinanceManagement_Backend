using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IStaff
    {
        public Task<ResponseResult> GetStaffList();
        public Task<ResponseResult> GetStaffById(int Id);
        public Task<ResponseResult> AddStaff(Staff staff);
        public Task<ResponseResult> UpdateStaff(int Id, Staff staff);
    }
}
