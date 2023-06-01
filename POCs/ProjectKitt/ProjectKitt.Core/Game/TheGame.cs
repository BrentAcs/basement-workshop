using ProjectKitt.Core.Models;

namespace ProjectKitt.Core.Game;

public interface ITheGame
{
   MapGrid MapGrid { get; }
   List<IMapGridUnitObject> UnitObjects { get; }
   IFactionLookup Factions { get; }
}

public class TheGame : ITheGame
{
   public TheGame(IFactionLookup factions)
   {
      Factions = factions;
      // MapGrid = MapGridCreator.Get(factions);
   }

   public MapGrid MapGrid { get; set; } = new();
   public List<IMapGridUnitObject> UnitObjects { get; } = new();
   public IFactionLookup Factions { get; }
}

