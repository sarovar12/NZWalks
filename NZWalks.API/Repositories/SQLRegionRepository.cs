using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateASync(Region region)
        {
            await dbContext.Regions.AddAsync(region); 
            await dbContext.SaveChangesAsync();
            return region;
        }


        public async Task<List<Region>> GetAllASync()
        {
            return await dbContext.Regions.ToListAsync();
        }


        public async Task<Region?> GetByIDASync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateASync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(y => y.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageURL= region.RegionImageURL;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteASync(Guid id)
        {
            var existsRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existsRegion == null)
            {
                return null;
            }
            dbContext.Regions.Remove(existsRegion);
            await dbContext.SaveChangesAsync();
            return existsRegion;
        }

    }
}
