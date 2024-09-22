using AutoMapper;
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
        //TODO check whether this photo is liked by this user
        //TODO check whether this photo exist
        //TODO check rating range

        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var feedback = _mapper.Map<Feedback>(dto);
        feedback.ReviewerId = userId;
        feedback.PhotoOnRateId = photoOnRateId;
        await _db.Feedbacks.AddAsync(feedback);
        await _db.SaveChangesAsync();
        return feedback;
    }
    
    
    
}