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
    public class ManagePurchasePayment : IPurchasePayment
    {
        private readonly ApplicationDbContext _context;
        public ManagePurchasePayment(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> addPurchasePayment(PurchasePayment purchasePayment)
        {
            try
            {

                List<string> error = new List<string>();

                bool orgExists = await _context.OrganizationMaster.AnyAsync(o => o.Id == purchasePayment.OrganizationId);
                bool OffExists = await _context.OfficeStaffs.AnyAsync(o => o.Id == purchasePayment.OfficeStaffId);
                bool FYearExist = await _context.FinancialYears.AnyAsync(o => o.Id == purchasePayment.FinancialYearId);
                bool PurchaseExist = await _context.PurchaseMasters.AnyAsync(o => o.Id == purchasePayment.PurchaseMasterId);
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
                if (!PurchaseExist)
                {
                    error.Add("Invalid Purchase. Purchase Detail does not exist.");
                }
                if (error.Count == 0)
                {
                    _context.PurchasePayments.Add(purchasePayment);
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

        public async Task<ResponseResult> getPurchasePaymentDetailById(int Id)
        {
            try
            {
                var result = await _context.PurchasePayments.Select(x => new
                {
                    x.Id,
                    x.PurchaseMasterId,
                    x.OrganizationId,
                    x.Amount,
                    x.PaymentType,
                    x.Remark,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                    x.CreatedAt,
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "Purchase Payment Detail Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getPurchasePaymentList()
        {
            try
            {
                var result = await _context.PurchasePayments.Select(x => new {
                    x.Id,
                    x.PurchaseMasterId,
                    x.OrganizationId,
                    x.Amount,
                    x.PaymentType,
                    x.Remark,
                    x.OfficeStaffId,
                    x.FinancialYearId,
                    x.CreatedAt,
                }).ToListAsync();
                if (result == null || !result.Any())
                {
                    return new ResponseResult("Fail", "Purchase Payment List Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> updatePurchasePaymentDetails(int Id, PurchasePayment purchasePayment)
        {
            try
            {
                var result = await _context.PurchasePayments.FirstOrDefaultAsync(x => x.Id == Id);

                if (result == null)
                {
                    return new ResponseResult("Fail", "Purchase Payment Detail Not Found");
                }
                result.Amount = purchasePayment.Amount;
                result.PaymentType = purchasePayment.PaymentType;
                result.Remark = purchasePayment.Remark;

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
