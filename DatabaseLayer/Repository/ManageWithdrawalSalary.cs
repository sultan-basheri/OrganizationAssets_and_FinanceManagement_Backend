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
    public class ManageWithdrawalSalary : IWithdrawalSalary
    {
        private readonly ApplicationDbContext _context;
        public ManageWithdrawalSalary(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> addWithdrawalSalary(WithdrawalSalary withdrawal)
        {
            try
            {

                List<string> error = new List<string>();

                bool OffExists = await _context.OfficeStaffs.AnyAsync(o => o.Id == withdrawal.OfficeStaffId);
                bool FYearExist = await _context.FinancialYears.AnyAsync(o => o.Id == withdrawal.FinancialYearId);
                bool StaffExist = await _context.Staffs.AnyAsync(o => o.Id == withdrawal.StaffId);
                
                if (!OffExists)
                {
                    error.Add("Invalid OfficeStaff. Office Staff does not exist.");
                }

                if (!FYearExist)
                {
                    error.Add("Invalid Financial Year. Financial Year does not exist.");
                }
                if (!StaffExist)
                {
                    error.Add("Invalid Staff. Staff does not exist.");
                }
                if (error.Count == 0)
                {
                    _context.WithdrawalSalaries.Add(withdrawal);
                    await _context.SaveChangesAsync();
                    return new ResponseResult("Ok", "Successfully saved");
                }
                return new ResponseResult("Fail", string.Join(",", error));
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getWithdrawalSalaryById(int Id)
        {
            try
            {
                var result = await _context.WithdrawalSalaries.Select(x => new
                {
                    x.Id,
                    x.StaffId,
                    x.WithdrawalAmount,
                    x.Remark,
                    x.PaymentType,
                    x.Reference,
                    x.PaymentDate,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                    x.CreatedAt
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "Withdrawal Salary Id Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getWithdrawalSalaryList()
        {
            try
            {
                var withdrawsalary = await _context.WithdrawalSalaries.Select(x => new {
                    x.Id,
                    x.StaffId,
                    x.WithdrawalAmount,
                    x.Remark,
                    x.PaymentType,
                    x.Reference,
                    x.PaymentDate,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                    x.CreatedAt
                }).ToListAsync();
                var staff = await _context.Staffs
                         .Select(a => new
                         {
                             a.Id,
                             a.FullName,
                             a.MosqueId,
                             a.OrganizationId,
                             OrganizationName = a.Organization.Name,
                             MosqueName = a.Mosque.Name

                         }).ToListAsync();
                var organizations = await _context.OrganizationMaster
                        .Select(a => new
                        {
                            a.Id,
                            a.Name,
                        }).ToListAsync();

                return new ResponseResult("Ok", new
                {
                    WithdrawSalary = withdrawsalary,
                    Staff = staff,
                    Organizations = organizations,
                });
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> updateWithdrawalSalary(int Id, WithdrawalSalary withdrawal)
        {
            try
            {
                var result = await _context.WithdrawalSalaries.FirstOrDefaultAsync(x => x.Id == Id);

                if (result == null)
                {
                    return new ResponseResult("Fail", "Withrawal Salary Not Found");
                }
                result.WithdrawalAmount = withdrawal.WithdrawalAmount;
                result.Remark = withdrawal.Remark;
                result.PaymentType = withdrawal.PaymentType;
                result.Reference = withdrawal.Reference; 
                result.PaymentDate = withdrawal.PaymentDate;

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
