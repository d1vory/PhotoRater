using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoRater.DTO;
using PhotoRater.Services;
using PhotoRater.Utils;
using PhotoRater.Utils.Feedback;

namespace PhotoRater.Controllers;

[Route("photo_on_rate")]
public class PhotoOnRateController: ControllerBase
{
    private readonly PhotoOnRateService _service;
    private readonly FeedbackService _feedbackService;

    public PhotoOnRateController(PhotoOnRateService service, FeedbackService feedbackService)
    {
        _service = service;
        _feedbackService = feedbackService;
    }

    [HttpGet]
    [Authorize]
    [Route("")]
    public async Task<ActionResult<ListPhotoOnRateDTO>> MyPhotosList()
    {
        var photos = await _service.GetMyPhotosList();
        return Ok(photos);
    }
    
    [HttpGet]
    [Authorize]
    [Route("{photoId}")]
    public async Task<ActionResult<ListPhotoOnRateDTO>> MyPhotoDetail([FromRoute] int photoId)
    {
        var photo = await _service.GetPhotoDetail(photoId);
        return Ok(photo);
    }
    
    
    [HttpPost]
    [Authorize]
    [Route("upload")]
    public async Task<ActionResult> UploadPhoto([FromForm] CreatePhotoOnRateDTO dto, IFormFile? image)
    {
        if (image == null)
        {
            return BadRequest("Image is not provided!");
        }

        var photoOnRate = await _service.CreatePhotoOnRate(dto, image);
        //TODO return dto!
        return Ok(photoOnRate);
    }


    [HttpPost]
    [Authorize]
    [Route("{photoId}/rate")]
    public async Task<ActionResult> RatePhoto([FromRoute] int photoId, [FromBody] CreateFeedbackDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.StringifyModelErrors());
        await _feedbackService.CreateFeedback(photoId, dto);
        return Ok("success!");
    }
}