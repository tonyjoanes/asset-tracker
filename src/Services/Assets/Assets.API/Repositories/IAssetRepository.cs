namespace Assets.API.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Assets.API.Entities;

    public interface IAssetRepository
    {
        Task<IEnumerable<Asset>> GetAssets();
        Task<Asset> GetAsset(string id);
        Task<IEnumerable<Asset>> GetAssetByName(string name);
        Task<IEnumerable<Asset>> GetAssetByType(string type);
        Task CreateAsset(Asset asset);
        Task<bool> UpdateAsset(Asset asset);
        Task<bool> DeleteAsset(string id);
    }
}
