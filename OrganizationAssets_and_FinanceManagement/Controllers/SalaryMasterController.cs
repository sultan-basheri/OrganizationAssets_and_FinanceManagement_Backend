using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryMasterController : ControllerBase
    {
        private readonly ISalaryMaster _salaryMaster;
        public SalaryMasterController(ISalaryMaster salaryMaster)
        {
            _salaryMaster = salaryMaster;
        }
        [HttpGet]
        public async Task<IActionResult> getSalaryMasterList()
        {
            try
            {
                var result = await _salaryMaster.getSalaryMasterList();
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
        public async Task<IActionResult> getSalayMasterById(int Id)
        {
            try
            {
                var result = await _salaryMaster.getSalaryMasterById(Id);
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
        public async Task<IActionResult> addSalaryMaster(SalaryMaster salaryMaster)
        {
            try
            {
                if(salaryMaster ==  null)
                {
                    return BadRequest("Please Fill All Details");
                }
                var result = await _salaryMaster.addSalaryMaster(salaryMaster);
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
        public async Task<IActionResult> updateSalaryMaster(int Id, SalaryMaster salaryMaster)
        {
            try
            {
                if (Id != salaryMaster.Id)
                {
                    return BadRequest("Salary Id Mismatch");
                }
                var result = await _salaryMaster.updateSalaryMaster(Id, salaryMaster);
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
