using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ISalaryMaster
    {
        public Task<ResponseResult> getSalaryMasterList();
        public Task<ResponseResult> getSalaryMasterById(int Id);
        public Task<ResponseResult> addSalaryMaster(SalaryMaster salarymaster);
        public Task<ResponseResult> updateSalaryMaster(int Id, SalaryMaster salaryMaster);
    }
}
