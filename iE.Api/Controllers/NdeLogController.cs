using iE.Api.Workflow;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/nde/log")]
public sealed class NdeLogController(INdeLogReadModelService ndeLogReadModelService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<NdeLogItemReadModel>> GetLogItems()
        => Ok(ndeLogReadModelService.GetLogItems());

    [HttpPost("{id}/transition")]
    public ActionResult<NdeLogItemReadModel> Transition(string id, [FromBody] NdeLogTransitionCommand command)
    {
        var actor = string.IsNullOrWhiteSpace(command.Actor) ? "demo.user" : command.Actor;
        var result = ndeLogReadModelService.Transition(id, new NdeLogTransitionRequest(command.NextStatus, command.Comment, actor));
        if (!result.Success || result.Item is null)
        {
            return BadRequest(new { reasonCode = result.ReasonCode });
        }

        return Ok(result.Item);
    }
}

public sealed record NdeLogTransitionCommand(string NextStatus, string? Comment, string? Actor);
