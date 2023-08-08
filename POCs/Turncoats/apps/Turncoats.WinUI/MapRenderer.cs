using Bass.Shared.Extensions;
using Turncoats.Game;

namespace Turncoats.WinUI;

public class MapRenderer
{
   private IDictionary<Stone, Color> _stoneColorMap = new Dictionary<Stone, Color>
   {
      { Stone.Red , Color.Red},
      { Stone.Blue , Color.Blue},
      { Stone.Black , Color.Black},
   };

   private int ColWidth { get; set; }
   private float CircleDim => (float)(ColWidth * .95);
   private float CirclePad => (float)(ColWidth * .05);

   public void Render(Graphics g, Map map, Size clientSize)
   {
      ColWidth = new[] { clientSize.Width / map.MaxSize.Width, clientSize.Height / map.MaxSize.Height }.Min();
      //var colCenter = ColWidth / 2;

      using var pen = new Pen(Color.DimGray, 2);

      foreach (var zone in map.Zones)
      {
         var location = ComputeLocation(zone.Location.X, zone.Location.Y);
         var rect = new RectangleF(location, new SizeF(CircleDim, CircleDim));
         g.DrawEllipse(pen, rect);

         if (zone.IsHome)
         {
            RenderHomeMarker(g, zone, rect);
         }
      }
   }

   private void RenderHomeMarker(Graphics g, Zone zone, RectangleF rect)
   {
      using var homeMarkerPen = new Pen(_stoneColorMap[zone.HomeFor], 2);
       var homeCircleSize = (float)(rect.Width * .05);
      var center = rect.GetCenter();
      var inner = new RectangleF(center, SizeF.Empty);
      inner.Inflate(homeCircleSize, homeCircleSize);

      inner.X -= homeCircleSize * 2;
      inner.Y -= homeCircleSize * 6;
      g.DrawEllipse(homeMarkerPen, inner);

      inner.X += homeCircleSize * 4;
      g.DrawEllipse(homeMarkerPen, inner);
   }

   private Point ComputeLocation(int x, int y)
   {
      var location = new Point((int)(x * ColWidth + CirclePad / 2), (int)(y * ColWidth + CirclePad / 2));
      if (x.IsEven())
         location.Y += ColWidth / 2;

      return location;
   }

}
