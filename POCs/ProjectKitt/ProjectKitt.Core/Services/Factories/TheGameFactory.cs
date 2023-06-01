using System.Linq.Expressions;
using System.Security.Cryptography;
using ProjectKitt.Core.Models;
using ProjectKitt.Core.Models.Scenarios;

namespace ProjectKitt.Core.Game;

public interface ITheGameFactory
{
   ITheGame CreateGame(GameScenario gameScenario);
}

public class TheGameFactory : ITheGameFactory
{
   private readonly IMapper _mapper;

   public TheGameFactory(IMapper mapper)
   {
      _mapper = mapper;
   }

   public ITheGame CreateGame(GameScenario gameScenario)
   {
      var factions = new FactionLookup();

      var mapGrid = new MapGrid {Size = gameScenario.MapGrid.Size};

      var theGame = new TheGame(factions) {MapGrid = mapGrid};

      foreach (var scenarioUnit in gameScenario.UnitObjects)
      {
         var unit = _mapper.Map<MapGridUnitObject>(scenarioUnit, opts => opts.Items[ "factions" ] = factions);
         if (scenarioUnit.OnMap)
         {
            mapGrid.Objects.Add(unit);
         }
         else
         {
            theGame.UnitObjects.Add(unit);
         }
      }

      return theGame;
   }
}

public class MappingProfile : Profile
{
   public MappingProfile()
   {
      CreateMap<GameScenarioMapGridUnitObject, MapGridUnitObject>()
         .ForMember(
            d => d.Owner,
            o => o.MapFrom(
               (src, dest, destMember, ctx) =>
               {
                  FactionLookup factions = (FactionLookup) ctx.Items[ "factions" ];
                  return factions.Get(src.OwnerName);
               })
         );
   }
}
