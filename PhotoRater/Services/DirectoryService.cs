using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoRater.Models.Directory;
using PhotoRater.Utils.Directory;

namespace PhotoRater.Services;

public class DirectoryService:BaseService
{
    public DirectoryService(BaseApplicationContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) : base(db, mapper, webHostEnvironment, httpContextAccessor)
    {
    }


    public async Task<List<StatusDTO>> GetStatuses()
    {
        var data =  _db.Statuses.ProjectToList<StatusDTO>(_mapper.ConfigurationProvider);
        return data;
    }

    public async Task<IEnumerable<TagDTO>> GetTags()
    {
        var data = _db.Tags.ProjectToList<TagDTO>(_mapper.ConfigurationProvider);
        
        return data;
    }
}