using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeStaffController : ControllerBase
    {
        private readonly IOfficeStaff _officeStaff;
        public OfficeStaffController(IOfficeStaff officeStaff)
        {
            _officeStaff = officeStaff;
        }
        [HttpGet]
        public async Task<IActionResult> getOfficeStaffList()
        {
            try
            {
                var result = await _officeStaff.getOfficeStaffList();
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
        [HttpGet("{id:int}")]
        public async Task<IActionResult> getOfficeStaffDetail(int Id)
        {
            try
            {
                var result = await _officeStaff.officeStaffProfile(Id);
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
        public async Task<IActionResult> signUpOfficeStaff(OfficeStaff officeStaff)
        {
            try
            {

                var result = await _officeStaff.signUpofficeStaff(officeStaff);
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
        [HttpPost("AdminAuth")]
        public async Task<IActionResult> officeStafAuthentication(Authentication authentication)
        {
            try
            {
                var result = await _officeStaff.officeStaffAuthentication(authentication);
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
        public async Task<IActionResult> changeProfile(int Id, OfficeStaff officeStaff)
        {
            try
            {
                if (Id != officeStaff.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _officeStaff.changeProfile(Id, officeStaff);
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
        [HttpPut("deactive-admin/{Id:int}")]
        public async Task<IActionResult> deactivateOfficeStaff(int Id)
        {
            try
            {
                var result = await _officeStaff.deactivateofficeStaff(Id);
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
        [HttpPut("updatePassword")]
        public async Task<IActionResult> updatePassword(Authentication authentication)
        {
            try
            {
                var result = await _officeStaff.updatePassword(authentication);
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
