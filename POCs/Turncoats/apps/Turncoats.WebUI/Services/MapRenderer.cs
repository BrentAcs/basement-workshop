using System.Drawing;
using Bass.Shared.Extensions;
using Turncoats.Game;

namespace Turncoats.WebUI.Services;

public class MapRendererOptions
{
   public Size ClientSize { get; set; } = new(1200, 800);
   public Size Padding { get; set; } = new(10, 10);
}

// Ref:  https://github.com/excubo-ag/Blazor.Canvas/

public interface IMapRenderer
{
   MapRendererOptions Options { get; }
   // Task Render(Canvas2DContext ctx, MapRendererOptions options, Map map);
}

public class MapRenderer : IMapRenderer
{
   private IDictionary<Stone, Color> _stoneColorMap = new Dictionary<Stone, Color>
   {
      {Stone.Red, Color.Red},
      {Stone.Blue, Color.Blue},
      {Stone.Black, Color.Black},
   };

   public MapRendererOptions Options { get; set; } = new();

   private int ColWidth { get; set; }
   private float CircleDim => (float)(ColWidth * .95);
   private float CirclePad => (float)(ColWidth * .05);

   // public async Task Render(Canvas2DContext ctx, MapRendererOptions options, Map map)
   // {
   //    Options = options;
   //    ColWidth = new[] {options.ClientSize.Width / map.MaxSize.Width, options.ClientSize.Height / map.MaxSize.Height}
   //       .Min();
   //
   //    await ctx.SetFillStyleAsync("Gray");
   //    await ctx.FillRectAsync(0, 0, 1200, 800);
   //
   //    await ctx.SetFillStyleAsync("White");
   //    ctx.DrawEllipse(string.Empty, string.Empty, 2, new PointF(100,100), 50, 50);
   //
   //    // foreach (var zone in map.Zones)
   //    // {
   //    //    var location = ComputeLocation(zone.Location.X, zone.Location.Y);
   //    //    var rect = new RectangleF(location, new SizeF(CircleDim, CircleDim));
   //    //   
   //    //    // g.DrawEllipse(pen, rect);
   //    //    //
   //    //    // if (zone.IsHome)
   //    //    // {
   //    //    //    RenderHomeMarker(g, zone, rect);
   //    //    // }
   //    // }
   // }

   
   private Point ComputeLocation(int x, int y)
   {
      var location = new Point((int)(x * ColWidth + CirclePad / 2), (int)(y * ColWidth + CirclePad / 2));
      if (x.IsEven())
         location.Y += ColWidth / 2;

      return location;
   }   
}

// public static class Canvas2DContextExtensions
// {
//  
//    public static async Task DrawEllipse(this Canvas2DContext ctx, string brush, string pen, double thickness, PointF origin, double radX, double radY)
//    {
//       double w = radX;
//       double h = radY;
//       double cx = origin.X + radX / 2;
//       double cy = origin.Y + radY / 2;
//       double lx = cx - radX / 2;
//       double rx = cx + radX / 2;
//       double ty = cy - radY / 2;
//       double by = cy + radY / 2;
//       const double magic = 0.551784;
//       double magicX = magic * radX / 2;
//       double magicY = radY * magic / 2;
//       
//       await ctx.BeginPathAsync();
//       try
//       {
//          await ctx.MoveToAsync(cx, ty);
//          await ctx.BezierCurveToAsync(cx + magicX, ty, rx, cy - magicY, rx, cy);
//          await ctx.BezierCurveToAsync(rx, cy + magicY, cx + magicX, by, cx, by);
//          await ctx.BezierCurveToAsync(cx - magicX, by, lx, cy + magicY, lx, cy);
//          await ctx.BezierCurveToAsync(lx, cy - magicY, cx - magicX, ty, cx, ty);
//       }
//       finally
//       {
//          await ctx.ClosePathAsync();
//       }
//
//    }
// }
