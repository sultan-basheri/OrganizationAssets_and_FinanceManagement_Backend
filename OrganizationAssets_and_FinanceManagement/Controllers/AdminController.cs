using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrganizationAssets_and_FinanceManagement.Repositories;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;
        public AdminController(IAdmin admin)
        {
            _admin = admin;
        }

        [HttpGet]
        public async Task<IActionResult> getAdminList()
        {
            try
            {
                var result = await _admin.getAdminList();
                if(result.Status.ToLower() == "ok")
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
        public async Task<IActionResult> getAdminDetail(int Id)
        {
            try
            {
                var result = await _admin.adminProfile(Id);
                if(result.Status.ToLower() == "ok")
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
        public async Task<IActionResult> signUpAdmin(Admin admin)
        {
            try
            {
                
                var result = await _admin.signUpAdmin(admin);
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
        public async Task<IActionResult> adminAuthentication(Authentication authentication)
        {
            try
            {
                var result = await _admin.adminAuthentication(authentication);
                if(result.Status.ToLower() == "ok")
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
        public async Task<IActionResult> changeProfile(int Id, Admin admin)
        {
            try
            {
                if(Id != admin.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _admin.changeProfile(Id, admin);
                if(result.Status.ToLower() == "ok")
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
        public async Task<IActionResult> deactivateAdmin(int Id)
        {
            try
            {
                var result = await _admin.deactivateAdmin(Id);
                if( result.Status.ToLower() == "ok")
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
                var result = await _admin.updatePassword(authentication);
                if(result.Status.ToLower() == "ok")
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
