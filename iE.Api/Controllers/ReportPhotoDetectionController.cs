using iE.Core.Reports.Photos;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/reports/photos")]
public class ReportPhotoDetectionController(IPhotoDetectionService photoDetectionService) : ControllerBase
{
    [HttpPost("detect")]
    public ActionResult<PhotoDetectionResult> Detect([FromBody] PhotoDetectionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.PhotoId))
        {
            return BadRequest(new { error = "photoId is required." });
        }

        var result = photoDetectionService.Detect(request);
        return Ok(result);
    }
}
