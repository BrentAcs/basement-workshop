using BaddEcon.Core.Models;
using Bass.Shared.Extensions;

namespace BaddEcon.Core.Services.Repos;

public interface IBaseCommodityTypeRepo<T, U> where T : IBaseCommodityType
{
   IEnumerable<T> GetAll();
   T Get(U value);
}

public abstract class BaseCommodityTypeRepo<T, U> : IBaseCommodityTypeRepo<T, U> where T : IBaseCommodityType
{
   public abstract IEnumerable<T> GetAll();
   public abstract T Get(U value);
}

public class RawResourceTypeRepo : BaseCommodityTypeRepo<IRawResourceType, RawResource>
{
   public override IEnumerable<IRawResourceType> GetAll()
   {
      var values = Enum.GetValues<RawResource>();

      return Enum.GetValues<RawResource>()
         .Select(value => (RawResourceType)value.GetAttributeOfType<RawResourceTypeAttribute>());
   }

   public override IRawResourceType Get(RawResource value) => (RawResourceType)value.GetAttributeOfType<RawResourceTypeAttribute>();
}

// public class RefinedResourceTypeRepo : BaseCommodityTypeRepo<IRefinedResourceType>
// {
//    public override IEnumerable<IRefinedResourceType> GetAll() =>throw new NotImplementedException();
//       // new[]
//       // {
//       //    new RefinedResourceType {Id = 1, Name = "Refined Copper", Weight = 20,},
//       //    new RefinedResourceType {Id = 2, Name = "Refined Iron", Weight = 20,},
//       //    new RefinedResourceType {Id = 3, Name = "Refined Silver", Weight = 20,},
//       //    new RefinedResourceType {Id = 4, Name = "Refined Titanium", Weight = 20,},
//       // };
// }
