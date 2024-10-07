using AutoMapper;
using HttpExceptions.Exceptions;
using Microsoft.EntityFrameworkCore;
using PhotoRater.Models;
using PhotoRater.Models.Directory;
using PhotoRater.Utils;
using PhotoRater.Utils.Feedback;

namespace PhotoRater.Services;

public class FeedbackService: BaseService
{
    public FeedbackService(BaseApplicationContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor) : base(db, mapper, webHostEnvironment, httpContextAccessor)
    {
    }
    
    public async Task<Feedback> CreateFeedback(int photoOnRateId, CreateFeedbackDTO dto)
    {
        await ValidateFeedback(photoOnRateId, dto);
        var tags = await ValidateTags(dto.Tags);
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var reviewer = await _db.Users.FindAsync(userId);
        if (reviewer == null)
        {
            throw new NotFoundException("User is not found");
        }
        var feedback = _mapper.Map<Feedback>(dto);
        feedback.ReviewerId = userId;
        feedback.PhotoOnRateId = photoOnRateId;
        feedback.Tags = tags;
        await _db.Feedbacks.AddAsync(feedback);
        if (reviewer.Karma <= User.MaxKarma)
        {
            reviewer.Karma += User.KarmaQuant;
        }
        var photo = await _db.PhotosOnRate.Include(p => p.User).FirstAsync(p => p.Id == photoOnRateId);
        var user = photo.User;
        if (user.Karma >= User.MinKarma)
        {
            user.Karma -= User.KarmaQuant;
        }

        await _db.SaveChangesAsync();
        return feedback;
    }
    
    public async Task<PhotoOnRateFeedbackDTO> GetNextPhotoForRate()
    {
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var photo = await (from p in _db.PhotosOnRate
            join u in _db.Users on p.UserId equals u.Id
            join f in _db.Feedbacks on
                p.Id equals f.PhotoOnRateId into grouping
            from f in grouping.DefaultIfEmpty()
            where f.ReviewerId != userId && p.UserId != userId && u.Karma > User.MinKarma && p.StatusId == (int)Status.Values.OnReview
            select p).OrderBy(r => Guid.NewGuid()).FirstOrDefaultAsync();
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

    private async Task<List<Tag>> ValidateTags(int[] tagIds)
    {
        var tags = await _db.Tags.Where(t => tagIds.Contains(t.Id)).ToListAsync();
        return tags;
    }
}