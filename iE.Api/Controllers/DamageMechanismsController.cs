using Microsoft.AspNetCore.Mvc;
using iE.Core.DamageMechanisms.Repositories;

namespace iE.Api.Controllers
{
    [ApiController]
    [Route("api/damage-mechanisms")]
    public class DamageMechanismsController : ControllerBase
    {
        [HttpGet("search")]
        public IActionResult Search(string? q = "")
        {
            var results = DamageMechanismRepository.SearchMechanisms(q ?? "");
            return Ok(results);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var result = DamageMechanismRepository.GetMechanism(name);

            if (result == null)
                return NotFound(new { message = $"Damage mechanism '{name}' not found." });

            return Ok(result);
        }
    }
}