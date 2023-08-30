using Bass.Shared.Extensions;
using Turncoats.Game;

namespace Turncoats.WinUI;

public class MapRenderer
{
   private IDictionary<Stone, Color> _stoneColorMap = new Dictionary<Stone, Color>
   {
      { Stone.Red, Color.Red }, { Stone.Blue, Color.Blue }, { Stone.Black, Color.Black },
   };

   private int ColWidth { get; set; }
   private float CircleDim => (float)(ColWidth * .95);
   private float CirclePad => (float)(ColWidth * .05);

   public void Render(Graphics g, Map map, Size clientSize)
   {
      ColWidth = new[] { clientSize.Width / map.MaxSize.Width, clientSize.Height / map.MaxSize.Height }.Min();

      using var blackPen = new Pen(Color.Black, 1);
      using var pen = new Pen(Color.DimGray, 2);

      foreach (var zone in map.Zones)
      {
         var location = ComputeLocation(zone.Location.X, zone.Location.Y);

         var borderRect = new RectangleF(location, new SizeF(ColWidth, ColWidth));
         g.DrawRectangle(blackPen, borderRect);

         location.Offset((int)(CirclePad / 2), (int)(CirclePad / 2));
         var rect = new RectangleF(location, new SizeF(CircleDim, CircleDim));
         g.DrawEllipse(pen, rect);

         if (zone.IsHome)
         {
            RenderHomeMarker(g, zone, rect);
         }

         RenderStones(g, zone, rect);
      }
   }

   private void RenderStones(Graphics g, Zone zone, RectangleF rect)
   {
      int y = -1;
      var circleSize = (float)(rect.Width * .1);
      var center = rect.GetCenter();

      var ys = new[]
      {
         center.Y - circleSize * 2,
         center.Y,
         center.Y + circleSize * 2
      };

      foreach (var stone in _stoneColorMap.Keys)
      {
         y++;
         int quantity = zone.Stones.QuantityFor(stone);
         if (quantity == 0)
            continue;

         using var brush = new SolidBrush(_stoneColorMap[stone]);
         var x = center.X - (circleSize * quantity / 2);

         for (int i = 0; i < quantity; i++, x+=circleSize)
         {
            var inner = new RectangleF(x, ys[y], circleSize, circleSize);
            g.FillEllipse(brush, inner);
         }

      }
   }

private void RenderHomeMarker(Graphics g, Zone zone, RectangleF rect)
   {
      using var homeMarkerPen = new Pen(_stoneColorMap[zone.HomeFor], 4);
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
