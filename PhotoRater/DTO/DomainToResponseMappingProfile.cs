using AutoMapper;
using PhotoRater.Models;
using PhotoRater.DTO;
using PhotoRater.DTO.Auth;
using PhotoRater.Models.Directory;
using PhotoRater.Utils.Directory;
using PhotoRater.Utils.Feedback;

namespace PhotoRater.DTO;


public class DomainToResponseMappingProfile: Profile
{
    public DomainToResponseMappingProfile()
    {
        // CreateMap<Models.OperationType, OperationTypeDto>();
        // CreateMap<CreateOperationTypeDto, Models.OperationType>();
        // CreateMap<UpdateOperationTypeDto, Models.OperationType>();
        CreateMap<RegisterUserDTO, User>();
        CreateMap<User, UserMeDTO>();
        
        CreateMap<CreatePhotoOnRateDTO, PhotoOnRate>();
        CreateMap<PhotoOnRate, ListPhotoOnRateDTO>().ForMember(x => x.Feedbacks, opt => opt.Ignore());;
        CreateMap<PhotoOnRate, DetailPhotoOnRateDTO>();
        CreateMap<PhotoOnRate, PhotoOnRateFeedbackDTO>();

        CreateMap<CreateFeedbackDTO, Feedback>()
            //.ForSourceMember(x => x.Tags, opt => opt.DoNotValidate());
            .ForMember(x => x.Tags, opt => opt.Ignore());

        CreateMap<Tag, TagDTO>();
        CreateMap<Status, StatusDTO>();

    }
    
}