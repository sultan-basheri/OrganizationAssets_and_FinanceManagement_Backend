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
    public class ManageOrganization : IOrganization
    {
        private readonly ApplicationDbContext _context;
        public ManageOrganization(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task<ResponseResult> addOrganization(Organization organization)
        {
            try
            {
                if(organization == null)
                {
                    return new ResponseResult("Fail", "Please Fill All Details");
                }
                List<string> error = new List<string>();
                var data = await _context.OrganizationMaster.ToListAsync();
                if (data.Any(x => x.Name == organization.Name))
                {
                    error.Add("Organization Name Already Exist");
                }
                if (data.Any(x => x.TrustNo == organization.TrustNo)) 
                {
                    error.Add("Trust Number Already Exist");
                }
                if(error.Count == 0)
                {
                    _context.OrganizationMaster.Add(organization);
                    await _context.SaveChangesAsync();
                    return new ResponseResult("Ok", "Successfully Saved");
                }
                return new ResponseResult("Fail",string.Join(",",error));
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
        public async Task<ResponseResult> getOrganizationById(int Id)
        {
            try
            {
                var result = await _context.OrganizationMaster.Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Address,
                    x.TrustNo,
                    x.City,
                    x.ContactNo,
                    x.AlternateNo,
                    x.Email,
                    x.EstablishedYear,
                    x.CreatedAt,
                    x.docType,
                    x.docUrl
                }).FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", $"Organization Id = {Id} Not Exist");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }
        public async Task<Organization?> getOrganizationEntityById(int id)
        {
            return await _context.OrganizationMaster.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<ResponseResult> getOrganizationList()
        {
            try
            {
                var result = await _context.OrganizationMaster.Select(x=> new
                {
                    x.Name,
                    x.Address,
                    x.TrustNo,
                    x.City,
                    x.ContactNo,
                    x.AlternateNo,
                    x.Email,
                    x.EstablishedYear,
                    x.CreatedAt,
                    x.docType,
                    x.docUrl,
                }).ToListAsync();

                if(result == null)
                {
                    return new ResponseResult("Fail", "Empty");
                }
                return new ResponseResult("Ok",result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }
        }

        public async Task<ResponseResult> updateOrganization(int Id,Organization organization)
        {
            try
            {
                List<string> errors = new List<string>();

                var result = await _context.OrganizationMaster.FirstOrDefaultAsync(x => x.Id == Id);

                if (result == null)
                    return new ResponseResult("Fail", "Organization not found");

                if (await _context.OrganizationMaster
                    .AnyAsync(x => x.Name == organization.Name && x.Id != Id))
                {
                    errors.Add("Organization Name already exists");
                }

                if (await _context.OrganizationMaster
                    .AnyAsync(x => x.TrustNo == organization.TrustNo && x.Id != Id))
                {
                    errors.Add("Trust number already exists");
                }

                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }

                // ✅ update
                result.Name = organization.Name;
                result.Address=organization.Address;
                result.TrustNo = organization.TrustNo;
                result.City= organization.City;
                result.ContactNo = organization.ContactNo;
                result.AlternateNo = organization.AlternateNo;
                result.Email = organization.Email;
                result.EstablishedYear = organization.EstablishedYear;
                result.docType = organization.docType;
                result.docUrl = organization.docUrl;
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
