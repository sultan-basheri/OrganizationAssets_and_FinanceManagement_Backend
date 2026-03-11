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
    public class ManageSalaryMaster : ISalaryMaster
    {
        private readonly ApplicationDbContext _context;
        public ManageSalaryMaster(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> addSalaryMaster(SalaryMaster salarymaster)
        {
            try
            {

                List<string> error = new List<string>();

                bool orgExists = await _context.OrganizationMaster.AnyAsync(o => o.Id == salarymaster.OrganizationId);
                bool OffExists = await _context.OfficeStaffs.AnyAsync(o => o.Id == salarymaster.OfficeStaffId);
                bool FYearExist = await _context.FinancialYears.AnyAsync(o => o.Id == salarymaster.FinancialYearId);
                bool StaffExist = await _context.Staffs.AnyAsync(o => o.Id == salarymaster.StaffId);
                if (!orgExists)
                {
                    error.Add("Invalid Organization. Organization does not exist.");
                }
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
                    _context.SalaryMaster.Add(salarymaster);
                    await _context.SaveChangesAsync();
                    return new ResponseResult("Ok", "Salary added Successfully");
                }
                return new ResponseResult("Fail", string.Join(",", error));
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getSalaryMasterById(int Id)
        {
            try
            {
                var result = await _context.SalaryMaster.Select(x => new
                {
                    x.Id,
                    x.OrganizationId,
                    x.StaffId,
                    x.SalaryAmount,
                    x.GeneratedDate,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "Salary Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getSalaryMasterList()
        {
            try
            {
                var salary = await _context.SalaryMaster.Select(x => new {
                    x.Id,
                    x.OrganizationId,
                    x.StaffId,
                    x.SalaryAmount,
                    x.GeneratedDate,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                }).ToListAsync();
                var staff = await _context.Staffs
                        .Select(a => new
                        {
                            a.Id,
                            a.FullName,
                            a.Salary,
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
                    Salary = salary,
                    Staff = staff,
                    Organizations = organizations,
                });
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> updateSalaryMaster(int Id, SalaryMaster salaryMaster)
        {
            try
            {
                var result = await _context.SalaryMaster.FirstOrDefaultAsync(x => x.Id == Id);

                if (result == null)
                {
                    return new ResponseResult("Fail", "Salary Not Found");
                }
                result.SalaryAmount = salaryMaster.SalaryAmount;
                result.Description = salaryMaster.Description;
                result.GeneratedDate = salaryMaster.GeneratedDate; 

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
