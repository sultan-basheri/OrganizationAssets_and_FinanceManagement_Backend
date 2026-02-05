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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DatabaseLayer.Repository
{
    public class ManageProperties : IProperties
    {
        private readonly ApplicationDbContext _context;
        public ManageProperties(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> GetPropertyList()
        {
            try
            {
                var result = await _context.Properties.Select(x => new
                {
                    x.Id,
                    x.OrganizationId,
                    x.PropertyType,
                    x.OwnerType,
                    x.RegNo,
                    x.Address,
                    x.Measurement,
                    x.MeasurementUnit,
                    x.OwnershipDate,
                    x.DocType,
                    x.DocUrl,
                    x.CreatedAt,
                    x.OfficeStaffId
                }).ToListAsync();

                if (result == null || !result.Any())
                {
                    return new ResponseResult("Fail", "Empty");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
        public async Task<ResponseResult> GetPropertyById(int Id)
        {
            try
            {
                var result = await _context.Properties.Select(x => new
                {
                    x.Id,
                    x.OrganizationId,
                    x.PropertyType,
                    x.OwnerType,
                    x.RegNo,
                    x.Address,
                    x.Measurement,
                    x.MeasurementUnit,
                    x.OwnershipDate,
                    x.DocType,
                    x.DocUrl,
                    x.CreatedAt,
                    x.OfficeStaffId
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", $"Staff Id = {Id} Not Exist");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
        public async Task<Properties?> getPropertyEntityById(int id)
        {
            return await _context.Properties.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<ResponseResult> AddProperty(Properties properties)
        {
            try
            {
                if (properties == null)
                {
                    return new ResponseResult("Fail", "Please Fill All Details");
                }
                List<string> error = new List<string>();
                bool orgExists = await _context.OrganizationMaster.AnyAsync(o => o.Id == properties.OrganizationId);
                bool OffExists = await _context.OfficeStaffs.AnyAsync(o => o.Id == properties.OfficeStaffId);
                var data = await _context.Properties.ToListAsync();
                if (!orgExists)
                {
                    error.Add("Invalid Organization. Organization does not exist.");
                }
                if (!OffExists)
                {
                    error.Add("Invalid OfficeStaff. Office Staff does not exist.");
                }
                if (data.Any(x => x.RegNo == properties.RegNo))
                {
                    error.Add("Registration Number Already Exist");
                }

                if (error.Count == 0)
                {
                    _context.Properties.Add(properties);
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
        public async Task<ResponseResult> UpdateProperty(int Id, Properties properties)
        {
            try
            {
                List<string> errors = new List<string>();
               
                var result = await _context.Properties.FirstOrDefaultAsync(x => x.Id == Id);
                var data = await _context.Properties.ToListAsync();
                if (result == null)
                {
                    return new ResponseResult("Fail", "Property not found");
                }

                if (data.Any(x => x.RegNo == properties.RegNo))
                {
                    errors.Add("Registration Number Already Exist");
                }

                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }

                // ✅ update
                result.PropertyType = properties.PropertyType;
                result.OwnerType = properties.OwnerType;
                result.RegNo = properties.RegNo;
                result.Address = properties.Address;
                result.Measurement = properties.Measurement;
                result.MeasurementUnit = properties.MeasurementUnit;
                result.DocUrl = properties.DocUrl;
                result.DocType = properties.DocType;
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
