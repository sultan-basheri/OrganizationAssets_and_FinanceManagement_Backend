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
    public class ManagePurchaseMaster : IPurchaseMaster
    {
        private readonly ApplicationDbContext _context;
        public ManagePurchaseMaster(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> addPurchase(PurchaseMaster purchaseMaster)
        {
            try
            {
                if (purchaseMaster == null)
                {
                    return new ResponseResult("Fail", "Please Fill All Details");
                }
                List<string> error = new List<string>();

                bool OrgExists = await _context.OrganizationMaster.AnyAsync(o => o.Id == purchaseMaster.OrganizationId);
                bool FYearExists = await _context.FinancialYears.AnyAsync(o => o.Id == purchaseMaster.FinancialYearId);
                bool OffExists = await _context.OfficeStaffs.AnyAsync(o => o.Id == purchaseMaster.OfficeStaffId);

                if (!OrgExists) 
                {
                    error.Add("Invalid Organization. Organization does not exist.");
                }

                if (!OffExists)
                {
                    error.Add("Invalid OfficeStaff. Office Staff does not exist.");
                }
                if (!FYearExists)
                {
                    error.Add("Invalid Financial Year. Financial Year does not exist.");
                }

                if (error.Count == 0)
                {
                    _context.PurchaseMasters.Add(purchaseMaster);
                    await _context.SaveChangesAsync();
                    return new ResponseResult("Ok", "Successfully Saved");
                }
                return new ResponseResult("Fail", string.Join(",", error));
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getPurchaseById(int Id)
        {
            try
            {
                var result = await _context.PurchaseMasters.Select(x => new
                {
                    x.Id,
                    x.BillNo,
                    x.ChallanNo,
                    x.BusinessName,
                    x.ContactPerson,
                    x.GSTIN,
                    x.Address,
                    x.GrossAmount,
                    x.GSTType,
                    x.GSTPercentage,
                    x.GSTAmount,
                    x.TotalAmount,
                    x.BillDate,
                    x.DocType,
                    x.BillUrl,
                    x.OrganizationId,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                    x.CreatedAt,
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", $"Purchase Id = {Id} Not Exist");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<PurchaseMaster?> getPurchaseEntityById(int id)
        {
            return await _context.PurchaseMasters.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ResponseResult> getPurchaseList()
        {
            try
            {
                var result = await _context.PurchaseMasters.Select(x => new
                {
                    x.Id,
                    x.BillNo,
                    x.ChallanNo,
                    x.BusinessName,
                    x.ContactPerson,
                    x.GSTIN,
                    x.Address,
                    x.GrossAmount,
                    x.GSTType,
                    x.GSTPercentage,
                    x.GSTAmount,
                    x.TotalAmount,
                    x.BillDate,
                    x.DocType,
                    x.BillUrl,
                    x.OrganizationId,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                    x.CreatedAt,
                }).ToListAsync();

                if (result == null || !result.Any())
                {
                    return new ResponseResult("Fail", "Purchase List Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> updatePurchaseDetail(int Id, PurchaseMaster purchaseMaster)
        {
            try
            {
                List<string> errors = new List<string>();

                var result = await _context.PurchaseMasters.FirstOrDefaultAsync(x => x.Id == Id);
                var data = await _context.PurchaseMasters.ToListAsync();
                if (result == null)
                {
                    return new ResponseResult("Fail", "Purachse Detail not found");
                }


                // ✅ update
                result.BillNo = purchaseMaster.BillNo;
                result.ChallanNo = purchaseMaster.ChallanNo;
                result.BusinessName = purchaseMaster.BusinessName;
                result.ContactPerson = purchaseMaster.ContactPerson;
                result.GSTIN = purchaseMaster.GSTIN;
                result.Address = purchaseMaster.Address;
                result.GrossAmount = purchaseMaster.GrossAmount;
                result.GSTType = purchaseMaster.GSTType;
                result.GSTPercentage = purchaseMaster.GSTPercentage;
                result.GSTAmount = purchaseMaster.GSTAmount;
                result.TotalAmount = purchaseMaster.TotalAmount;
                result.BillDate = purchaseMaster.BillDate;
                result.DocType = purchaseMaster.DocType;
                result.BillUrl = purchaseMaster.BillUrl;
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
