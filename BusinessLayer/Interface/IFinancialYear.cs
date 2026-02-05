using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IFinancialYear
    {
        public Task<ResponseResult> GetFinancialYearList();
        public Task<ResponseResult> GetFinancialYearById(int Id);
        public Task<ResponseResult> AddFinancialYear(FinancialYear financialYear);
        public Task<ResponseResult> UpdateFinancialYear(int Id, FinancialYear financialYear);
    }
}
