namespace Bass.Shared.Infrastructure.Storage;

// https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e

public interface IMongoRepository
{
   string? CollectionName { get; }
   Task InitializeIndexesAsync(CancellationToken cancellationToken = default);
   Task SeedDataAsync(CancellationToken cancellationToken = default);
}

public interface IMongoRepoCreate<TDoc, TId> where TDoc : IMongoDocument<TId> where TId : IEquatable<TId> 
{
   void InsertOne(TDoc document);
   Task InsertOneAsync(TDoc document, CancellationToken cancellationToken = default);
   void InsertMany(ICollection<TDoc> documents);
   Task InsertManyAsync(ICollection<TDoc> documents, CancellationToken cancellationToken = default);
}

public interface IMongoRepoRead<TDoc, TId> where TDoc : IMongoDocument<TId> where TId : IEquatable<TId>
{
   TDoc FindById(TId id);
   Task<TDoc> FindByIdAsync(TId id, CancellationToken cancellationToken=default);
#if USE_LINQ_TO_MONGO
   TDoc FindOne(Expression<Func<TDoc, bool>> filterExpression);
   Task<TDoc> FindOneAsync(Expression<Func<TDoc, bool>> filterExpression);
#endif
}

public interface IMongoRepoUpdate<TDoc, TId> where TDoc : IMongoDocument<TId> where TId : IEquatable<TId>
{
   void ReplaceOne(TDoc document);
   Task ReplaceOneAsync(TDoc document, CancellationToken cancellationToken = default);
}

public interface IMongoRepoDelete<TDoc, TId> where TDoc : IMongoDocument<TId> where TId : IEquatable<TId>
{
   void DeleteById(TId id);
   Task DeleteByIdAsync(TId id, CancellationToken cancellationToken = default);
#if USE_LINQ_TO_MONGO
   void DeleteOne(Expression<Func<TDoc, bool>> filterExpression);
   Task DeleteOneAsync(Expression<Func<TDoc, bool>> filterExpression);
   void DeleteMany(Expression<Func<TDoc, bool>> filterExpression);
   Task DeleteManyAsync(Expression<Func<TDoc, bool>> filterExpression);
#endif
}

public interface IMongoRepoCrud<TDoc, TId> :
   IMongoRepoCreate<TDoc, TId>,
   IMongoRepoRead<TDoc, TId>,
   IMongoRepoUpdate<TDoc, TId>,
   IMongoRepoDelete<TDoc, TId> where TDoc : IMongoDocument<TId> where TId : IEquatable<TId>
{
}

public interface IMongoRepoQueryable<TDoc, TId> where TDoc : IMongoDocument<TId> where TId : IEquatable<TId>
{
   IEnumerable<TDoc> All();
   IQueryable<TDoc> AsQueryable();
#if USE_LINQ_TO_MONGO
   IEnumerable<TDoc> FilterBy(Expression<Func<TDoc, bool>> filterExpression);
   IEnumerable<TProjected> FilterBy<TProjected>(
      Expression<Func<TDoc, bool>> filterExpression,
      Expression<Func<TDoc, TProjected>> projectionExpression);
#endif
}

public interface IMongoRepoPageable<TDoc, TId> where TDoc : IMongoDocument<TId> where TId : IEquatable<TId>
{
   long CalcPageCount(long count, int pageSize);

   Task<(long totalRecords, IEnumerable<TDoc> data)> AggregateByPage(
      FilterDefinition<TDoc> filterDefinition,
      SortDefinition<TDoc> sortDefinition,
      int page,
      int pageSize = 20);
}

public interface IMongoRepository<TDoc, TId> :
   IMongoRepository,
   IMongoRepoCrud<TDoc, TId>,
   IMongoRepoQueryable<TDoc, TId>,
   IMongoRepoPageable<TDoc, TId> where TDoc : IMongoDocument<TId> where TId : IEquatable<TId>
{
}
