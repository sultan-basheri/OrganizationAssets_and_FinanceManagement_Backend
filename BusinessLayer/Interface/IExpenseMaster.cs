using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IExpenseMaster
    {
        public Task<ResponseResult> getExpenseMasterList();
        public Task<ResponseResult> getExpenseMasterById(int Id);
        public Task<ResponseResult> addExpenseMaster(ExpensesMaster expensesMaster);
        public Task<ResponseResult> updateExpenseMaster(int Id, ExpensesMaster expensesMaster);
    }
}
