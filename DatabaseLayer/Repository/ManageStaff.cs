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
    public class ManageStaff : IStaff
    {
        private readonly ApplicationDbContext _context;
        public ManageStaff(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> AddStaff(Staff staff)
        {
            try
            {
                if (staff == null)
                {
                    return new ResponseResult("Fail", "Please Fill All Details");
                }
                List<string> error = new List<string>();
                bool orgExists = await _context.OrganizationMaster.AnyAsync(o => o.Id == staff.OrganizationId);
                bool mosExists = await _context.Mosques.AnyAsync(o => o.Id == staff.MosqueId);
                bool OffExists = await _context.OfficeStaffs.AnyAsync(o => o.Id == staff.OfficeStaffId);
                var data = await _context.Staffs.ToListAsync();
                if (!orgExists)
                {
                    error.Add("Invalid Organization. Organization does not exist.");
                }
                if (!mosExists)
                {
                    error.Add("Invalid Mosque. Mosque does not exist.");
                }
                if (!OffExists)
                {
                    error.Add("Invalid OfficeStaff. Office Staff does not exist.");
                }
                if (data.Any(x => x.AadharNo == staff.AadharNo))
                {
                    error.Add("Aadhar Number Already Exist");
                }
                if (data.Any(x => x.ContactNo == staff.ContactNo))
                {
                    error.Add("Aadhar Number Already Exist");
                }

                if (error.Count == 0)
                {
                    _context.Staffs.Add(staff);
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

        public async Task<ResponseResult> GetStaffById(int Id)
        {
            try
            {
                var result = await _context.Staffs.Select(x => new
                {
                   x.Id,
                   x.MosqueId,
                   x.OrganizationId,
                   x.FullName,
                   x.DateOfJoining,
                   x.Address,
                   x.AlternateNo,
                   x.AadharNo,
                   x.VillageTown,
                   x.Pincode,
                   x.Salary,
                   x.StaffType,
                   x.Description,
                   x.CreatedAt,
                   x.OfficeStaffId,
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

        public async Task<ResponseResult> GetStaffList()
        {
            try
            {
                var result = await _context.Staffs.Select(x => new
                {
                    x.Id,
                    x.MosqueId,
                    x.OrganizationId,
                    x.FullName,
                    x.Address,
                    x.AadharNo,
                    x.ContactNo,
                    x.AlternateNo,
                    x.VillageTown,
                    x.Pincode,
                    x.Salary,
                    x.StaffType,
                    x.DateOfJoining,
                    x.Description,
                    x.CreatedAt,
                    x.OfficeStaffId,
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

        public async Task<ResponseResult> UpdateStaff(int Id, Staff staff)
        {
            try
            {
                List<string> errors = new List<string>();

                bool orgExists = await _context.OrganizationMaster.AnyAsync(o => o.Id == staff.OrganizationId);
                bool mosExists = await _context.Mosques.AnyAsync(o => o.Id == staff.MosqueId);
                var result = await _context.Staffs.FirstOrDefaultAsync(x => x.Id == Id);

                if (result == null)
                    return new ResponseResult("Fail", "Organization not found");

                if (!orgExists)
                {
                    errors.Add("Invalid Organization. Organization does not exist.");
                }
                if (!mosExists)
                {
                    errors.Add("Invalid Mosque. Mosque does not exist.");
                }
                if (await _context.Staffs.AnyAsync(x => x. AadharNo== staff.AadharNo && x.Id != Id))
                {
                    errors.Add("Aadhaar Name already exists");
                }

                if (await _context.Staffs.AnyAsync(x => x.ContactNo == staff.ContactNo && x.Id != Id))
                {
                    errors.Add("Contact number already exists");
                }

                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }

                // ✅ update
                result.MosqueId = staff.MosqueId;
                result.OrganizationId = staff.OrganizationId;
                result.FullName = staff.FullName;
                result.Address = staff.Address;
                result.AadharNo = staff.AadharNo;
                result.ContactNo = staff.ContactNo;
                result.AlternateNo = staff.AlternateNo;
                result.VillageTown = staff.VillageTown;
                result.Pincode = staff.Pincode;
                result.Salary = staff.Salary;
                result.StaffType = staff.StaffType;
                result.Description = staff.Description;
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
