using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

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
                    return BadRequest(new { Status = "Fail", Result = "Model is Empty" });
                }

                if (!string.IsNullOrWhiteSpace(properties.base64Data) && !string.IsNullOrWhiteSpace(properties.DocType))
                {
                    string ext = properties.DocType.ToLower().Replace(".", "");
                    string extension = $".{ext}";

                    string fileName = $"{Guid.NewGuid()}{extension}";
                    properties.DocUrl = $"/Documents/{fileName}";
                }
                else
                {
                    properties.DocUrl = null;
                    properties.DocType = null;
                }

                var result = await _properties.AddProperty(properties);

                if (result.Status.ToLower() == "ok")
                {
                    if (!string.IsNullOrWhiteSpace(properties.base64Data) && !string.IsNullOrWhiteSpace(properties.DocUrl))
                    {
                        string fileName = Path.GetFileName(properties.DocUrl);

                        string pureBase64 = properties.base64Data;
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
        public async Task<IActionResult> updateProperties(int Id, Properties properties)
        {
            try
            {
                if (Id != properties.Id)
                {
                    return BadRequest(new { Status = "Fail", Result = "Id Mismatch" });
                }

                var oldPro = await _properties.getPropertyEntityById(Id);
                if (oldPro == null)
                {
                    return NotFound(new { Status = "Fail", Result = "Property not found" });
                }

                string oldFilePathToDelete = null;
                string newFileNameToSave = null;
                string pureBase64ToSave = null;

                if (string.IsNullOrWhiteSpace(properties.base64Data))
                {
                    properties.DocUrl = oldPro.DocUrl;
                    properties.DocType = oldPro.DocType;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(oldPro.DocUrl))
                    {
                        string oldFileName = Path.GetFileName(oldPro.DocUrl);
                        oldFilePathToDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents", oldFileName);
                    }

                    if (!string.IsNullOrWhiteSpace(properties.DocType))
                    {
                        string ext = properties.DocType.ToLower().Replace(".", "");
                        string extension = $".{ext}";
                        newFileNameToSave = $"{Guid.NewGuid()}{extension}";

                        properties.DocUrl = $"/Documents/{newFileNameToSave}";

                        pureBase64ToSave = properties.base64Data;
                        if (pureBase64ToSave.Contains(","))
                        {
                            pureBase64ToSave = pureBase64ToSave.Split(',')[1];
                        }
                    }
                }

                var result = await _properties.UpdateProperty(Id, properties);

                if (result.Status.ToLower() == "ok")
                {
                    if (!string.IsNullOrWhiteSpace(pureBase64ToSave) && !string.IsNullOrWhiteSpace(newFileNameToSave))
                    {
                        if (!string.IsNullOrWhiteSpace(oldFilePathToDelete) && System.IO.File.Exists(oldFilePathToDelete))
                        {
                            System.IO.File.Delete(oldFilePathToDelete);
                        }

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