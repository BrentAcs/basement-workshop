﻿namespace Bass.Shared.Infrastructure.Storage;

public class MongoRepository<TDoc, TId> : IMongoRepository<TDoc, TId> where TDoc : IMongoDocument<TId> where TId : IEquatable<TId>
{
   public MongoRepository(IMongoContext? mongoContext, ILogger<MongoRepository<TDoc, TId>> logger)
   {
      MongoContext = mongoContext;
      Logger = logger;
      Collection = MongoContext!.Db.GetCollection<TDoc>(CollectionName);
   }

   protected IMongoContext? MongoContext { get; set; }
   protected ILogger<MongoRepository<TDoc, TId>> Logger { get; }
   protected IMongoCollection<TDoc> Collection { get; }

   // --- IMongoRepository

   public string? CollectionName => GetCollectionName(typeof(TDoc));

   public virtual Task InitializeIndexesAsync(CancellationToken cancellationToken = default) =>
      Task.CompletedTask;

   public virtual Task SeedDataAsync(CancellationToken cancellationToken = default) =>
      Task.CompletedTask;

   protected virtual async Task<bool> NeedsSeedDataAsync(CancellationToken cancellationToken = default)
   {
      var cursor = await Collection
            .FindAsync(FilterDefinition<TDoc>.Empty, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
      return !await cursor.AnyAsync(cancellationToken).ConfigureAwait(false);
   }
  
   // --- IMongoRepoCreate

   public virtual void InsertOne(TDoc document) =>
      Collection.InsertOne(document);

   public virtual Task InsertOneAsync(TDoc document, CancellationToken cancellationToken = default) =>
      Task.Run(() => Collection.InsertOneAsync(document, cancellationToken: cancellationToken), cancellationToken);

   public void InsertMany(ICollection<TDoc> documents) =>
      Collection.InsertMany(documents);

   public virtual async Task
      InsertManyAsync(ICollection<TDoc> documents, CancellationToken cancellationToken = default) =>
      await Collection.InsertManyAsync(documents, cancellationToken: cancellationToken);

   // --- IMongoRepoRead

   public virtual TDoc FindById(TId id)
   {
      //var objectId = new ObjectId(id);
      var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, id);
      return Collection.Find(filter).SingleOrDefault();
   }

   public virtual Task<TDoc> FindByIdAsync(TId id, CancellationToken cancellationToken = default) =>
      Task.Run(() =>
      {
         //var objectId = new ObjectId(id);
         var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, id);
         return Collection.Find(filter).SingleOrDefaultAsync(cancellationToken);
      });

   // --- IMongoRepoUpdate

   public void ReplaceOne(TDoc document)
   {
      var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, document.Id);
      Collection.FindOneAndReplace(filter, document);
   }

   public virtual async Task ReplaceOneAsync(TDoc document, CancellationToken cancellationToken = default)
   {
      var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, document.Id);
      await Collection.FindOneAndReplaceAsync(filter, document, cancellationToken: cancellationToken)
         .ConfigureAwait(false);
   }

   // --- IMongoRepoDelete

   public void DeleteById(TId id)
   {
      //var objectId = new ObjectId(id);
      var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, id);
      Collection.FindOneAndDelete(filter);
   }

   public Task DeleteByIdAsync(TId id, CancellationToken cancellationToken = default) =>
      Task.Run(() =>
      {
         // var objectId = new ObjectId(id);
         var filter = Builders<TDoc>.Filter.Eq(doc => doc.Id, id);
         Collection.FindOneAndDeleteAsync(filter, cancellationToken: cancellationToken);
      });

   // --- IMongoRepoQueryable

   public IEnumerable<TDoc> All() =>
      AsQueryable()
         .ToList();

   public virtual IQueryable<TDoc> AsQueryable() =>
      Collection.AsQueryable();

   // --- IMongoRepoPageable

   public long CalcPageCount(long count, int pageSize) =>
      (long)Math.Ceiling((double)count / pageSize);

   public async Task<(long totalRecords, IEnumerable<TDoc> data)> AggregateByPage(
      FilterDefinition<TDoc> filterDefinition,
      SortDefinition<TDoc> sortDefinition,
      int page,
      int pageSize = 20)
   {
      // ref:  https://kevsoft.net/2020/01/27/paging-data-in-mongodb-with-csharp.html
      var countFacet = AggregateFacet.Create("count",
         PipelineDefinition<TDoc, AggregateCountResult>
            .Create(new[] {PipelineStageDefinitionBuilder.Count<TDoc>()}));

      var dataFacet = AggregateFacet.Create("data",
         PipelineDefinition<TDoc, TDoc>.Create(new[]
         {
            PipelineStageDefinitionBuilder
               .Sort(sortDefinition),
            PipelineStageDefinitionBuilder.Skip<TDoc>((page - 1) * pageSize), PipelineStageDefinitionBuilder
               .Limit<TDoc>(pageSize),
         }));

      var aggregation = await Collection.Aggregate()
         .Match(filterDefinition)
         .Facet(countFacet, dataFacet)
         .ToListAsync();

      var count = aggregation.First()
         .Facets.First(x => x.Name == "count")
         .Output<AggregateCountResult>()
         ?.FirstOrDefault()
         ?.Count;

      //var totalPages = (int)Math.Ceiling((double)count / pageSize);

      var data = aggregation.First()
         .Facets.First(x => x.Name == "data")
         .Output<TDoc>();

      return (count.Value, data);
      //return (totalPages, data);
   }

   private static string? GetCollectionName(Type documentType) =>
      ((BsonCollectionAttribute)documentType.GetCustomAttributes(
            typeof(BsonCollectionAttribute),
            true)
         .FirstOrDefault()!)?.CollectionName;

#if USE_LINQ_TO_MONGO
   public virtual TDoc FindOne(Expression<Func<TDoc, bool>> filterExpression) =>
      Collection.Find(filterExpression).FirstOrDefault();

   public virtual Task<TDoc> FindOneAsync(Expression<Func<TDoc, bool>> filterExpression) =>
      Task.Run(() => Collection.Find(filterExpression).FirstOrDefaultAsync());
#endif
#if USE_LINQ_TO_MONGO
   public virtual IEnumerable<TDoc> FilterBy(Expression<Func<TDoc, bool>> filterExpression) =>
      Collection.Find(filterExpression).ToEnumerable();

   public virtual IEnumerable<TProjected> FilterBy<TProjected>(
      Expression<Func<TDoc, bool>> filterExpression,
      Expression<Func<TDoc, TProjected>> projectionExpression) =>
      Collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
#endif
#if USE_LINQ_TO_MONGO
   public void DeleteOne(Expression<Func<TDoc, bool>> filterExpression) =>
      Collection.FindOneAndDelete(filterExpression);

   public Task DeleteOneAsync(Expression<Func<TDoc, bool>> filterExpression) =>
      Task.Run(() => Collection.FindOneAndDeleteAsync(filterExpression));


   public void DeleteMany(Expression<Func<TDoc, bool>> filterExpression) =>
      Collection.DeleteMany(filterExpression);

   public Task DeleteManyAsync(Expression<Func<TDoc, bool>> filterExpression) =>
      Task.Run(() => Collection.DeleteManyAsync(filterExpression));
#endif
}
