using ProjectKitt.Core.Game;

namespace ProjectKitt.Core.Models;

public interface IMapGridObject
{
   PointF Location { get; }
   IFaction Owner { get; }
}

public abstract class MapGridObject : IMapGridObject
{
   public PointF Location { get; set; }
   public IFaction Owner { get; set; } = Faction.Default;
}