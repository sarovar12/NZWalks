using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllASync();
        Task<Region?> GetByIDASync(Guid id);

        Task<Region> CreateASync(Region region);
        Task<Region?> UpdateASync(Guid id, Region region); 
        Task<Region?> DeleteASync(Guid id); 

    }
}
