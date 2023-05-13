namespace Bass.Shared.Infrastructure.Storage;

public interface IMongoContext
{
   IMongoDbSettings Settings { get; }
   IMongoClient Client { get; }
   IMongoDatabase Db { get; }
}

public interface IMongoDbSettings
{
   string ConnectionString { get; set; }
   string DatabaseName { get; set; }
}