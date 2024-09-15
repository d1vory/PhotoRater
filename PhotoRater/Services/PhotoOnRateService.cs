using System.Security.Claims;
using AutoMapper;
using PhotoRater.Models;
using PhotoRater.DTO;
using PhotoRater.Utils;

namespace PhotoRater.Services;

public class PhotoOnRateService
{
    private readonly BaseApplicationContext _db;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public PhotoOnRateService(BaseApplicationContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }


    public async Task<PhotoOnRate> CreatePhotoOnRate(CreatePhotoOnRateDTO dto,  IFormFile image)
    {
        
        var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(image.FileName)}";
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        var imagePath = Path.Combine("images", fileName);
        var photoOnRate = _mapper.Map<PhotoOnRate>(dto);
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        photoOnRate.Photo = imagePath;
        photoOnRate.UserId = userId;
        await _db.PhotosOnRate.AddAsync(photoOnRate);
        await _db.SaveChangesAsync();
        return photoOnRate;
    }

    public async Task<IEnumerable<ListPhotoOnRateDTO>> GetMyPhotosList()
    {
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var objects = _db.PhotosOnRate.Where(p => p.UserId == userId);
        //var kek = await objects.ProjectToListAsync<ListPhotoOnRateDTO>(_mapper.ConfigurationProvider);
        var kek2 = objects.ProjectToList<ListPhotoOnRateDTO>(_mapper.ConfigurationProvider);
        return kek2;
    }
}