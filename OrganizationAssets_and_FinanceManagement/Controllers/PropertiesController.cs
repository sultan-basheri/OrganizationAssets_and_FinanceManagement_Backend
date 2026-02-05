using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationAssets_and_FinanceManagement.Repositories;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IProperties _properties;
        public PropertiesController(IProperties properties)
        {
            _properties = properties;
        }

        [HttpGet]
        public async Task<IActionResult> getPropertyList()
        {
            try
            {
                var result = await _properties.GetPropertyList();
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
        public async Task<IActionResult> GetPropertyById(int Id)
        {
            try
            {
                var result = await _properties.GetPropertyById(Id);
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
        public async Task<IActionResult> addProperties(Properties properties)
        {
            try
            {
                if (properties == null)
                {
                    return BadRequest(new
                    {
                        Status = "Fail",
                        Result = "Model is Empty"
                    });
                }

                //  Only if base64Data is provided then generate filename + upload
                if (!string.IsNullOrWhiteSpace(properties.base64Data))
                {
                    string extension = properties.DocType.ToLower() == "pdf" ? ".pdf" : ".png";
                    string fileName = $"{Guid.NewGuid()}{extension}";

                    properties.DocUrl = $"/Documents/{fileName}";
                }
                else
                {
                    // base64 empty => docUrl/docType null
                    properties.DocUrl = null;
                    properties.DocType = null;
                }

                var result = await _properties.AddProperty(properties);

                if (result.Status.ToLower() == "ok")
                {
                    // ✅ Upload only when base64 exists
                    if (!string.IsNullOrWhiteSpace(properties.base64Data))
                    {
                        string fileName = System.IO.Path.GetFileName(properties.DocUrl);

                        DocumentUploadClass duc = new DocumentUploadClass();
                        await duc.SaveBase64DocumentAsync(fileName, properties.base64Data, properties.DocType);
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
        public async Task<IActionResult> updateProperties(int Id, Properties properties)
        {
            try
            {
                if (Id != properties.Id)
                {
                    return BadRequest("Id Mismatch");
                }

                // ✅ old Property entity
                var oldOrg = await _properties.getPropertyEntityById(Id);
                if (oldOrg == null)
                {
                    return NotFound(new ResponseResult("Fail", "Organization not found"));
                }

                // ✅ base64 empty => docUrl/docType same
                if (string.IsNullOrWhiteSpace(properties.base64Data))
                {
                    properties.DocUrl = oldOrg.DocUrl;
                    properties.DocType = oldOrg.DocType;
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
                    string extension = properties.DocType.ToLower() == "pdf" ? ".pdf" : ".png";
                    string fileName = $"{Guid.NewGuid()}{extension}";

                    // 3) new docUrl set
                    properties.DocUrl = $"/Documents/{fileName}";

                    // 4) save new file
                    DocumentUploadClass duc = new DocumentUploadClass();
                    await duc.SaveBase64DocumentAsync(fileName, properties.base64Data, properties.DocType);
                }

                // DB update
                var result = await _properties.UpdateProperty(Id, properties);

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
