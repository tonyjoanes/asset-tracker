namespace Assets.API.Data
{
    using Assets.API.Entities;
    using MongoDB.Driver;

    public interface IAssetContext
    {
        IMongoCollection<Asset> Assets { get; }
    }
}
