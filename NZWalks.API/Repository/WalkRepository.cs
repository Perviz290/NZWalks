using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Repository.IRepository;

namespace NZWalks.API.Repository
{
    public class WalkRepository : IWalkRepository
    {

        private readonly AppDbContext _db;
        public WalkRepository(AppDbContext db)
        {
           this._db = db;
        }


        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _db.Walks.AddAsync(walk);
            await _db.SaveChangesAsync();
            return walk;
        }








    }
}
