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
    public class ManageDonation : IDonation
    {
        private readonly ApplicationDbContext _context;
        public ManageDonation(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> addDonation(Donation donation)
        {
            try
            {
                if (donation == null)
                {
                    return new ResponseResult("Fail", "Please Fill All Details");
                }

                List<string> error = new List<string>();

                bool orgExists = await _context.OrganizationMaster.AnyAsync(o => o.Id == donation.OrganizationId);
                bool OffExists = await _context.OfficeStaffs.AnyAsync(o => o.Id == donation.OfficeStaffId);
                bool FYearExist = await _context.FinancialYears.AnyAsync(o => o.Id == donation.FinancialYearId);
                bool StaffExist = await _context.Staffs.AnyAsync(o => o.Id == donation.StaffId);
                var data = await _context.Properties.ToListAsync();
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
                    _context.Donations.Add(donation);
                    await _context.SaveChangesAsync();
                    return new ResponseResult("Ok", "Donation added Successfully");
                }
                return new ResponseResult("Fail", string.Join(",", error));
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getDonationById(int Id)
        {
            try
            {
                var result = await _context.Donations.Select(x => new
                {
                    x.Id,
                    x.OrganizationId,
                    x.StaffId,
                    x.DonorThrough,
                    x.Amount,
                    x.DonationType,
                    x.PaymentType,
                    x.PaymentDate,
                    x.Remark,
                    x.CreatedAt,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "Donation Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getDonationList()
        {
            try
            {
                var result = await _context.Donations.Select(x => new {
                    x.Id,
                    x.OrganizationId,
                    x.StaffId,
                    x.DonorThrough,
                    x.Amount,
                    x.DonationType,
                    x.PaymentType,
                    x.PaymentDate,
                    x.Remark,
                    x.CreatedAt,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                }).ToListAsync();
                if (result == null || !result.Any())
                {
                    return new ResponseResult("Fail", "Donation List Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> updateDonation(int Id, Donation donation)
        {
            try
            {
                List<string> errors = new List<string>();

                var result = await _context.Donations.FirstOrDefaultAsync(x => x.Id == Id);
                var data = await _context.SalaryMaster.ToListAsync();
                bool StaffExist = await _context.Staffs.AnyAsync(o => o.Id == donation.StaffId);

                if (result == null)
                {
                    return new ResponseResult("Fail", "Donation Not Found");
                }

                if (!StaffExist)
                {
                    errors.Add("Invalid Staff. Staff does not exist.");
                }

                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }

                result.StaffId = donation.StaffId;
                result.DonorThrough = donation.DonorThrough;
                result.Amount = donation.Amount;
                result.DonationType = donation.DonationType;
                result.PaymentType = donation.PaymentType;
                result.PaymentDate = donation.PaymentDate;
                result.Remark = donation.Remark;

                await _context.SaveChangesAsync();

                return new ResponseResult("Ok", "Donation Updated Successfully");
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
    }
}
