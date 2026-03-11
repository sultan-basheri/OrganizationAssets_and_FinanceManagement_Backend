using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDetailController : ControllerBase
    {
        private readonly IPurchaseDetail _purchaseDetail;
        public PurchaseDetailController(IPurchaseDetail purchaseDetail)
        {
            _purchaseDetail = purchaseDetail;
        }
        [HttpGet]
        public async Task<IActionResult> getPurchaseDetailList()
        {
            try
            {
                var result = await _purchaseDetail.getPurchaseDetailList();
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
        public async Task<IActionResult> GetPurchaseDetailById(int Id)
        {
            try
            {
                var result = await _purchaseDetail.getPurchaseDetailById(Id);
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
        public async Task<IActionResult> addPurchaseDetail(PurchaseDetail purchaseDetail)
        { 
            try
            {
                if (purchaseDetail == null)
                {
                    return BadRequest(new { Status = "Fail", Result = "Model is Empty" });
                }

               var result = await _purchaseDetail.addPurchaseDetail(purchaseDetail);
                if (result.Status.ToLower() == "ok")
                {
                    return Ok(result);
                }
                return BadRequest(result);

            }
            catch (Exception exp)
            {
                return StatusCode(500, new { Status = "Fail", Result = exp.Message });
            }
        }
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> updatePurchasedetail(int Id, PurchaseDetail purchaseDetail)
        {
            try
            {
                if (Id != purchaseDetail.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _purchaseDetail.updatePurchaseDetail(Id, purchaseDetail);
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
