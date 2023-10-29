using AutoMapper;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {

       public AutoMapperProfiles() 
        {
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<CreateRegionDto,Region>().ReverseMap();
            CreateMap<UpdateRegionDto,Region>().ReverseMap();

            CreateMap<CreateWalkDto,Walk>().ReverseMap();
            CreateMap<UpdateWalkDto, Walk>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();  

            CreateMap<Difficulty,DifficultyDto>().ReverseMap();
      
        }




    }
}
