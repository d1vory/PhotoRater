using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoRater.Services;
using PhotoRater.Utils;
using PhotoRater.Utils.Feedback;

namespace PhotoRater.Controllers;

[Route("feed")]
public class FeedbackController: ControllerBase
{
    private readonly FeedbackService _service;

    public FeedbackController(FeedbackService feedbackService)
    {
        _service = feedbackService;
    }

    [HttpGet]
    [Authorize]
    [Route("next")]
    public async Task<ActionResult<PhotoOnRateFeedbackDTO>> GetNextPhotoForRate()
    {
        var dto = await _service.GetNextPhotoForRate();
        return Ok(dto);
    }
    
    
    [HttpPost]
    [Authorize]
    [Route("{photoId}/rate")]
    public async Task<ActionResult> RatePhoto([FromRoute] int photoId, [FromBody] CreateFeedbackDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.StringifyModelErrors());
        await _service.CreateFeedback(photoId, dto);
        return Ok("success!");
    }
}