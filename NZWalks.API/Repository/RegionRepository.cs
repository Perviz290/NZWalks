using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repository.IRepository;
using System.Net;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AppDbContext _db;
        public RegionRepository(AppDbContext db)
        {
            this._db = db;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _db.Regions.AddAsync(region);
            await _db.SaveChangesAsync();

            return region;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            return await _db.Regions.ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _db.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Region?> UpdateAsync(UpdateRegionDto updateRegionDto, Region existingRegion)
        {
            existingRegion.Code = updateRegionDto.Code;
            existingRegion.Name = updateRegionDto.Name;
            existingRegion.RegionImageUrl = updateRegionDto.RegionImageUrl;
            await _db.SaveChangesAsync();
            return existingRegion;
        }
        public async Task<Region?> DeleteAsync(Region deleteRegion)
        {
           _db.Regions.Remove(deleteRegion);
            await _db.SaveChangesAsync();
            return deleteRegion;
        }







    }
}
