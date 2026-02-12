using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IExpenseCategory _expenseCategory;
        public ExpenseCategoryController(IExpenseCategory expenseCategory)
        {
            _expenseCategory = expenseCategory;
        }
        [HttpGet]
        public async Task<IActionResult> getExpenseCategoryList()
        {
            try
            {
                var result = await _expenseCategory.getExpenseCategoryList();
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
        public async Task<IActionResult> getExpenseCategoryById(int Id)
        {
            try
            {
                var result = await _expenseCategory.getExpenseCategoryById(Id);
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
        public async Task<IActionResult> addExpenseCategory(ExpenseCategory expenseCategory)
        {
            try
            {
                if (expenseCategory == null)
                {
                    return BadRequest("Please Fill All Details");
                }
                var result = await _expenseCategory.addExpenseCategory(expenseCategory);
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
        public async Task<IActionResult> updateExpense(int Id, ExpenseCategory expenseCategory)
        {
            try
            {
                if (Id != expenseCategory.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _expenseCategory.updateExpenseCategory(Id, expenseCategory);
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

