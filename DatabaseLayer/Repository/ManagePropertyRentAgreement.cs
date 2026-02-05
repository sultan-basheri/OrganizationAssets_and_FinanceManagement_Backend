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
    public class ManagePropertyRentAgreement : IPropertyRentAgreement
    {
        private readonly ApplicationDbContext _context;
        public ManagePropertyRentAgreement(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> AddPRAgreement(PropertyRentAgreement PRAgreement)
        {
            try
            {
                if (PRAgreement == null)
                {
                    return new ResponseResult("Fail", "Please Fill All Details");
                }

                List<string> error = new List<string>();

                // 1) PropertyId + Date overlap check
                bool isOverlap = await _context.PropertyRentAgreements.AnyAsync(x =>
                    x.PropertyId == PRAgreement.PropertyId &&
                    (
                        PRAgreement.DateFrom <= x.DateTo &&
                        PRAgreement.DateTo >= x.DateFrom
                    )
                );

                if (isOverlap)
                {
                    error.Add("This property already has an active agreement in the selected date range");
                }

                // Aadhar unique check (same as your rule)
                bool isAadharExist = await _context.PropertyRentAgreements.AnyAsync(x => x.AadharNo == PRAgreement.AadharNo);
                if (isAadharExist)
                {
                    error.Add("Aadhar Number Already Exist");
                }

                // DocumentType + DocumentNo unique check
                bool isDocExist = await _context.PropertyRentAgreements.AnyAsync(x =>
                    x.DocumentNo == PRAgreement.DocumentNo &&
                    x.DocumentType == PRAgreement.DocumentType
                );
                if (isDocExist)
                {
                    error.Add("Document Number Already Exist");
                }

                if (error.Count == 0)
                {
                    _context.PropertyRentAgreements.Add(PRAgreement);
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


        public async Task<ResponseResult> GetPRAgreementById(int Id)
        {
            try
            {
                var result = await _context.PropertyRentAgreements.Select(x => new
                {
                    x.Id,
                    x.PropertyId,
                    x.DateFrom,
                    x.DateTo,
                    x.RentAmount,
                    x.FullName,
                    x.Address,
                    x.ContactNo,
                    x.AlternateNo,
                    x.Deposite,
                    x.DocumentType,
                    x.DocumentNo,
                    x.ReferenceName,
                    x.RentType,
                    x.OfficeStaffId,
                    x.DocExtension,
                    x.DocUrl
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", $"Property Rent Agreement Id = {Id} Not Exist");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
        public async Task<PropertyRentAgreement?> getPRAgreementEntityById(int id)
        {
            return await _context.PropertyRentAgreements.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ResponseResult> GetPRAgreementList()
        {
            try
            {
                var result = await _context.PropertyRentAgreements.Select(x => new
                {
                    x.Id,
                    x.PropertyId,
                    x.DateFrom,
                    x.DateTo,
                    x.RentAmount,
                    x.FullName,
                    x.Address,
                    x.ContactNo,
                    x.AlternateNo,
                    x.Deposite,
                    x.DocumentType,
                    x.DocumentNo,
                    x.ReferenceName,
                    x.RentType,
                    x.OfficeStaffId,
                    x.DocExtension,
                    x.DocUrl
                }).ToListAsync();
                if (result == null || !result.Any())
                {
                    return new ResponseResult("Fail", "Record Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> UpdatePRAgreement(int Id, PropertyRentAgreement PRAgreement)
        {
            try
            {
                List<string> errors = new List<string>();

                var result = await _context.PropertyRentAgreements.FirstOrDefaultAsync(x => x.Id == Id);

                if (result == null)
                    return new ResponseResult("Fail", "Agreement not found");

                // ✅ Date validation
                if (PRAgreement.DateFrom > PRAgreement.DateTo)
                {
                    errors.Add("DateFrom cannot be greater than DateTo");
                }

                // ✅ 1) PropertyId + Date overlap check (exclude same record)
                bool isOverlap = await _context.PropertyRentAgreements.AnyAsync(x =>
                    x.Id != Id &&
                    x.PropertyId == PRAgreement.PropertyId &&
                    (
                        PRAgreement.DateFrom <= x.DateTo &&
                        PRAgreement.DateTo >= x.DateFrom
                    )
                );

                if (isOverlap)
                {
                    errors.Add("This property already has an active agreement in the selected date range");
                }

                // ✅ 2) Aadhar unique check (exclude same record)
                bool isAadharExist = await _context.PropertyRentAgreements
                    .AnyAsync(x => x.Id != Id && x.AadharNo == PRAgreement.AadharNo);

                if (isAadharExist)
                {
                    errors.Add("Aadhar Number Already Exist");
                }

                // ✅ 3) DocumentType + DocumentNo unique check (exclude same record)
                bool isDocExist = await _context.PropertyRentAgreements.AnyAsync(x =>
                    x.Id != Id &&
                    x.DocumentNo == PRAgreement.DocumentNo &&
                    x.DocumentType == PRAgreement.DocumentType
                );

                if (isDocExist)
                {
                    errors.Add("Document Number Already Exist");
                }

                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }

                // ✅ update
                result.PropertyId = PRAgreement.PropertyId;
                result.DateFrom = PRAgreement.DateFrom;
                result.DateTo = PRAgreement.DateTo;
                result.RentAmount = PRAgreement.RentAmount;
                result.FullName = PRAgreement.FullName;
                result.Address = PRAgreement.Address;
                result.ContactNo = PRAgreement.ContactNo;
                result.AlternateNo = PRAgreement.AlternateNo;
                result.AadharNo = PRAgreement.AadharNo;
                result.Deposite = PRAgreement.Deposite;
                result.DocumentType = PRAgreement.DocumentType;
                result.DocumentNo = PRAgreement.DocumentNo;
                result.ReferenceName = PRAgreement.ReferenceName;
                result.RentType = PRAgreement.RentType;

                // document fields
                result.DocExtension = PRAgreement.DocExtension;
                result.DocUrl = PRAgreement.DocUrl;

                result.OfficeStaffId = PRAgreement.OfficeStaffId;

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
