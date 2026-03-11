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
    public class ManageVendor : IVendor
    {
        private readonly ApplicationDbContext _context;
        public ManageVendor(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> addVendor(Vendor vendor)
        {
            try
            {

                List<string> error = new List<string>();

                bool BuisNameExists = await _context.Vendor.AnyAsync(o => o.BusinessName == vendor.BusinessName);
                if (BuisNameExists)
                {
                    error.Add("Business Name already exist.");
                }
                if (error.Count == 0)
                {
                    _context.Vendor.Add(vendor);
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

        public async Task<ResponseResult> getVendorById(int Id)
        {
            try
            {
                var result = await _context.Vendor.Select(x => new
                {
                    x.Id,
                    x.BusinessName,
                    x.ContactPerson,
                    x.ContactNo,
                    x.Email,
                    x.GstIn,
                    x.Address,
                    x.CreatedAt,
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "Vendor Detail Not Found");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> getVendorList()
        {
            try
            {
                var result = await _context.Vendor.Select(x => new {
                    x.Id,
                    x.BusinessName,
                    x.ContactPerson,
                    x.ContactNo,
                    x.Email,
                    x.GstIn,
                    x.Address,
                    x.CreatedAt,
                }).ToListAsync();
                
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> updateVendor(int Id, Vendor vendor)
        {
            try
            {
                List<string> error = new List<string>();
                var result = await _context.Vendor.FirstOrDefaultAsync(x => x.Id == Id);

                if (result == null)
                {
                    return new ResponseResult("Fail", "Vendor Detail Not Found");
                }
                bool BuisNameExists = await _context.Vendor.AnyAsync(o => o.BusinessName == vendor.BusinessName && o.Id != vendor.Id);
                if (BuisNameExists)
                {
                    error.Add("Business Name already exist.");
                }

                if (error.Count == 0)
                {
                    result.BusinessName = vendor.BusinessName;
                    result.ContactNo = vendor.ContactNo;
                    result.ContactPerson = vendor.ContactPerson;
                    result.Email = vendor.Email;
                    result.GstIn = vendor.GstIn;
                    result.Address = vendor.Address;

                    await _context.SaveChangesAsync();

                    return new ResponseResult("Ok", "Updated Successfully");
                }
                return new ResponseResult("Fail", string.Join(",", error));

            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
    }
}
