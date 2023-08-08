using System.Drawing;

namespace Turncoats.Game;

public class Map
{
   public List<Zone> Zones { get; set; } = new();
   public IEnumerable<Point> Locations => Zones.Select(_ => _.Location);
   public Size MaxSize => new(Locations.Select(_ => _.X).Max() + 1, Locations.Select(_ => _.Y).Max() + 1);
}

