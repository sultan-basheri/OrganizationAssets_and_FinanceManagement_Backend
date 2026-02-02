using BusinessLayer.Interface;
using BusinessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrganizationAssets_and_FinanceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MosqueController : ControllerBase
    {
        private readonly IMosque _mosque;
        public MosqueController(IMosque mosque)
        {
            _mosque = mosque;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                
                var result = await _mosque.GetList();
                
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
        [HttpGet("GetMosqueList")]
        public async Task<IActionResult> GetMosqueList()
        {
            try
            {
                var result = await _mosque.GetMosqueList();
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
        public async Task<IActionResult> GetMosqueById(int Id)
        {
            try
            {
                var result = await _mosque.GetMosqueById(Id);
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
        public async Task<IActionResult> addMosque(Mosque mosque)
        {
            try
            {
                var result = await _mosque.AddMosque(mosque);
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
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> updateMosque(int Id,Mosque mosque)
        {
            try
            {
                if(Id != mosque.Id)
                {
                    return BadRequest("Id Mismatch");
                }
                var result = await _mosque.UpdateMosque(Id,mosque);
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
