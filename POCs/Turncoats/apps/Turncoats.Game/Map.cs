using System.Drawing;

namespace Turncoats.Game;

public class Map
{
   public Map()
   {
      Zones = new List<Zone>
      {
         // Col 0
         Zone.Create(0, 0,0, Stone.Black),
         Zone.Create(1, 0,1),
         // Col 1
         Zone.Create(2, 1,1),
         Zone.Create(3, 1,2),
         // col 2
         Zone.Create(4, 2,0),
         Zone.Create(5, 2,2, Stone.Red),
         // col 3
         Zone.Create(6, 3,0, Stone.Blue),
         Zone.Create(7, 3,1),
         Zone.Create(8, 3,2),
         // col 4
         Zone.Create(9, 4,0),
         Zone.Create(10, 4,1),
      };
   }

   public IEnumerable<Zone> Zones { get; private set; }

   public IEnumerable<Point> Locations => Zones.Select(_ => _.Location);
   public Size MaxSize => new(Locations.Select(_ => _.X).Max() + 1, Locations.Select(_ => _.Y).Max() + 1);

   public void Populate()
   {

   }
}
