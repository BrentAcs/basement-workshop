using ProjectKitt.Core.Models;
using ProjectKitt.Core.Services;

namespace ProjectKitt.Core.Game;

public interface ITheGame
{
   MapGrid MapGrid { get; }
   IFactionLookup Factions { get; }
}

public class TheGame : ITheGame
{
   public TheGame(IFactionLookup factions)
   {
      Factions = factions;
      MapGrid = MapGridCreator.Get(factions);
   }

   public MapGrid MapGrid { get; set; } = new();
   public IFactionLookup Factions { get; }
}