using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendor _vendor;
        public VendorController(IVendor vendor)
        {
            _vendor = vendor;
        }
        [HttpGet]
        public async Task<IActionResult> getVendorList()
        {
            try
            {
                var result = await _vendor.getVendorList();
                if (result.Status.ToLower() == "ok")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception exp)
            {
                return StatusCode(500, new ResponseResult("Fail", exp.Message));
            }
        }
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> getVendorById(int Id)
        {
            try
            {
                var result = await _vendor.getVendorById(Id);
                if (result.Status.ToLower() == "ok")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception exp)
            {
                return StatusCode(500, new ResponseResult("Fail", exp.Message));
            }
        }
        [HttpPost]
        public async Task<IActionResult> addVendor(Vendor vendor)
        {
            try
            {
                if (vendor == null)
                {
                    return BadRequest("Please Fill All Details");
                }
                var result = await _vendor.addVendor(vendor);
                if (result.Status.ToLower() == "ok")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception exp)
            {
                return StatusCode(500, new ResponseResult("Fail", exp.Message));
            }
        }
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> updateVendor(int Id, Vendor vendor)
        {
            try
            {
                if (Id != vendor.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _vendor.updateVendor(Id, vendor);
                if (result.Status.ToLower() == "ok")
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception exp)
            {
                return StatusCode(500, new ResponseResult("Fail", exp.Message));
            }
        }
    }
}
