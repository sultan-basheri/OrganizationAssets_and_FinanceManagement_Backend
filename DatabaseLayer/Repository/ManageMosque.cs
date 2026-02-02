using BusinessLayer.Interface;
using BusinessLayer.Model;
using DatabaseLayer.ApplicationContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DatabaseLayer.Repository
{
    public class ManageMosque : IMosque
    {
        private readonly ApplicationDbContext _context;
        public ManageMosque(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseResult> AddMosque(Mosque mosque)
        {
            try
            {
                if (mosque == null)
                    return new ResponseResult("Fail", "Please fill all the fields");

                List<string> errors = new();

                //  Organization must exist
                bool orgExists = await _context.OrganizationMaster.AnyAsync(o => o.Id == mosque.OrganizationId);

                if (!orgExists)
                    errors.Add("Invalid Organization. Organization does not exist.");

                //  Mosque exists in another organization 
                bool mosqueExistsInOtherOrg = await _context.Mosques.AnyAsync(m =>  m.Name == mosque.Name && m.OrganizationId != mosque.OrganizationId);

                if (mosqueExistsInOtherOrg)
                    errors.Add("This mosque is already assigned to another organization.");

                //  Duplicate mosque in same organization 
                bool duplicateInSameOrg = await _context.Mosques.AnyAsync(m =>m.Name == mosque.Name && m.OrganizationId == mosque.OrganizationId);

                if (duplicateInSameOrg)
                    errors.Add("Mosque already exists in this organization.");

                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }
                await _context.Mosques.AddAsync(mosque);
                await _context.SaveChangesAsync();

                return new ResponseResult("Ok", "Mosque added successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult("Fail", ex.Message);
            }
        }

        public async Task<ResponseResult> GetList()
        {
            try
            {
                var result = await _context.Mosques.ToListAsync();
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

        public async Task<ResponseResult> GetMosqueById(int Id)
        {
            try
            {
                var result = await _context.Mosques.FirstOrDefaultAsync(x => x.Id == Id);
                if (result == null)
                {
                    return new ResponseResult("Fail", "Mosque Not Exist");
                }
                return new ResponseResult("Ok", result);
            }
            catch (Exception exp)
            {
                return new ResponseResult("Fail", exp.Message);
            }

        }

        public async Task<ResponseResult> GetMosqueList()
        {
            var result = await _context.Mosques.Select(x => new
            {
                x.OrganizationId,
                x.Name,
                x.Address,
                x.StreetName,
                x.Area,
                x.MosqueImam,
                x.ContactNo,
                x.EstablishedDate,
                x.Established_Hijri,
                x.MosqueType,
            }).ToListAsync();
            if (result == null || !result.Any())
            {
                return new ResponseResult("Fail", "Empty");
            }
            return new ResponseResult("Ok", result);
        }

        public async Task<ResponseResult> UpdateMosque(int Id, Mosque mosque)
        {
            try
            {
                List<string> errors = new List<string>();

                var existing = await _context.Mosques.FirstOrDefaultAsync(x => x.Id == Id);
                bool orgExists = await _context.OrganizationMaster.AnyAsync(o => o.Id == mosque.OrganizationId);


                if (existing == null)
                {
                    return new ResponseResult("Fail", "Mosque not found");
                }
                if (!orgExists)
                {
                    errors.Add("Invalid Organization. Organization does not exist.");
                }
                bool existsInOtherOrg = await _context.Mosques.AnyAsync(m => m.Name == mosque.Name && m.OrganizationId != existing.OrganizationId);

                if (existsInOtherOrg)
                    errors.Add("This mosque is already assigned to another organization.");

                //  Duplicate mosque in SAME organization
                bool duplicateInSameOrg = await _context.Mosques.AnyAsync(m =>m.Name == mosque.Name && m.OrganizationId == existing.OrganizationId && m.Id != Id);

                if (duplicateInSameOrg)
                    errors.Add("Mosque name already exists in this organization.");
                if (errors.Count > 0)
                {
                    return new ResponseResult("Fail", string.Join(" | ", errors));
                }

                // ✅ update
                existing.OrganizationId = mosque.OrganizationId;
                existing.Address = mosque.Address;
                existing.StreetName = mosque.StreetName;
                existing.Area = mosque.Area;
                existing.MosqueImam = mosque.MosqueImam;
                existing.ContactNo = mosque.ContactNo;
                existing.MosqueType = mosque.MosqueType;

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
