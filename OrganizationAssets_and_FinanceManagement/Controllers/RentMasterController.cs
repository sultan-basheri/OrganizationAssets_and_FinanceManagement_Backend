using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentMasterController : ControllerBase
    {
        private readonly IRentMaster _rentMaster;
        public RentMasterController(IRentMaster rentMaster)
        {
            _rentMaster = rentMaster;
        }
        [HttpGet]
        public async Task<IActionResult> GetRentMasterList()
        {
            try
            {
                var result = await _rentMaster.GetRentMasterList();
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
        public async Task<IActionResult> GetRentMasterById(int Id)
        {
            try
            {
                var result = await _rentMaster.GetRentMasterById(Id);
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
        public async Task<IActionResult> addRentMaster(RentMaster rentMaster)
        {
            try
            {
                var result = await _rentMaster.addRentMaster(rentMaster);
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
        public async Task<IActionResult> updateRentMaster(int Id, RentMaster rentMaster)
        {
            try
            {
                if (Id != rentMaster.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _rentMaster.updateRentMaster(Id, rentMaster);
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
