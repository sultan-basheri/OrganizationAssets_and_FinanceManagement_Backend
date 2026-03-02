using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationAssets_and_FinanceManagement.Repositories;
using System.Data;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganization _organization;
        public OrganizationController(IOrganization organization)
        {
            _organization = organization;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrganizationList()
        {
            try
            {
                var result = await _organization.getOrganizationList();
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
        public async Task<IActionResult> GetOrganizationById(int Id)
        {
            try
            {
                var result = await _organization.getOrganizationById(Id);
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
        public async Task<IActionResult> addOrganization(Organization organization)
        {
            try
            {
                if (organization == null)
                {
                    return BadRequest(new
                    {
                        Status = "Fail",
                        Result = "Model is Empty"
                    });
                }

                if (!string.IsNullOrWhiteSpace(organization.base64Data))
                {
                    string ext = organization.docType.ToLower().Replace(".", "");
                    string extension = $".{ext}";

                    string fileName = $"{Guid.NewGuid()}{extension}";
                    organization.docUrl = $"/Documents/{fileName}";
                }
                else
                {
                    organization.docUrl = null;
                    organization.docType = null;
                }

                var result = await _organization.addOrganization(organization);

                if (result.Status.ToLower() == "ok")
                {
                    if (!string.IsNullOrWhiteSpace(organization.base64Data) && !string.IsNullOrWhiteSpace(organization.docUrl))
                    {
                        string fileName = System.IO.Path.GetFileName(organization.docUrl);

                        string pureBase64 = organization.base64Data;
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
                // Custom ResponseResult ya anonymous object
                return StatusCode(500, new { Status = "Fail", Result = exp.Message });
            }
        }

        [HttpPut("{Id:int}")]
        public async Task<IActionResult> updateOrganization(int Id, Organization organization)
        {
            try
            {
                if (Id != organization.Id)
                {
                    return BadRequest(new { Status = "Fail", Result = "Id Mismatch" });
                }

                var oldOrg = await _organization.getOrganizationEntityById(Id);
                if (oldOrg == null)
                {
                    return NotFound(new { Status = "Fail", Result = "Organization not found" });
                }

                string oldFilePathToDelete = null;
                string newFileNameToSave = null;
                string pureBase64ToSave = null;

                if (string.IsNullOrWhiteSpace(organization.base64Data))
                {
                    organization.docUrl = oldOrg.docUrl;
                    organization.docType = oldOrg.docType;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(oldOrg.docUrl))
                    {
                        string oldFileName = Path.GetFileName(oldOrg.docUrl);
                        oldFilePathToDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Documents", oldFileName);
                    }

                    string ext = organization.docType.ToLower().Replace(".", "");
                    string extension = $".{ext}";
                    newFileNameToSave = $"{Guid.NewGuid()}{extension}";

                    organization.docUrl = $"/Documents/{newFileNameToSave}";

                    // Base64 string ko clean karein (Prefix hatayein) taaki file corrupt na ho
                    pureBase64ToSave = organization.base64Data;
                    if (pureBase64ToSave.Contains(","))
                    {
                        pureBase64ToSave = pureBase64ToSave.Split(',')[1];
                    }
                }

                var result = await _organization.updateOrganization(Id, organization);

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
