using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoRater.DTO;
using PhotoRater.Services;

namespace PhotoRater.Controllers;

[Route("photo_on_rate")]
public class PhotoOnRateController: ControllerBase
{
    private readonly PhotoOnRateService _service;

    public PhotoOnRateController(PhotoOnRateService service)
    {
        _service = service;
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
}