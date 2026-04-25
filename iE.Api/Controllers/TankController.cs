using Microsoft.AspNetCore.Mvc;
using iE.Core.Equipment.Tanks.Data;
using iE.Core.Equipment.Tanks.Models;
using iE.Core.Equipment.Tanks.Services;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/tanks")]
    public class TankController : ControllerBase
    {
        private readonly TankShellService _service = new();

        [HttpGet("materials")]
        public IActionResult GetMaterials()
        {
            return Ok(TankMaterialRepository.GetAll());
        }

        [HttpPost("shell")]
        public IActionResult CalculateShell([FromBody] TankShellInput input)
        {
            return Ok(_service.Calculate(input));
        }
    }
}