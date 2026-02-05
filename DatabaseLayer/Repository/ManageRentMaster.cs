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
    public class ManageRentMaster : IRentMaster
    {
        private readonly ApplicationDbContext _context;
        public ManageRentMaster(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> addRentMaster(RentMaster rentMaster)
        {
            try
            {
                if (rentMaster == null)
                {
                    return new ResponseResult("Fail", "Please fill all the fields");
                }
                List<string> errors = new List<string>();

                // Property Rent Agreement must exist
                bool praExists = await _context.PropertyRentAgreements.AnyAsync(x => x.Id == rentMaster.PropertyRentAgreementId);

                if (!praExists)
                    errors.Add("Invalid Property Rent Agreement Id. Property Rent Agreement does not exist.");

                // Duplicate payment check (same agreement + same payment date)
                bool paymentAlreadyExist = await _context.RentMasters.AnyAsync(x =>
                    x.PropertyRentAgreementId == rentMaster.PropertyRentAgreementId &&
                    x.PaymentDate == rentMaster.PaymentDate
                );

                if (paymentAlreadyExist)
                    errors.Add("Rent already paid for this agreement on the same payment date.");

                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }

                await _context.RentMasters.AddAsync(rentMaster);
                await _context.SaveChangesAsync();

                return new ResponseResult("Ok", "Successfully Saved");
            }
            catch (Exception ex)
            {
                return new ResponseResult("Fail", ex.Message);
            }
        }

        public async Task<ResponseResult> GetRentMasterById(int Id)
        {
            try
            {
                var result = await _context.RentMasters.Select(x => new
                {
                    x.Id,
                    x.PropertyRentAgreementId,
                    x.RentAmount,
                    x.PaymentType,
                    x.Reference,
                    x.PaymentDate,
                    x.OfficeStaffId,
                    x.CreatedAt, 
                    x.UpdatedAt,
                    x.Remark
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "Rent Not Exist");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> GetRentMasterList()
        {
            try
            {
                var result = await _context.RentMasters.Select(x => new
                {
                    x.Id,
                    x.PropertyRentAgreementId,
                    x.RentAmount,
                    x.PaymentType,
                    x.Reference,
                    x.PaymentDate,
                    x.OfficeStaffId,
                    x.CreatedAt,
                    x.UpdatedAt,
                    x.Remark
                }).ToListAsync();
                if(result == null !|| !result.Any())
                {
                    return new ResponseResult("Ok","Data not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> updateRentMaster(int Id, RentMaster rentMaster)
        {
            try
            {
                if (rentMaster == null)
                    return new ResponseResult("Fail", "Model is empty");

                List<string> errors = new List<string>();

                var existing = await _context.RentMasters.FirstOrDefaultAsync(x => x.Id == Id);

                if (existing == null)
                    return new ResponseResult("Fail", "Rent record not found");

                // ✅ PRA must exist
                bool praExists = await _context.PropertyRentAgreements.AnyAsync(x => x.Id == rentMaster.PropertyRentAgreementId);

                if (!praExists)
                    errors.Add("Invalid Property Rent Agreement Id. Property Rent Agreement does not exist.");

                // Duplicate payment check (exclude current record)
                bool paymentAlreadyExist = await _context.RentMasters.AnyAsync(x =>
                    x.Id != Id &&
                    x.PropertyRentAgreementId == rentMaster.PropertyRentAgreementId &&
                    x.PaymentDate == rentMaster.PaymentDate
                );

                if (paymentAlreadyExist)
                    errors.Add("Rent already paid for this agreement on the same payment date.");

                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }

                // ✅ Update fields
                existing.RentAmount = rentMaster.RentAmount;
                existing.PaymentType = rentMaster.PaymentType;
                existing.Reference = rentMaster.Reference;
                existing.Remark = rentMaster.Remark;
                existing.PaymentDate = rentMaster.PaymentDate;
                existing.UpdatedAt = DateTime.Now;

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
