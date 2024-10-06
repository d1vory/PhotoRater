using AutoMapper;

namespace PhotoRater.Services;

public abstract class BaseService
{
    protected readonly BaseApplicationContext _db;
    protected readonly IMapper _mapper;
    protected readonly IWebHostEnvironment _webHostEnvironment;
    protected readonly IHttpContextAccessor _httpContextAccessor;
    
    public BaseService(BaseApplicationContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
    }
}