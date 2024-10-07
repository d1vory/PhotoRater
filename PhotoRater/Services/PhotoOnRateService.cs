using System.Security.Claims;
using AutoMapper;
using HttpExceptions.Exceptions;
using PhotoRater.Models;
using PhotoRater.DTO;
using PhotoRater.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PhotoRater.Services;

public class PhotoOnRateService: BaseService
{
    public PhotoOnRateService(BaseApplicationContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) : base(db, mapper, webHostEnvironment, httpContextAccessor)
    {
    }

    public async Task<PhotoOnRate> CreatePhotoOnRate(CreatePhotoOnRateDTO dto,  IFormFile image)
    {
        
        var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(image.FileName)}";
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        var imagePath = $"images/{fileName}";
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
        return objects.ProjectToList<ListPhotoOnRateDTO>(_mapper.ConfigurationProvider);
    }

    public async Task<DetailPhotoOnRateDTO> GetPhotoDetail(int photoId)
    {
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var obj = await _db.PhotosOnRate.FirstOrDefaultAsync(p => p.Id == photoId && p.UserId == userId);
        if (obj == null) throw new NotFoundException("photo is not found!");
        var dto = _mapper.Map<DetailPhotoOnRateDTO>(obj);
        
        var feedbacks = _db.Feedbacks.Include(f => f.Tags).Where(f => f.PhotoOnRateId == obj.Id);
        var maxRating = 0;
        var minRating = 0;
        double averageRating = 0;
        if (feedbacks.Any())
        {
            averageRating = feedbacks.Average(f => f.DigitalRating); 
            maxRating = feedbacks.Max(f => f.DigitalRating); 
            minRating = feedbacks.Min(f => f.DigitalRating);
        }

        var comments = feedbacks.Select(f => f.Comment).ToArray();


        var tagsQuantityQuery = feedbacks.SelectMany(f => f.Tags).GroupBy(t => t.Id).Select(x => new { x.Key, Count = x.Count() });
        var tagsQuantity = tagsQuantityQuery.AsEnumerable().ToDictionary(kvp => kvp.Key, kvp => kvp.Count);

        
        dto.AverageRating = averageRating;
        dto.MaxRating = maxRating;
        dto.MinRating = minRating;
        dto.Comments = comments;
        dto.TagsQuantity = tagsQuantity;
        return dto;
    }


}