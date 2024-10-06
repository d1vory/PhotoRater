using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoRater.Models.Directory;
using PhotoRater.Services;
using PhotoRater.Utils.Directory;

namespace PhotoRater.Controllers;

[Route("directory")]
public class DirectoryController: ControllerBase
{
    private readonly DirectoryService _service;

    public DirectoryController(DirectoryService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]
    [Route("status")]
    public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
    {
        var data = await _service.GetStatuses();
        return Ok(data);
    }
    
    [HttpGet]
    [Authorize]
    [Route("tag")]
    public async Task<ActionResult<IEnumerable<TagDTO>>> GetTags()
    {
        var data = await _service.GetTags();
        return Ok(data);
    }
    
    
}