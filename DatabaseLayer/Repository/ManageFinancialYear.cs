using BusinessLayer.Interface;
using BusinessLayer.Model;
using DatabaseLayer.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DatabaseLayer.Repository
{
    public class ManageFinancialYear : IFinancialYear
    {
        private readonly ApplicationDbContext _context;
        public ManageFinancialYear(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> AddFinancialYear(FinancialYear financialYear)
        {
            try
            {
                if (financialYear == null)
                    return new ResponseResult("Fail", "Please fill all the fields");

                List<string> errors = new();
                var data = await _context.FinancialYears.ToListAsync();

                if(data.Any(x => x.DateFrom == financialYear.DateFrom))
                {
                    errors.Add("DateFrom already exists");
                }

                if (data.Any(x => x.DateTo == financialYear.DateTo))
                {
                    errors.Add("DateTo already exists");
                }
                if (data.Any(x => x.YearName == financialYear.YearName))
                {
                    errors.Add("Year Name already exists");
                }
                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }
                await _context.FinancialYears.AddAsync(financialYear);
                await _context.SaveChangesAsync();

                return new ResponseResult("Ok", "Financial Year added successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult("Fail", ex.Message);
            }
        }
        public async Task<ResponseResult> GetFinancialYearList()
        {
            try
            {
                var result = await _context.FinancialYears.Select(x => new
                {
                    x.Id,
                    x.YearName,
                    x.DateFrom,
                    x.DateTo,
                    x.CreatedAt,
                    x.OfficeStaffId
                }).ToListAsync();
                if (result == null || !result.Any())
                {
                    return new ResponseResult("Fail", "Data Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
        public async Task<ResponseResult> GetFinancialYearById(int Id)
        {
            try
            {
                var result = await _context.FinancialYears.Select(x => new
                {
                    x.Id,
                    x.YearName,
                    x.DateFrom,
                    x.DateTo,
                    x.CreatedAt,
                    x.OfficeStaffId
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null )
                {
                    return new ResponseResult("Fail", "Financial Year Not Exist");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
        public async Task<ResponseResult> UpdateFinancialYear(int Id, FinancialYear financialYear)
        {
            try
            {
                List<string> errors = new List<string>();

                var data = await _context.FinancialYears.ToListAsync();
                var result = await _context.FinancialYears.FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "Financial Year not found");
                }
                if (data.Any(x => x.DateFrom == financialYear.DateFrom))
                {
                    errors.Add("DateFrom already exists");
                }

                if (data.Any(x => x.DateTo == financialYear.DateTo))
                {
                    errors.Add("DateTo already exists");
                }
                if (data.Any(x => x.YearName == financialYear.YearName))
                {
                    errors.Add("Year Name already exists");
                }
                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }

                // ✅ update
                result.DateFrom = financialYear.DateFrom;
                result.DateTo = financialYear.DateTo;
                result.YearName = financialYear.YearName;

                await _context.SaveChangesAsync();

                return new ResponseResult("Ok", "Update successful");
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
    }
}
