using System.Drawing;
using Blazor.Extensions.Canvas.Canvas2D;
using Turncoats.Game;

namespace Turncoats.WebUI.Services;

public class MapRendererOptions
{
   public Size ClientSize { get; set; } = new(1200, 800);
   public Size Padding { get; set; } = new(10, 10);
}

public interface IMapRenderer
{
   MapRendererOptions Options { get; set; }
   Task Render(Canvas2DContext ctx, Map map);
}

public class MapRenderer : IMapRenderer
{
   public MapRendererOptions Options { get; set; } = new();

   private int ColWidth { get; set; }

   
   public async Task Render(Canvas2DContext ctx, Map map)
   {
      await ctx.SetFillStyleAsync("Gray");
      await ctx.FillRectAsync(0, 0, 1200,800);
   }
}
