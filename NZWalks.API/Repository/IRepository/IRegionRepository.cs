using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Repository.IRepository
{
    public interface IRegionRepository
    {

        Task<List<Region>>GetAllAsync();

        Task<Region>GetByIdAsync(Guid id);

        Task<Region>CreateAsync(Region region);

        Task<Region?>UpdateAsync(UpdateRegionDto updateRegionDto,Region region);

        Task<Region?>DeleteAsync(Region deleteRegion);


    }
}
