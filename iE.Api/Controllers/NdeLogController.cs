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
}
