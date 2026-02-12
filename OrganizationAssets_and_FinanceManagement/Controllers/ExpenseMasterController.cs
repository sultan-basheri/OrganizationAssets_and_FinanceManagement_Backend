using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseMasterController : ControllerBase
    {
        private readonly IExpenseMaster _expenseMaster;
        public ExpenseMasterController(IExpenseMaster expenseMaster)
        {
            _expenseMaster = expenseMaster;
        }
        [HttpGet]
        public async Task<IActionResult> getExpenseMasterList()
        {
            try
            {
                var result = await _expenseMaster.getExpenseMasterList();
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
        public async Task<IActionResult> getExpenseMasterById(int Id)
        {
            try
            {
                var result = await _expenseMaster.getExpenseMasterById(Id);
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
        public async Task<IActionResult> addExpenseMaster(ExpensesMaster expensesMaster)
        {
            try
            {
                if (expensesMaster == null)
                {
                    return BadRequest("Please Fill All Details");
                }
                var result = await _expenseMaster.addExpenseMaster(expensesMaster);
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
        public async Task<IActionResult> updateExpenseMaster(int Id, ExpensesMaster expensesMaster)
        {
            try
            {
                if (Id != expensesMaster.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _expenseMaster.updateExpenseMaster(Id, expensesMaster);
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
