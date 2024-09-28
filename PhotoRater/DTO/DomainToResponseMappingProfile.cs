using AutoMapper;
using PhotoRater.Models;
using PhotoRater.DTO;
using PhotoRater.Utils.Feedback;

namespace PhotoRater.Utils;


public class DomainToResponseMappingProfile: Profile
{
    public DomainToResponseMappingProfile()
    {
        // CreateMap<Models.OperationType, OperationTypeDto>();
        // CreateMap<CreateOperationTypeDto, Models.OperationType>();
        // CreateMap<UpdateOperationTypeDto, Models.OperationType>();

        CreateMap<CreatePhotoOnRateDTO, PhotoOnRate>();
        CreateMap<PhotoOnRate, ListPhotoOnRateDTO>();
        CreateMap<PhotoOnRate, DetailPhotoOnRateDTO>();
        CreateMap<PhotoOnRate, PhotoOnRateFeedbackDTO>();

        CreateMap<CreateFeedbackDTO, Models.Feedback>();

    }
    
}