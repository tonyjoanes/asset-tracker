namespace Assets.API.Entities
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Asset
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")] 
        public string Name { get; set; }

        public string Type { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        public string Status { get; set; }

        public string ImageFile { get; set; }
    }
}
