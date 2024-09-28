using AutoMapper;
using HttpExceptions.Exceptions;
using Microsoft.EntityFrameworkCore;
using PhotoRater.Models;
using PhotoRater.Utils;
using PhotoRater.Utils.Feedback;

namespace PhotoRater.Services;

public class FeedbackService
{
    private readonly BaseApplicationContext _db;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public FeedbackService(BaseApplicationContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Feedback> CreateFeedback(int photoOnRateId, CreateFeedbackDTO dto)
    {
        await ValidateFeedback(photoOnRateId, dto);
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var feedback = _mapper.Map<Feedback>(dto);
        feedback.ReviewerId = userId;
        feedback.PhotoOnRateId = photoOnRateId;
        await _db.Feedbacks.AddAsync(feedback);
        await _db.SaveChangesAsync();
        return feedback;
    }
    
    public async Task<PhotoOnRateFeedbackDTO> GetNextPhotoForRate()
    {
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var photo = await (from p in _db.PhotosOnRate
            join f in _db.Feedbacks on
                p.Id equals f.PhotoOnRateId into grouping
            from f in grouping.DefaultIfEmpty()
            where f.ReviewerId != userId && p.UserId != userId
            select p).OrderBy(r => Guid.NewGuid()).FirstAsync();
        
        if (photo == null) throw new BadRequestException("There are no photos left to rate!");
        var dto = _mapper.Map<PhotoOnRateFeedbackDTO>(photo);
        
        return dto;

    }

    private async Task ValidateFeedback(int photoOnRateId, CreateFeedbackDTO dto)
    {
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var photo = await _db.PhotosOnRate.FirstOrDefaultAsync(p => p.Id == photoOnRateId);
        
        if (photo == null)
        {
            throw new NotFoundException("This photo is not found!");
        }

        if (photo.UserId == userId)
        {
            throw new BadRequestException("Cant feedback own photos!");
        }

        if (await _db.Feedbacks.AnyAsync(f => f.PhotoOnRateId == photoOnRateId && f.ReviewerId == userId))
        {
            throw new BadRequestException("You already reviewed this photo!");
        }
    }
    
}