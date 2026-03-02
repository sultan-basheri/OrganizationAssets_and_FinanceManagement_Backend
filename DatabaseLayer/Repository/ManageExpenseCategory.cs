using BusinessLayer.Interface;
using BusinessLayer.Model;
using DatabaseLayer.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Repository
{
    public class ManageExpenseCategory : IExpenseCategory
    {
        private readonly ApplicationDbContext _context;
        public ManageExpenseCategory(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> addExpenseCategory(ExpenseCategory expenseCategory)
        {
            try
            {
                List<string> error = new List<string>();
                bool OffExists = await _context.OfficeStaffs.AnyAsync(o => o.Id == expenseCategory.OfficeStaffId);

                if (!OffExists)
                {
                    error.Add("Invalid OfficeStaff. Office Staff does not exist.");
                }
                if(await _context.ExpenseCategories.AnyAsync(e=>e.Name == expenseCategory.Name))
                {
                     error.Add("Expense Category Already Exists");
                }
                if (error.Count == 0)
                {
                    _context.ExpenseCategories.Add(expenseCategory);
                    await _context.SaveChangesAsync();
                    return new ResponseResult("Ok", "Expense Category added Successfully");
                }
                return new ResponseResult("Fail", string.Join(",", error));
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getExpenseCategoryById(int Id)
        {
            try
            {
                var result = await _context.ExpenseCategories.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.CreatedAt,
                    x.OfficeStaffId,
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "Expense Category Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getExpenseCategoryList()
        {
            try
            {
                var result = await _context.ExpenseCategories.Select(x => new {
                    x.Id,
                    x.Name,
                    x.CreatedAt,
                    x.OfficeStaffId,
                }).ToListAsync();
                if (result == null || !result.Any())
                {
                    return new ResponseResult("Fail", "Expense Category List Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> updateExpenseCategory(int Id, ExpenseCategory expenseCategory)
        {
            try
            {
                List<string> error = new List<string>();
                var result = await _context.ExpenseCategories.FirstOrDefaultAsync(x => x.Id == Id);

                if (result == null)
                {
                    return new ResponseResult("Fail", "Expense Category Not Found");
                }
                if (await _context.ExpenseCategories.AnyAsync(e => e.Name == expenseCategory.Name && e.Id != Id))
                {
                    error.Add("Expense Category Already Exists");
                }
                if (error.Count != 0)
                {
                    return new ResponseResult("Fail", string.Join(", ", error));
                }

                result.Name = expenseCategory.Name;
               
                await _context.SaveChangesAsync();

                return new ResponseResult("Ok", "Updated Successfully");
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
    }
}
