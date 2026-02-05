using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaff _staff;
        public StaffController(IStaff staff)
        {
            _staff = staff;
        }
        [HttpGet]
        public async Task<IActionResult> GetStaffList()
        {
            try
            {
                var result = await _staff.GetStaffList();
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
        public async Task<IActionResult> GetStaffById(int Id)
        {
            try
            {
                var result = await _staff.GetStaffById(Id);
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
        public async Task<IActionResult> addStaff(Staff staff)
        {
            try
            {
                var result = await _staff.AddStaff(staff);
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
        public async Task<IActionResult> updateMosque(int Id, Staff staff)
        {
            try
            {
                if (Id != staff.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _staff.UpdateStaff(Id, staff);
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
