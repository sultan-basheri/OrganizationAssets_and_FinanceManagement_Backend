using BusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IExpenseCategory
    {
        public Task<ResponseResult> getExpenseCategoryList();
        public Task<ResponseResult> getExpenseCategoryById(int Id);
        public Task<ResponseResult> addExpenseCategory(ExpenseCategory expenseCategory);
        public Task<ResponseResult> updateExpenseCategory(int Id, ExpenseCategory expenseCategory);
    }
}
