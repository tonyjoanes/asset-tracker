namespace Assets.API.Data
{
    using Assets.API.Entities;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class AssetContext : IAssetContext
    {
        private readonly DatabaseSettings _databaseSettings;

        public AssetContext(IOptions<DatabaseSettings> databaseSettings)
        {
            _databaseSettings = databaseSettings.Value;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);

            Assets = database.GetCollection<Asset>(_databaseSettings.CollectionName);

            // Seeding Database
            AssetContextSeed.SeedData(Assets);
        }

        public IMongoCollection<Asset> Assets { get; }
    }
}
