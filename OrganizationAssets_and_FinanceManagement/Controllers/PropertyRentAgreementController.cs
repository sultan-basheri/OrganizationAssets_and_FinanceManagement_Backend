using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationAssets_and_FinanceManagement.Repositories;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyRentAgreementController : ControllerBase
    {
        private readonly IPropertyRentAgreement _propertyRent;
        public PropertyRentAgreementController(IPropertyRentAgreement propertyRent)
        {
            _propertyRent = propertyRent;
        }
        [HttpGet]
        public async Task<IActionResult> GetPRAgreementList()
        {
            try
            {
                var result = await _propertyRent.GetPRAgreementList();
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
        public async Task<IActionResult> GetPRAgreementById(int Id)
        {
            try
            {
                var result = await _propertyRent.GetPRAgreementById(Id);
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
        public async Task<IActionResult> addPRAgreement(PropertyRentAgreement propertyRent)
        {
            try
            {
                if (propertyRent == null)
                {
                    return BadRequest(new
                    {
                        Status = "Fail",
                        Result = "Model is Empty"
                    });
                }

                //  Only if base64Data is provided then generate filename + upload
                if (!string.IsNullOrWhiteSpace(propertyRent.base64Data))
                {
                    string extension = propertyRent.DocExtension.ToLower() == "pdf" ? ".pdf" : ".png";
                    string fileName = $"{Guid.NewGuid()}{extension}";

                    propertyRent.DocUrl = $"/Documents/{fileName}";
                }
                else
                {
                    // base64 empty => docUrl/docType null
                    propertyRent.DocUrl = null;
                    propertyRent.DocExtension = null;
                }

                var result = await _propertyRent.AddPRAgreement(propertyRent);

                if (result.Status.ToLower() == "ok")
                {
                    // ✅ Upload only when base64 exists
                    if (!string.IsNullOrWhiteSpace(propertyRent.base64Data))
                    {
                        string fileName = System.IO.Path.GetFileName(propertyRent.DocUrl);

                        DocumentUploadClass duc = new DocumentUploadClass();
                        await duc.SaveBase64DocumentAsync(fileName, propertyRent.base64Data, propertyRent.DocExtension);
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
        public async Task<IActionResult> updatePRAgreement(int Id, PropertyRentAgreement propertyRent)
        {
            try
            {
                if (Id != propertyRent.Id)
                {
                    return BadRequest("Id Mismatch");
                }

                // ✅ old organization entity
                var oldOrg = await _propertyRent.getPRAgreementEntityById(Id);
                if (oldOrg == null)
                {
                    return NotFound(new ResponseResult("Fail", "Organization not found"));
                }

                // ✅ base64 empty => docUrl/docType same
                if (string.IsNullOrWhiteSpace(propertyRent.base64Data))
                {
                    propertyRent.DocUrl = oldOrg.DocUrl;
                    propertyRent.DocExtension = oldOrg.DocExtension;
                }
                else
                {
                    // 1) old file delete
                    if (!string.IsNullOrWhiteSpace(oldOrg.DocUrl))
                    {
                        string oldFileName = Path.GetFileName(oldOrg.DocUrl);
                        string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents", oldFileName);

                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // 2) new filename generate
                    string extension = propertyRent.DocExtension.ToLower() == "pdf" ? ".pdf" : ".png";
                    string fileName = $"{Guid.NewGuid()}{extension}";

                    // 3) new docUrl set
                    propertyRent.DocUrl = $"/Documents/{fileName}";

                    // 4) save new file
                    DocumentUploadClass duc = new DocumentUploadClass();
                    await duc.SaveBase64DocumentAsync(fileName, propertyRent.base64Data, propertyRent.DocExtension);
                }

                // DB update
                var result = await _propertyRent.UpdatePRAgreement(Id, propertyRent);

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
