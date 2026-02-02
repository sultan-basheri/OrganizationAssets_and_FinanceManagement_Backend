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
        public async Task<IActionResult> GetList()
        {
            try
            {
                var result = await _organization.getList();
                if(result.Status.ToLower() == "ok")
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
        [HttpGet("getOrganizationList")]
        public async Task<IActionResult> GetOrganizationList()
        {
            try
            {
                var result = await _organization.getOrganizationList();
                if( result.Status.ToLower() == "ok")
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
                if(result.Status.ToLower() == "ok")
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
                // Unique filename

                if (organization == null)
                {
                    return BadRequest(new
                    {
                        Status  = "Fail",
                        Result = "Model is Empty"
                    });
                }

                string extension = organization!.docType.ToLower() == "pdf" ? ".pdf" : ".png";
                string fileName = $"{Guid.NewGuid()}{extension}";

                organization.docUrl = $"/Documents/{fileName}";

                var result = await _organization.addOrganization(organization);

                if (result.Status.ToLower() == "ok")
                {
                    DocumentUploadClass duc = new DocumentUploadClass();
                    await duc.SaveBase64DocumentAsync(fileName, organization.base64Data, organization!.docType);
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
        public async Task<IActionResult> updateOrganization(int Id,Organization organization)
        {
            try
            {
                if(Id != organization.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _organization.updateOrganization(Id,organization);
                if(result.Status.ToLower() == "ok")
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
