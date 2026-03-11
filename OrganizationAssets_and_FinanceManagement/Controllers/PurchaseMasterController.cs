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
                    return BadRequest(new { Status = "Fail", Result = "Model is Empty" });
                }

                // 1. Generate Extension and FileName
                if (!string.IsNullOrWhiteSpace(purchaseMaster.base64Data))
                {
                    // Dynamic extension handling (pdf, png, jpg, etc.)
                    string ext = purchaseMaster.DocType.ToLower().Replace(".", "");
                    string extension = $".{ext}";

                    string fileName = $"{Guid.NewGuid()}{extension}";
                    purchaseMaster.BillUrl = $"/Documents/{fileName}";
                }
                else
                {
                    // If no file uploaded
                    purchaseMaster.BillUrl = null;
                    purchaseMaster.DocType = null;
                }

                // 2. Save data to Database first
                var result = await _purchaseMaster.addPurchase(purchaseMaster);

                // 3. If DB save is successful, save physical file
                if (result.Status.ToLower() == "ok")
                {
                    if (!string.IsNullOrWhiteSpace(purchaseMaster.base64Data) && !string.IsNullOrWhiteSpace(purchaseMaster.BillUrl))
                    {
                        string fileName = System.IO.Path.GetFileName(purchaseMaster.BillUrl);

                        // --- INLINE FILE SAVING LOGIC ---
                        string pureBase64 = purchaseMaster.base64Data;
                        if (pureBase64.Contains(","))
                        {
                            pureBase64 = pureBase64.Split(',')[1];
                        }

                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents");

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string filePath = Path.Combine(folderPath, fileName);
                        byte[] fileBytes = Convert.FromBase64String(pureBase64);
                        await System.IO.File.WriteAllBytesAsync(filePath, fileBytes);
                        // --------------------------------
                    }

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
        public async Task<IActionResult> updatePurchase(int Id, PurchaseMaster purchaseMaster) // Notice the method name change to match intent
        {
            try
            {
                if (Id != purchaseMaster.Id)
                {
                    return BadRequest(new { Status = "Fail", Result = "Id Mismatch" });
                }

                // ✅ 1. Fetch old Purchase entity
                var oldPur = await _purchaseMaster.getPurchaseEntityById(Id);
                if (oldPur == null)
                {
                    return NotFound(new { Status = "Fail", Result = "Purchase Detail not found" });
                }

                string oldFilePathToDelete = null;
                string newFileNameToSave = null;
                string pureBase64ToSave = null;

                // ✅ 2. Handle file logic if a new file is uploaded
                if (string.IsNullOrWhiteSpace(purchaseMaster.base64Data))
                {
                    purchaseMaster.BillUrl = oldPur.BillUrl;
                    purchaseMaster.DocType = oldPur.DocType;
                }
                else
                {
                    // Store old path for safe deletion later
                    if (!string.IsNullOrWhiteSpace(oldPur.BillUrl))
                    {
                        string oldFileName = Path.GetFileName(oldPur.BillUrl);
                        oldFilePathToDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents", oldFileName);
                    }

                    // Generate new filename with dynamic extension
                    string ext = purchaseMaster.DocType.ToLower().Replace(".", "");
                    string extension = $".{ext}";
                    newFileNameToSave = $"{Guid.NewGuid()}{extension}";

                    purchaseMaster.BillUrl = $"/Documents/{newFileNameToSave}";

                    // Clean the Base64 string to avoid corrupt files
                    pureBase64ToSave = purchaseMaster.base64Data;
                    if (pureBase64ToSave.Contains(","))
                    {
                        pureBase64ToSave = pureBase64ToSave.Split(',')[1];
                    }
                }

                // ✅ 3. Update Database first (Safe approach)
                var result = await _purchaseMaster.updatePurchase(Id, purchaseMaster);

                if (result.Status.ToLower() == "ok")
                {
                    // ✅ 4. Manage physical files only after successful DB update
                    if (!string.IsNullOrWhiteSpace(pureBase64ToSave) && !string.IsNullOrWhiteSpace(newFileNameToSave))
                    {
                        // A) Delete old file
                        if (!string.IsNullOrWhiteSpace(oldFilePathToDelete) && System.IO.File.Exists(oldFilePathToDelete))
                        {
                            System.IO.File.Delete(oldFilePathToDelete);
                        }

                        // B) Save new file inline
                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string newFilePath = Path.Combine(folderPath, newFileNameToSave);
                        byte[] fileBytes = Convert.FromBase64String(pureBase64ToSave);

                        await System.IO.File.WriteAllBytesAsync(newFilePath, fileBytes);
                    }

                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception exp)
            {
                return StatusCode(500, new { Status = "Fail", Result = exp.Message });
            }
        }
    }
}
