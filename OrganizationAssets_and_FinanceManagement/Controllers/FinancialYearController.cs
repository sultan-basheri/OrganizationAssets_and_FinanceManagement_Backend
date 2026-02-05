using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialYearController : ControllerBase
    {
        private readonly IFinancialYear _financialYear;
        public FinancialYearController(IFinancialYear financialYear)
        {
            _financialYear = financialYear;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetFinancialList()
        {
            try
            {
                var result = await _financialYear.GetFinancialYearList();
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
        public async Task<IActionResult> GetFinancialYearById(int Id)
        {
            try
            {
                var result = await _financialYear.GetFinancialYearById(Id);
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
        public async Task<IActionResult> addFinancial(FinancialYear financialYear)
        {
            try
            {
                var result = await _financialYear.AddFinancialYear(financialYear);
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
        public async Task<IActionResult> updateFinancial(int Id, FinancialYear financialYear)
        {
            try
            {
                if (Id != financialYear.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _financialYear.UpdateFinancialYear(Id, financialYear);
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
