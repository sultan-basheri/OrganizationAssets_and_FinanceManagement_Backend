using BusinessLayer.Interface;
using BusinessLayer.Model;
using DatabaseLayer.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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
                    x.VendorId,
                    x.BillNo,
                    x.ChallanNo,
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

                    PurDetail = x.PurDetail.Select(d => new {
                        d.Id,
                        d.Name,
                        d.PurchaseAmount,
                        d.Quantity,
                        d.GrossAmount,
                        d.GstPercentage,
                        d.GstAmount,
                        d.TotalAmount
                    }).ToList(),

                    PurPaymentMaster = x.PurPaymentMaster.Select(p => new {
                        p.Id,
                        p.PaymentType,
                        p.Amount,
                        p.PaymentDate,
                        p.Remark
                    }).ToList()

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
                var purmaster = await _context.PurchaseMasters.Select(x => new
                {
                    x.Id,
                    x.VendorId,
                    x.BillNo,
                    x.ChallanNo,
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
                    PaidAmount = x.PurPaymentMaster.Sum(p => p.Amount),
                    PurDetail = x.PurDetail.Select(d => new {
                        d.Name,
                        d.Quantity
                    }).ToList(),
                    PurPaymentMaster = x.PurPaymentMaster.Select(p => new {
                        p.PaymentDate
                    }).ToList()

                }).OrderByDescending(x => x.Id).ToListAsync(); 

                var organizations = await _context.OrganizationMaster
                        .Select(a => new
                        {
                            a.Id,
                            a.Name,
                        }).ToListAsync();

                var vendor = await _context.Vendor
                        .Select(a => new
                        {
                            a.Id,
                            a.BusinessName,
                            a.ContactPerson,
                            a.ContactNo,
                            a.Email,
                            a.Address,
                            a.GstIn
                        }).ToListAsync();

                return new ResponseResult("Ok", new
                {
                    Organizations = organizations,
                    PurMaster = purmaster,
                    Vendor = vendor
                });
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
        public async Task<ResponseResult> updatePurchase(int Id, PurchaseMaster purchaseMaster)
        {
            try
            {
                // Include Products & Payments to modify them
                var result = await _context.PurchaseMasters
                    .Include(x => x.PurDetail)
                    .Include(x => x.PurPaymentMaster)
                    .FirstOrDefaultAsync(x => x.Id == Id);

                if (result == null)
                {
                    return new ResponseResult("Fail", "Purchase Detail not found");
                }

                // 1. Update Master Details
                result.VendorId = purchaseMaster.VendorId;
                result.BillNo = purchaseMaster.BillNo;
                result.ChallanNo = purchaseMaster.ChallanNo;
                result.GrossAmount = purchaseMaster.GrossAmount;
                result.GSTType = purchaseMaster.GSTType;
                result.GSTPercentage = purchaseMaster.GSTPercentage;
                result.GSTAmount = purchaseMaster.GSTAmount;
                result.TotalAmount = purchaseMaster.TotalAmount;
                result.BillDate = purchaseMaster.BillDate;
                result.DocType = purchaseMaster.DocType;
                result.BillUrl = purchaseMaster.BillUrl;

                // 2. Clear old list
                _context.PurchaseDetails.RemoveRange(result.PurDetail);
                _context.PurchasePayments.RemoveRange(result.PurPaymentMaster);

                // 3. Add newly modified list
                result.PurDetail = purchaseMaster.PurDetail;
                result.PurPaymentMaster = purchaseMaster.PurPaymentMaster;

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
