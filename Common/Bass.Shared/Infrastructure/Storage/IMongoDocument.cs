using MongoDB.Bson.Serialization.Attributes;

namespace Bass.Shared.Infrastructure.Storage;

public interface IMongoDocument<T> where T : IEquatable<T>
{
   // [BsonId]
   // [BsonRepresentation(BsonType.ObjectId)]
   // [JsonConverter(typeof(ObjectIdConverter))]
   // ObjectId Id { get; set; }
   T Id { get; set; }
}
