using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawalSalaryController : ControllerBase
    {
        private readonly IWithdrawalSalary _withdrawal;
        public WithdrawalSalaryController(IWithdrawalSalary withdrawal)
        {
            _withdrawal = withdrawal;
        }
        [HttpGet]
        public async Task<IActionResult> getWithdrawalSalaryList()
        {
            try
            {
                var result = await _withdrawal.getWithdrawalSalaryList();
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
        public async Task<IActionResult> getWithdrawalSalaryById(int Id)
        {
            try
            {
                var result = await _withdrawal.getWithdrawalSalaryById(Id);
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
        public async Task<IActionResult> addWithdrawalSalaryMaster(WithdrawalSalary withdrawal)
        {
            try
            {
                if (withdrawal == null)
                {
                    return BadRequest("Please Fill All Details");
                }
                var result = await _withdrawal.addWithdrawalSalary(withdrawal);
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
        public async Task<IActionResult> updateWithdrawalSalary(int Id,[FromBody] WithdrawalSalary withdrawal)
        {
            try
            {
                if (Id != withdrawal.Id)
                {
                    return BadRequest("Withdrawal Salary Id Mismatch");
                }
                var result = await _withdrawal.updateWithdrawalSalary(Id, withdrawal);
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
