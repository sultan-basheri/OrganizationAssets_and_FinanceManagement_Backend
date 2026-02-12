using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IWithdrawalSalary
    {
        public Task<ResponseResult> getWithdrawalSalaryList();
        public Task<ResponseResult> getWithdrawalSalaryById(int Id);
        public Task<ResponseResult> addWithdrawalSalary(WithdrawalSalary withdrawal);
        public Task<ResponseResult> updateWithdrawalSalary(int Id, WithdrawalSalary withdrawal);
    }
}
