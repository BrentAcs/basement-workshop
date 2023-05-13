using MongoDB.Bson.Serialization.Attributes;

namespace Bass.Shared.Infrastructure.Storage;

public interface IMongoDocument
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   [JsonConverter(typeof(ObjectIdConverter))]
   ObjectId Id { get; set; }
}