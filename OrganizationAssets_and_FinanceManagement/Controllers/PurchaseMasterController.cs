using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationAssets_and_FinanceManagement.Repositories;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseMasterController : ControllerBase
    {
        private readonly IPurchaseMaster _purchaseMaster;
        public PurchaseMasterController(IPurchaseMaster purchaseMaster)
        {
            _purchaseMaster = purchaseMaster;
        }
        [HttpGet]
        public async Task<IActionResult> getPurchaseList()
        {
            try
            {
                var result = await _purchaseMaster.getPurchaseList();
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
                var result = await _purchaseMaster.getPurchaseById(Id);
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
        public async Task<IActionResult> addPurchase(PurchaseMaster purchaseMaster)
        {
            try
            {
                if (purchaseMaster == null)
                {
                    return BadRequest(new
                    {
                        Status = "Fail",
                        Result = "Model is Empty"
                    });
                }

                //  Only if base64Data is provided then generate filename + upload
                if (!string.IsNullOrWhiteSpace(purchaseMaster.base64Data))
                {
                    string extension = purchaseMaster.DocType.ToLower() == "pdf" ? ".pdf" : ".png";
                    string fileName = $"{Guid.NewGuid()}{extension}";

                    purchaseMaster.BillUrl = $"/Documents/{fileName}";
                }
                else
                {
                    // base64 empty => docUrl/docType null
                    purchaseMaster.BillUrl = null;
                    purchaseMaster.DocType = null;
                }

                var result = await _purchaseMaster.addPurchase(purchaseMaster);

                if (result.Status.ToLower() == "ok")
                {
                    // ✅ Upload only when base64 exists
                    if (!string.IsNullOrWhiteSpace(purchaseMaster.base64Data))
                    {
                        string fileName = System.IO.Path.GetFileName(purchaseMaster.BillUrl);

                        DocumentUploadClass duc = new DocumentUploadClass();
                        await duc.SaveBase64DocumentAsync(fileName, purchaseMaster.base64Data, purchaseMaster.DocType);
                    }

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
        public async Task<IActionResult> updateProperties(int Id, PurchaseMaster purchaseMaster)
        {
            try
            {
                if (Id != purchaseMaster.Id)
                {
                    return BadRequest("Id Mismatch");
                }

                // ✅ old Property entity
                var oldPur = await _purchaseMaster.getPurchaseEntityById(Id);
                if (oldPur == null)
                {
                    return NotFound(new ResponseResult("Fail", "Purchase Detail not found"));
                }

                // ✅ base64 empty => docUrl/docType same
                if (string.IsNullOrWhiteSpace(purchaseMaster.base64Data))
                {
                    purchaseMaster.BillUrl = oldPur.BillUrl;
                    purchaseMaster.DocType = oldPur.DocType;
                }
                else
                {
                    // 1) old file delete
                    if (!string.IsNullOrWhiteSpace(oldPur.BillUrl))
                    {
                        string oldFileName = Path.GetFileName(oldPur.BillUrl);
                        string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents", oldFileName);

                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // 2) new filename generate
                    string extension = purchaseMaster.DocType.ToLower() == "pdf" ? ".pdf" : ".png";
                    string fileName = $"{Guid.NewGuid()}{extension}";

                    // 3) new docUrl set
                    purchaseMaster.BillUrl = $"/Documents/{fileName}";

                    // 4) save new file
                    DocumentUploadClass duc = new DocumentUploadClass();
                    await duc.SaveBase64DocumentAsync(fileName, purchaseMaster.base64Data, purchaseMaster.DocType);
                }

                // DB update
                var result = await _purchaseMaster.updatePurchaseDetail(Id, purchaseMaster);

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
