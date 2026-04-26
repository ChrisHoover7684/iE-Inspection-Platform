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
        private readonly TankShellService _shellService = new();
        private readonly TankBottomService _bottomService = new();
        private readonly TankSettlementService _settlementService = new();

        [HttpGet("materials")]
        public IActionResult GetMaterials()
        {
            return Ok(TankMaterialRepository.GetAll());
        }

        [HttpPost("shell")]
        public IActionResult CalculateShell([FromBody] TankShellInput input)
        {
            return Ok(_shellService.Calculate(input));
        }

        [HttpPost("bottom")]
        public IActionResult CalculateBottom([FromBody] TankBottomInput input)
        {
            return Ok(_bottomService.Calculate(input));
        }

        [HttpPost("settlement")]
        public IActionResult CalculateSettlement([FromBody] TankSettlementInput input)
        {
            return Ok(_settlementService.Calculate(input));
        }
    }
}
