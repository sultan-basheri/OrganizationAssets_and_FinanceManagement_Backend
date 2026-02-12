using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasePaymentController : ControllerBase
    {
        private readonly IPurchasePayment _purchasePayment;
        public PurchasePaymentController(IPurchasePayment purchasePayment)
        {
            _purchasePayment = purchasePayment;
        }
        [HttpGet]
        public async Task<IActionResult> getPurchasePaymentList()
        {
            try
            {
                var result = await _purchasePayment.getPurchasePaymentList();
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
        public async Task<IActionResult> getPurchasePaymentById(int Id)
        {
            try
            {
                var result = await _purchasePayment.getPurchasePaymentDetailById(Id);
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
        public async Task<IActionResult> addPurchasePayment(PurchasePayment purchasePayment)
        {
            try
            {
                if (purchasePayment == null)
                {
                    return BadRequest("Please Fill All Details");
                }
                var result = await _purchasePayment.addPurchasePayment(purchasePayment);
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
        public async Task<IActionResult> updatePurchasePayment(int Id, PurchasePayment purchasePayment)
        {
            try
            {
                if (Id != purchasePayment.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _purchasePayment.updatePurchasePaymentDetails(Id, purchasePayment);
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
