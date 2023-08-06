using BaddEcon.Core.Models;
using Bass.Shared.Infrastructure.Storage;

namespace BaddEcon.Core.Services.Lookups;

public interface IBaseCommodityTypeLookup<TType> where TType : IBaseCommodityType
{
   IEnumerable<TType> GetAll();
   TType Find(int id);
   Task<TType> FindAsync(int id, CancellationToken cancellationToken=default);
}

public abstract class BaseCommodityTypeLookup<TType> :
   IBaseCommodityTypeLookup<TType> where TType : IBaseCommodityType
{
   private readonly IMongoRepository<TType, int> _repo;

   protected BaseCommodityTypeLookup(IMongoRepository<TType, int> repo)
   {
      _repo = repo;
   }

   public IEnumerable<TType> GetAll() => _repo.All();
   public TType Find(int id) => _repo.FindById(id);

   public Task<TType> FindAsync(int id, CancellationToken cancellationToken = default) =>
      _repo.FindByIdAsync(id, cancellationToken);
}
