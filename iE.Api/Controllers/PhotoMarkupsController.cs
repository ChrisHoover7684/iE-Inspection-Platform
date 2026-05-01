using iE.Api.Contracts;
using iE.Core.Reports;
using iE.Core.Reports.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace iE.Api.Controllers;

[ApiController]
[Route("api/photos/{photoId}/markups")]
public class PhotoMarkupsController(PhotoMarkupRepository photoMarkupRepository) : ControllerBase
{
    [HttpPost]
    public ActionResult<PhotoMarkup> Create(string photoId, [FromBody] CreatePhotoMarkupRequest request)
    {
        if (string.IsNullOrWhiteSpace(photoId))
        {
            return BadRequest(new { error = "photoId is required." });
        }

        try
        {
            var created = photoMarkupRepository.Create(photoId, request.MarkupJson);
            return Created($"/api/photos/{photoId}/markups/{created.Id}", created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public ActionResult<List<PhotoMarkup>> Get(string photoId)
    {
        if (string.IsNullOrWhiteSpace(photoId))
        {
            return BadRequest(new { error = "photoId is required." });
        }

        return Ok(photoMarkupRepository.GetByPhotoId(photoId));
    }
}
