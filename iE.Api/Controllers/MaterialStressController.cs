using Microsoft.AspNetCore.Mvc;
using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;

namespace iE.Api.Controllers

{
    [ApiController]
    [Route("api/material-stress")]
    public class MaterialStressController : ControllerBase
    {
        private readonly MaterialStressService _service;

        public MaterialStressController(MaterialStressService service)
        {
            _service = service;
        }

        [HttpPost("lookup")]
        public ActionResult<StressLookupResult> Lookup([FromBody] StressLookupRequest request)
        {
            try
            {
                var result = _service.Library.Lookup(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("stats")]
        public ActionResult<object> Stats()
        {
            return Ok(_service.Library.GetStatistics());
        }
        
    }
}