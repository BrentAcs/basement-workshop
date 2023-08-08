using System.Drawing;

namespace Turncoats.Game;

public class Zone
{
   public int Id { get; set; }
   public Point Location { get; set; }
   public Stone HomeFor { get; set; }
   public bool IsHome => HomeFor != Stone.None;
   public StonesForZone Stones { get; init; } = new();

   public static Zone Create(int id, int x, int y, Stone homeFor = Stone.None) =>
      new()
      {
         Id = id,
         Location = new Point(x, y),
         HomeFor = homeFor
      };
}
