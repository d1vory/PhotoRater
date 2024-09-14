using AutoMapper;
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
    
    
    [HttpPost]
    [Route("upload")]
    public async Task<ActionResult> UploadPhoto([FromForm] CreatePhotoOnRateDTO dto, IFormFile? image)
    {
        if (image == null)
        {
            return BadRequest("Image is not provided!");
        }

        var photoOnRate = await _service.CreatePhotoOnRate(dto, image);
        return Ok(photoOnRate);
    }
}