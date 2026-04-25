using Microsoft.AspNetCore.Mvc;
using iE.Core.MaterialStress.Models;
using iE.Core.MaterialStress.Services;
using iE.Core.MaterialStress.Importers;

namespace iE.Api.Controllers

{
    [ApiController]
    [Route("api/material-stress")]
    public class MaterialStressController : ControllerBase
    {
        private static readonly MaterialStressService _service;

        // Static constructor → runs once when API starts
        static MaterialStressController()
        {
            _service = new MaterialStressService();

            // Existing
            NewStressDataImporter.ImportAll(_service);
            OldStressDataImporter.ImportAll(_service);

            // 🔥 ADD THIS
            B313StressImporter.Import(_service);
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