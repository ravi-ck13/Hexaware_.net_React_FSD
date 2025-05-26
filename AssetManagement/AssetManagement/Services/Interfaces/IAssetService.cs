using AssetManagement.Models;

namespace AssetManagement.Services.Interfaces
{
    public interface IAssetService
    {
        Task<IEnumerable<Asset>> GetAllAsync();
        Task<Asset?> GetByIdAsync(int id);
        Task<Asset> CreateAsync(Asset asset);
        Task<Asset?> UpdateAsync(int id, Asset asset);
        Task<bool> DeleteAsync(int id);
    }
}
