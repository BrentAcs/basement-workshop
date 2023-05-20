using ProjectKitt.Core.Models;
using ProjectKitt.Core.Services;

namespace ProjectKitt.Core.Game;

public interface ITheGame
{
   MapGrid MapGrid { get; }
   IFactionCollection Factions { get; }   
}

public class TheGame : ITheGame
{
   private readonly IMapGridRepo _mapGridRepo;

   public TheGame(IFactionCollection factions, IMapGridRepo mapGridRepo)
   {
      Factions = factions;
      _mapGridRepo = mapGridRepo;
      
      MapGrid = _mapGridRepo.Get();
   }

   public MapGrid MapGrid { get; set; } = new();
   public IFactionCollection Factions { get; }
}
