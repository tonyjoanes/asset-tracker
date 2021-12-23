namespace Assets.API.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Assets.API.Data;
    using Assets.API.Entities;
    using MongoDB.Driver;

    public class AssetRepository : IAssetRepository
    {
        private readonly IAssetContext _context;

        public AssetRepository(IAssetContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asset>> GetAssets()
        {
            return await _context.Assets
                .Find(prop => true)
                .ToListAsync();
        }

        public async Task<Asset> GetAsset(string id)
        {
            return await _context.Assets
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssetByName(string name)
        {
            FilterDefinition<Asset> filter = Builders<Asset>.Filter.ElemMatch(p => p.Name, name);

            return await _context.Assets
                .Find(filter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Asset>> GetAssetByType(string type)
        {
            FilterDefinition<Asset> filter = Builders<Asset>.Filter.Eq(p => p.Type, type);

            return await _context.Assets
                .Find(filter)
                .ToListAsync();
        }

        public async Task CreateAsset(Asset asset)
        {
            await _context.Assets.InsertOneAsync(asset);
        }

        public async Task<bool> UpdateAsset(Asset asset)
        {
            var updateResult = await _context.Assets
                .ReplaceOneAsync(filter: g => g.Id == asset.Id, replacement: asset);

            return updateResult.IsAcknowledged
                && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsset(string id)
        {
            var deleteResult = await _context.Assets.DeleteOneAsync<Asset>(p => p.Id == id);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }   
    }
}
