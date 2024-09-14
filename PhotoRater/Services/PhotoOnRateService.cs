using AutoMapper;
using PhotoRater.Models;
using PhotoRater.DTO;

namespace PhotoRater.Services;

public class PhotoOnRateService
{
    private readonly BaseApplicationContext _db;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PhotoOnRateService(BaseApplicationContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
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
        photoOnRate.Photo = imagePath;
        await _db.PhotosOnRate.AddAsync(photoOnRate);
        await _db.SaveChangesAsync();
        return photoOnRate;
    }
}