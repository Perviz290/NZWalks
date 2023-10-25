using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repository.IRepository
{
    public interface IWalkRepository
    {

        Task<Walk>CreateAsync(Walk walk);



    }
}
