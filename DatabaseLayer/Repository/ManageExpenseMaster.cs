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
    public class ManageExpenseMaster : IExpenseMaster
    {
        private readonly ApplicationDbContext _context;
        public ManageExpenseMaster(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> addExpenseMaster(ExpensesMaster expensesMaster)
        {
            try
            {

                List<string> error = new List<string>();

                bool ExpCategoryExists = await _context.ExpenseCategories.AnyAsync(o => o.Id == expensesMaster.ExpenseCategoryId);
             
                var data = await _context.Properties.ToListAsync();
               
                if (!ExpCategoryExists)
                {
                    error.Add("Invalid Expense Category. Expense Category does not Exist.");
                }
                if (error.Count == 0)
                {
                    _context.ExpensesMaster.Add(expensesMaster);
                    await _context.SaveChangesAsync();
                    return new ResponseResult("Ok", "Expenses added Successfully");
                }
                return new ResponseResult("Fail", string.Join(",", error));
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getExpenseMasterById(int Id)
        {
            try
            {
                var result = await _context.ExpensesMaster.Select(x => new
                {
                    x.Id,
                    x.OrganizationId,
                    x.MosqueId,
                    x.ExpenseCategoryId,
                    x.PaidTo,
                    x.ExpenseAmount,
                    x.Remark,
                    x.PaymentType,
                    x.Reference,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                    x.CreatedAt
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

        public async Task<ResponseResult> getExpenseMasterList()
        {
            try
            {
                var expMaster = await _context.ExpensesMaster.Select(x => new {
                    x.Id,
                    x.OrganizationId,
                    x.MosqueId,
                    x.ExpenseCategoryId,
                    x.PaidTo,
                    x.ExpenseAmount,
                    x.Remark,
                    x.PaymentType,
                    x.Reference,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                    x.CreatedAt
                }).ToListAsync();

                var expCategory = await _context.ExpenseCategories
                       .Select(a => new
                       {
                           a.Id,
                           a.Name,
                       }).ToListAsync();
                var organizations = await _context.OrganizationMaster
                       .Select(a => new
                       {
                           a.Id,
                           a.Name,
                       }).ToListAsync();

                var mosques = await _context.Mosques
                       .Select(a => new
                       {
                           a.Id,
                           a.Name,
                           a.Address,
                           a.OrganizationId,
                           OrganizationName = a.OrgMosque.Name
                       }).ToListAsync();
                return new ResponseResult("Ok", new
                {
                    ExpensesMaster = expMaster,
                    ExpCategory = expCategory,
                    Organizations = organizations,
                    Mosques = mosques
                });
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> updateExpenseMaster(int Id, ExpensesMaster expensesMaster)
        {
            try
            {
                var result = await _context.ExpensesMaster.FirstOrDefaultAsync(x => x.Id == Id);

                if (result == null)
                {
                    return new ResponseResult("Fail", "Expenses Not Found");
                }
                result.ExpenseCategoryId = expensesMaster.ExpenseCategoryId;
                result.MosqueId = expensesMaster.MosqueId;
                result.OrganizationId= expensesMaster.OrganizationId;
                result.PaidTo = expensesMaster.PaidTo;
                result.ExpenseAmount = expensesMaster.ExpenseAmount;
                result.Remark = expensesMaster.Remark;
                result.PaymentType = expensesMaster.PaymentType;
                result.Reference = expensesMaster.Reference;

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
