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
    public class ManagePurchaseDetail : IPurchaseDetail
    { 
        private readonly ApplicationDbContext _context;
        public ManagePurchaseDetail(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> addPurchaseDetail(PurchaseDetail purchaseDetail)
        {
            try
            {
                if (purchaseDetail == null)
                {
                    return new ResponseResult("Fail", "Please Fill All Details");
                }
                List<string> error = new List<string>();

                bool purMasterExists = await _context.PurchaseMasters.AnyAsync(o => o.Id == purchaseDetail.PurchaseMasterId);
                if (!purMasterExists)
                {
                    error.Add("Invalid Purchase Master. Purchase Master does not exist.");
                }

                if (error.Count == 0)
                {
                    _context.PurchaseDetails.Add(purchaseDetail);
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

        public async Task<ResponseResult> getPurchaseDetailById(int Id)
        {
            try
            {
                var result = await _context.PurchaseDetails.Select(x => new
                {
                    x.Id,
                    x.PurchaseMasterId,
                    x.Name,
                    x.GrossAmount,
                    x.GstAmount,
                    x.GstPercentage,
                    x.PurchaseAmount,
                    x.Quantity,
                    x.TotalAmount,
                    x.CreatedAt,
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", $"PurchaseDetail Id = {Id} Not Exist");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }

        }

        public async Task<PurchaseDetail?> getPurchaseDetailEntityById(int id)
        {
            return await _context.PurchaseDetails.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<ResponseResult> getPurchaseDetailList()
        {
            try
            {
                var result = await _context.PurchaseDetails.Select(x => new
                {
                    x.Id,
                    x.PurchaseMasterId,
                    x.Name,
                    x.GrossAmount,
                    x.GstAmount,
                    x.GstPercentage,
                    x.PurchaseAmount,
                    x.Quantity,
                    x.TotalAmount,
                    x.CreatedAt,
                }).ToListAsync();

                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }

        }

        public async Task<ResponseResult> updatePurchaseDetail(int Id, PurchaseDetail purchaseDetail)
        {
            try
            {
                List<string> errors = new List<string>();

                var result = await _context.PurchaseDetails.FirstOrDefaultAsync(x => x.Id == Id);
                var data = await _context.PurchaseDetails.ToListAsync();
                if (result == null)
                {
                    return new ResponseResult("Fail", "Purachse Detail not found");
                }


                // ✅ update
                result.PurchaseMasterId = purchaseDetail.PurchaseMasterId;
                result.Name = purchaseDetail.Name; 
                result.GrossAmount = purchaseDetail.GrossAmount;
                result.GstAmount = purchaseDetail.GstAmount;
                result.GstPercentage = purchaseDetail.GstPercentage;
                result.PurchaseAmount = purchaseDetail.PurchaseAmount;
                result.Quantity = purchaseDetail.Quantity;
                result.TotalAmount = purchaseDetail.TotalAmount;
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
