using System.Drawing;
using System.Net;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Turncoats.Game;
using Turncoats.WebUI.Services;

namespace Turncoats.WebUI.Pages;

public partial class TurncoatsMap
{
   private Canvas2DContext _canvas2DContext;
   private IMapRenderer _mapRenderer = new MapRenderer();
   private TheGame _game = new();
   
   public ElementReference _divCanvas;
   public BECanvasComponent _beCanvas;

   [Inject]
   public IJSRuntime jsRuntime { get; set; }
   [Inject]
   public IMapCanvasRuntime MapCanvasRuntime { get; set; }

   public TurncoatsMap()
   {
      // _map = new StockMapGenerator().Generate();
      // new StockMapPopulator().Populate(_map);
   }
   
   protected override async Task OnAfterRenderAsync(bool firstRender)
   {
      if (firstRender)
      {
         _canvas2DContext = await _beCanvas.CreateCanvas2DAsync().ConfigureAwait(false);
         //await _canvas2DContext.SetTextBaselineAsync(TextBaseline.Top);
         await _mapRenderer.Render(_canvas2DContext, _game.Map).ConfigureAwait(false);
      }
   }
   
   public async void OnClick(MouseEventArgs e)
   {
   }

   public async void OnMouseOver(MouseEventArgs e)
   {
      Console.WriteLine("OnMouseOver.");
   }

   public async void OnMouseEnter(MouseEventArgs e)
   {
      Console.WriteLine("OnMouseEnter....");
      // var info = await MapCanvasRuntime.GetClientInfo(divCanvas);
      // var mousePos = new PointF
      // {
      //    X = (float)(e.ClientX - info.Offset.X),
      //    Y = (float)(e.ClientY - info.Offset.Y)
      // };
      // Console.WriteLine($"   mouse Pos: {mousePos.X}, {mousePos.Y}, {info.ClientSize.Width}, {info.ClientSize.Height}");
      
      
      // var ctx = await _beCanvas.CreateCanvas2DAsync();
      // await ctx.SetFillStyleAsync("Gray");
      // await ctx.FillRectAsync(0, 0, 1200,800);
   }
}

