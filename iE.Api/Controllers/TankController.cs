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


        [HttpGet("joint-efficiencies")]
        public IActionResult GetJointEfficiencies([FromQuery] double kFactor = 0)
        {
            return Ok(TankJointEfficiencyRepository.GetAll(kFactor));
        }

        [HttpGet("joint-efficiencies/options")]
        public IActionResult GetJointEfficiencyOptions()
        {
            return Ok(TankJointEfficiencyRepository.GetOptions());
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

        [HttpPost("localized-corrosion")]
        public IActionResult CalculateLocalizedCorrosion([FromBody] TankLocalizedCorrosionInput input)
        {
            return Ok(_shellService.CalculateLocalizedCorrosion(input));
        }

        [HttpPost("mrt")]
        public IActionResult CalculateMrt([FromBody] TankMrtInput input)
        {
            try
            {
                return Ok(_bottomService.CalculateMrt(input));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
