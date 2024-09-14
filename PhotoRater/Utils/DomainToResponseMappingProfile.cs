using AutoMapper;
using PhotoRater.Models;
using PhotoRater.DTO;

namespace PhotoRater.Utils;


public class DomainToResponseMappingProfile: Profile
{
    public DomainToResponseMappingProfile()
    {
        // CreateMap<Models.OperationType, OperationTypeDto>();
        // CreateMap<CreateOperationTypeDto, Models.OperationType>();
        // CreateMap<UpdateOperationTypeDto, Models.OperationType>();

        CreateMap<CreatePhotoOnRateDTO, PhotoOnRate>();

    }
    
}