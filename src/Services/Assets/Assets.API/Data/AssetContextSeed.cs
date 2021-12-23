namespace Assets.API.Data
{
    using System;
    using System.Collections.Generic;
    using Assets.API.Entities;
    using MongoDB.Driver;

    public class AssetContextSeed
    {
        public static void SeedData(IMongoCollection<Asset> assetsCollection)
        {
            bool assetsExist = assetsCollection
                .Find(p => true)
                .Any();

            if (!assetsExist)
            {
                assetsCollection.InsertManyAsync(GetPreconfiguredAssets());
            }
        }

        private static IEnumerable<Asset> GetPreconfiguredAssets()
        {
            return new List<Asset>()
            {
                new Asset()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Microwave Oven",
                    Type = "KitchenAppliance",
                    Manufacturer = "Bosch",
                    Model = "B436266",
                    SerialNumber = "BOS1234467",
                    Status = "Working",
                    ImageFile = "oven1.png"
                },
                new Asset()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Vauxhall Mokka",
                    Type = "Car",
                    Manufacturer = "Vauxhall",
                    Model = "Mokka",
                    SerialNumber = "dvsgvbffdsbdb",
                    Status = "Working",
                    ImageFile = "mokka.png"
                }
            };
        }
    }
}