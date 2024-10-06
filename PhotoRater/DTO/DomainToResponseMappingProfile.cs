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
        CreateMap<PhotoOnRate, ListPhotoOnRateDTO>();
        CreateMap<PhotoOnRate, DetailPhotoOnRateDTO>();
        CreateMap<PhotoOnRate, PhotoOnRateFeedbackDTO>();

        CreateMap<CreateFeedbackDTO, Models.Feedback>();

        CreateMap<Tag, TagDTO>();
        CreateMap<Status, StatusDTO>();

    }
    
}